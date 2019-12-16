using System;

namespace TimeManagementReportingSystem
{
    public class TimeStringValidator : IStringValidator
    {
        public bool IsValid(string text)
        {
            try
            {
                string[] parts = text.Split(':');
                int hours = int.Parse(parts[0]);
                int minutes = int.Parse(parts[1]);
                return IsHourAndMinutesValid(hours, minutes);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return false;
        }

        private bool IsHourAndMinutesValid(int hours, int minutes)
        {
            return IsHoursValid(hours) && IsMinutesValid(minutes);
        }

        private bool IsHoursValid(int hours)
        {
            if (hours >= 0 && hours < 24)
                return true;

            return false;
        }

        private bool IsMinutesValid(int minutes)
        {
            if (minutes >= 0 && minutes < 60)
                return true;

            return false;
        }
    }
}
