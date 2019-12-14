using System;
using System.Collections.Generic;

namespace TimeManagementReportingSystem
{
    public class Appointment : Event
    {
        private string recipient;

        public string Recipient => recipient;

        public Appointment(string name, string contact, string date, string time, string location, int timeUsage, string recipient) : base(name, contact, date, time, location, timeUsage)
        {
            this.recipient = recipient;
        }

        public override string ToString()
        {
            string mainContent = base.ToString();

            return mainContent + $", Recipient: {recipient}.";
        }
    }
}
