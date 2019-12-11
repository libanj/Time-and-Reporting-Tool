using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for PredictionForm.xaml
    /// </summary>
    public partial class PredictionForm : Window
    {

        public PredictionForm()
        {
            InitializeComponent();
            new XMLManager().Read();
            Trace.WriteLine($"Prediction {GetPredictionFourWeeks()}");
            Trace.WriteLine($"AllEventsDuration {GetAllEventsDurationTime()}");
            PredictionLabel.Content = PrintPrediction();
        }

        private int GetAllEventsDurationTime()
        {
            int totalDuration = 0;
            foreach (var _event in EventsDataHandler.GetInstance().events)
            {
                if (_event.EventType == EventType.RECURRING)
                {
                    totalDuration += Convert.ToInt32(_event.TimeUsage) * 4;
                }
                else
                {
                    totalDuration += Convert.ToInt32(_event.TimeUsage);
                }
            }
            return totalDuration;
        }
        private int GetPredictionFourWeeks() => EventsDataHandler.GetInstance().events.Count > 0 ? GetAllEventsDurationTime() / EventsDataHandler.GetInstance().events.Count : 0;
        private string PrintPrediction() => $"Time {Math.DivRem(GetPredictionFourWeeks(), 60, out int minutes)}hh:{minutes}mm";
        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
