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
    }
}
