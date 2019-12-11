using System;
using System.Windows;
using System.Diagnostics;
using System.Threading;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TimeManagementReportingSystem
{
    /// <summary>
    /// Interaction logic for AddEventForm.xaml
    /// </summary>
    public partial class AddEventForm : Window
    {
        private int numberOfColumns = 8;
        private const int textBoxWidth = 115;
        private const int textBoxHeight = 40;
        private const int numberOfEntries = 1;

        StackPanel entryPanel;

        public AddEventForm()
        {
            InitializeComponent();
            CreateColumns();
            CreateInitialEntries();
            ((DatePicker)entryPanel.Children[2]).SelectedDate = DateTime.Now;
        }

        private void CreateColumns()
        {
            StackPanel columnPanel = new StackPanel();
            columnPanel.Orientation = Orientation.Horizontal;

            string[] columns = { "Name", "Email", "Date", "Location", "Recipient", "Duration", "Event Type", "Recurring Type" };

            foreach (var column in columns)
            {
                TextBox textBox = new TextBox();
                textBox.Width = textBoxWidth;
                textBox.Height = textBoxHeight;
                textBox.Text = column;
                textBox.IsReadOnly = true;
                columnPanel.Children.Add(textBox);
            }

            MainPanel.Children.Add(columnPanel);
        }

        private void CreateInitialEntries()
        {
            entryPanel = new StackPanel();
            entryPanel.Orientation = Orientation.Horizontal;

            for (int i = 0; i < numberOfColumns; i++)
            {
                if (i == 2)
                {
                    DatePicker datePicker = new DatePicker();
                    datePicker.Width = textBoxWidth;
                    datePicker.Height = textBoxHeight;
                    entryPanel.Children.Add(datePicker);
                }
                else if (i == 6)
                {
                    ListBox listBox = new ListBox();
                    listBox.Items.Add(new string("Task"));
                    listBox.Items.Add(new string("Appointment"));
                    entryPanel.Children.Add(listBox);
                }
                else if (i == 7)
                {
                    CheckBox checkBox = new CheckBox();
                    checkBox.Width = textBoxWidth;
                    checkBox.Height = textBoxHeight;
                    entryPanel.Children.Add(checkBox);
                }
                else
                {
                    TextBox textBox = new TextBox();
                    textBox.Width = textBoxWidth;
                    textBox.Height = textBoxHeight;
                    entryPanel.Children.Add(textBox);
                }
            }

            MainPanel.Children.Add(entryPanel);
        }

        private void SubmitFormButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsDataValid())
            {
                SaveEntriesFromDynamicComponents();
                ClearForm();
            }
            else
            {
                MessageBox.Show("Invalid entry");
            }
        }



        private bool IsDataValid()
        {
            for (int i = 0; i < entryPanel.Children.Count; i++)
            {
                if (entryPanel.Children[i].GetType() == typeof(TextBox))
                {
                    TextBox textBox = (TextBox)entryPanel.Children[i];
                    Trace.WriteLine(textBox.Text);

                    // if duration textbox
                    if (i == 5)
                    {
                        if (!IsDurationValid(textBox.Text))
                            return false;
                    }
                    // else other textboxes
                    else
                    {
                        if (!IsTextBoxValid(textBox.Text))
                            return false;
                    }
                }
                else if (entryPanel.Children[i].GetType() == typeof(DatePicker))
                {
                    DatePicker datePicker = (DatePicker)entryPanel.Children[i];
                    string date = datePicker.SelectedDate.Value.ToShortDateString();

                    if (!IsDateValid(date))
                        return false;
                }
            }

            return true;
        }

        private void SaveEntriesFromDynamicComponents()
        {
            string name = "";
            string contact = "";
            string date = "";
            string location = "";
            string recipient = "";
            int timeUsage = 0;
            EventType eventType = EventType.ONE_OFF;

            bool isAppointment = false;

            for (int i = 0; i < entryPanel.Children.Count; i++)
            {
                if (entryPanel.Children[i].GetType() == typeof(TextBox))
                {
                    TextBox textBox = (TextBox)entryPanel.Children[i];
                    Trace.WriteLine(textBox.Text);

                    switch (i)
                    {
                        case 0:
                            name = textBox.Text;
                            break;

                        case 1:
                            contact = textBox.Text;
                            break;

                        case 3:
                            location = textBox.Text;
                            break;

                        case 4:
                            recipient = textBox.Text;
                            break;

                        case 5:
                            timeUsage = int.Parse(textBox.Text);
                            break;
                    }
                }
                else if (entryPanel.Children[i].GetType() == typeof(DatePicker))
                {
                    DatePicker datePicker = (DatePicker)entryPanel.Children[i];
                    date = datePicker.SelectedDate.Value.ToShortDateString();
                }
                else if (entryPanel.Children[i].GetType() == typeof(ListBox))
                {
                    ListBox listBox = (ListBox)entryPanel.Children[i];

                    if (listBox.SelectedItem.ToString() == "Appointment")
                        isAppointment = true;

                    Trace.WriteLine(listBox.SelectedItem);
                }
                else if (entryPanel.Children[i].GetType() == typeof(CheckBox))
                {
                    CheckBox checkBox = (CheckBox)entryPanel.Children[i];
                    Trace.WriteLine(checkBox.IsChecked ?? false);

                    if (checkBox.IsChecked == true)
                    {
                        eventType = EventType.RECURRING;
                    }
                    else
                    {
                        eventType = EventType.ONE_OFF;
                    }
                }
            }

            Event eventToAdd = null;

            if (isAppointment)
            {
                eventToAdd = new Appointment(name, contact, date, location, timeUsage, recipient);
                eventToAdd.EventType = eventType;
            }
            else
            {
                eventToAdd = new Task(name, contact, date, location, timeUsage);
                eventToAdd.EventType = eventType;
            }

            SaveEventUsingThread(eventToAdd);

            MessageBox.Show("Saved Events!");
        }

        private void ClearForm()
        {
            for (int i = 0; i < entryPanel.Children.Count; i++)
            {
                if (entryPanel.Children[i].GetType() == typeof(TextBox))
                {
                    TextBox textBox = (TextBox)entryPanel.Children[i];
                    textBox.Text = "";
                }
            }
        }

        private void SaveEventUsingThread(Event eventToAdd)
        {
            try
            {
                AddEvent_Model addEvent_Model = new AddEvent_Model(new XMLManager());

                // Create a thread and get the thread to execute a function 
                Thread thread = new Thread(() => addEvent_Model.AddEvent(eventToAdd));
                thread.Start();
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
        }

        private bool IsTextBoxValid(string textboxValue) => textboxValue != "";

        private bool IsDurationValid(string duration)
        {
            Regex regex = new Regex("^[0-9]+$");

            if (duration == "")
            {
                return false;
            }
            else if (regex.IsMatch(duration))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool IsDateValid(string date)
        {
            if (date == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void CancelFormButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddEntriesButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

