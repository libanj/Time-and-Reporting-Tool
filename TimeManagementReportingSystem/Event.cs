using System;
using System.Collections.Generic;

namespace TimeManagementReportingSystem
{
    public enum EventType
    {
        ONE_OFF, RECURRING, MAX
    }

    public abstract class Event
    {
        protected string name;
        protected string contact;
        protected string date;
        protected string location;
        protected int timeUsage;
        protected EventType eventType = EventType.ONE_OFF;

        public string Name => name; // create public method for other classes to access the protected variables
        public string Contact => contact;
        public string Date => date;
        public string Location => location;
        public int TimeUsage => timeUsage;

        public EventType EventType { get { return eventType; } set { eventType = value; } }

        public Event(string name, string contact, string date, string location, int timeUsage)
        {
            this.name = name;
            this.contact = contact;
            this.date = date;
            this.location = location;
            this.timeUsage = timeUsage;
        }

        public override string ToString()
        {
            return $"Name: {name}, Contact: {contact}, DateTime: {date}, Location: {location}, Duration: {timeUsage}, Event Type: {eventType}";
        }
    }
}
