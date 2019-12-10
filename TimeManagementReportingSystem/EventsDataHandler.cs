using System;
using System.Collections.Generic;
using System.Text;

namespace TimeManagementReportingSystem
{
    public class EventsDataHandler
    {
        // Create a static instance variable as a Singleton
        private static EventsDataHandler instance;

        // This list is static because we want to make sure that every instance will still have the same list of events
        public readonly List<Event> events; // can access list however cannot change the list('readonly')

        private EventsDataHandler()
        {
            events = new List<Event>(); // create new "read-only" List
        }

        public static EventsDataHandler GetInstance()
        {
            // if we haven't already created an instance of this class, then we create an instance.
            if (instance == null)
                instance = new EventsDataHandler(); //create new instance and return it

            return instance;
        }
    }
}
