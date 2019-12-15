using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TimeManagementReportingSystem
{
    /// <summary>
    /// Interaction logic for EventForm.xaml
    /// </summary>
    public partial class EventForm : Window
    {
        public EventForm()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            AddEventForm addevent = new AddEventForm(new TimeStringValidator());
            addevent.ShowDialog();
            addevent.Focus();
        }

        private void View_Button_Click(object sender, RoutedEventArgs e)
        {
            ViewEventForm viewevents = new ViewEventForm(new XMLManager());
            viewevents.ShowDialog();
            viewevents.Focus();
        }
    }
}
