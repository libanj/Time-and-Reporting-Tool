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
    /// Interaction logic for HomepageForm.xaml
    /// </summary>
    public partial class HomepageForm : Window
    {
        public HomepageForm()
        {
            InitializeComponent();
        }

        private void EventForm_Button_Click(object sender, RoutedEventArgs e)
        {
            EventForm eventForm = new EventForm();
            eventForm.ShowDialog();
            eventForm.Focus();
        }

        private void PredictionForm_Button_Click(object sender, RoutedEventArgs e)
        {
            // call prediction xaml page
            PredictionForm predictionForm = new PredictionForm();
            predictionForm.ShowDialog();
            predictionForm.Focus();
            // create prediction model class where i read the xml data
            // capture duration tag and eventType tag.
        }
    }
}
