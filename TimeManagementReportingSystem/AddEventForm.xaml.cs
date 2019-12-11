using System;
using System.Windows;
using System.Diagnostics;

namespace TimeManagementReportingSystem
{
    /// <summary>
    /// Interaction logic for AddEventForm.xaml
    /// </summary>
    public partial class AddEventForm : Window
    {
        public AddEventForm()
        {
            InitializeComponent();
            EnableOrDisableComponents(false);
            RecipientTextBox.IsEnabled = false;
        }


        private void CreateTextBox()
        {
            
        }

        private void SubmitFormButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsDataValid())
            {
                PrintAllEvents();
                AddEvent_Model addEvent_Model = new AddEvent_Model(new XMLManager());
                addEvent_Model.AddEvent(GetData());

                MessageBox.Show("Saved event successfully.");
                ClearForm();
            }
        }

        private bool IsDataValid()
        {
            // Check if the textfields contain data and are valid

            bool taskIsChecked = TaskRadioButton.IsChecked ?? false;
            bool appointmentIsChecked = AppointmentRadioButton.IsChecked ?? false;

            if (taskIsChecked)
            {
                if (IsTaskContentFormValid())
                    return true;
            }
            else if (appointmentIsChecked)
            {
                if (IsAppointmentContentFormValid())
                    return true;
            }

            return false;
        }

        private void PrintAllEvents()
        {
            foreach (var eventInList in EventsDataHandler.GetInstance().events)
            {
                Trace.WriteLine(eventInList.ToString());
            }
        }

        private void ClearForm()
        {
            NameTextBox.Text = "";
            ContactTextBox.Text = "";
            DateTimePicker.Text = "";
            LocationTextBox.Text = "";
            RecipientTextBox.Text = "";
            DurationTextBox.Text = "";
        }

        private Event GetData()
        {
            bool taskIsChecked = TaskRadioButton.IsChecked ?? false;
            bool appointmentIsChecked = AppointmentRadioButton.IsChecked ?? false;

            string name = NameTextBox.Text;
            string contact = ContactTextBox.Text;
            string datetime = DateTimePicker.SelectedDate.ToString().Split(' ')[0];
            string location = LocationTextBox.Text;
            int timeUsage = Convert.ToInt32(DurationTextBox.Text);
            string recipient = RecipientTextBox.Text;

            if (taskIsChecked)
            {
                Task taskToAdd = new Task(name, contact, datetime, location, timeUsage);

                bool isChecked = EventTypeCheckBox.IsChecked ?? false;

                if (isChecked)
                    taskToAdd.EventType = EventType.RECURRING;
                else
                    taskToAdd.EventType = EventType.ONE_OFF;

                return taskToAdd;
            }
            else if (appointmentIsChecked)
            {
                Appointment appointmentToAdd = new Appointment(name, contact, datetime, location, timeUsage, recipient);

                bool isChecked = EventTypeCheckBox.IsChecked ?? false;

                if (isChecked)
                    appointmentToAdd.EventType = EventType.RECURRING;
                else
                    appointmentToAdd.EventType = EventType.ONE_OFF;

                return appointmentToAdd;
            }

            return null;
        }

        private bool IsTaskContentFormValid()
        {
            if (!IsTextBoxValid(NameTextBox.Text) || !IsTextBoxValid(ContactTextBox.Text)
                || !IsTextBoxValid(DateTimePicker.SelectedDate.ToString()) || !IsTextBoxValid(LocationTextBox.Text)
                || !IsTextBoxValid(DurationTextBox.Text))
            {
                MessageBox.Show("You must fill in all fields...");
                return false;
            }

            return true;
        }

        private bool IsAppointmentContentFormValid()
        {
            if (!IsTextBoxValid(NameTextBox.Text) || !IsTextBoxValid(ContactTextBox.Text)
             || !IsTextBoxValid(DateTimePicker.SelectedDate.ToString())
             || !IsTextBoxValid(LocationTextBox.Text) || !IsTextBoxValid(RecipientTextBox.Text)
             || !IsTextBoxValid(DurationTextBox.Text))
            {
                MessageBox.Show("You must fill in all fields...");
                return false;
            }

            return true;
        }

        private bool IsTextBoxValid(string textboxValue) => textboxValue != "";

        private void CancelFormButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TaskRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            EnableOrDisableComponents(true);
            RecipientTextBox.IsEnabled = false;
            SubmitFormButton.IsEnabled = true;
        }

        private void AppointmentRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            EnableOrDisableComponents(true);
            RecipientTextBox.IsEnabled = true;
            SubmitFormButton.IsEnabled = true;
        }

        private void EnableOrDisableComponents(bool value)
        {
            NameTextBox.IsEnabled = value;
            ContactTextBox.IsEnabled = value;
            DateTimePicker.IsEnabled = value;
            LocationTextBox.IsEnabled = value;
            SubmitFormButton.IsEnabled = value;
        }

        private void EventTypeCheckBox_OnChanged(object sender, RoutedEventArgs e)
        {
            bool isChecked = EventTypeCheckBox.IsChecked ?? false;

            if (isChecked)
                EventTypeCheckBox.Content = "Recurring (uncheck box to select one-off)";
            else
                EventTypeCheckBox.Content = "One-Off (check box to select recurring)";
        }
    }
}

