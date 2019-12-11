using System;
using System.Collections.Generic;

namespace TimeManagementReportingSystem
{
    public class AddEvent_Model
    {
        private IDataManager manager;

        public AddEvent_Model(IDataManager manager)
        {
            this.manager = manager;
        }

        public void AddEvent(Event eventToAdd)
        {
            // Add it to events list
            EventsDataHandler.GetInstance().events.Add(eventToAdd);

            // Then we write to file
            manager.Write(eventToAdd);
        }
    }
}
