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
using System.Diagnostics;


namespace TimeManagementReportingSystem
{
    /// <summary>
    /// Interaction logic for ViewEventForm.xaml
    /// </summary>
    public partial class ViewEventForm : Window
    {
        private IDataManager datamanager;

        public ViewEventForm(IDataManager datamanager)
        {
            this.datamanager = datamanager;
            EventsDataHandler.GetInstance().events.Clear();
            datamanager.Read();
            InitializeComponent();
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Show_Button_Click(object sender, RoutedEventArgs e)
        {
            // Choose a start and end date
            String startDate = StartDatePicker.SelectedDate.ToString();
            String endDate = EndDatePicker.SelectedDate.ToString();

            // Create an instance of the view event model
            ViewEvent_Model viewEventModel = new ViewEvent_Model();

            // Create a list of events that get the specified dates from the model
            List<Event> eventsToDisplay = viewEventModel.GetEventsFromDatesSpecified(startDate, endDate);

            List<string> eventsData = new List<string>();

            for (int i = 0; i < eventsToDisplay.Count; i++)
            {
                string eventData = eventsToDisplay[i].ToString();
                eventsData.Add(eventData);
            }

            ViewDateListBox.ItemsSource = eventsData;
        }
    }
}
