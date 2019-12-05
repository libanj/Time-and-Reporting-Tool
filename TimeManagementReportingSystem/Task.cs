using System;
using System.Collections.Generic;

namespace TimeManagementReportingSystem
{
    public class Task : Event
    {
        private bool isComplete;
        public bool IsComplete => isComplete;

        public Task(string name, string contact, string date, string location, int timeUsage, bool isComplete = false) : base(name, contact, date, location, timeUsage)
        {
            this.isComplete = isComplete;
        }

        public override string ToString()
        {
            string mainContent = base.ToString();

            return mainContent + $", Is Complete: {isComplete}.";
        }


    }
}
