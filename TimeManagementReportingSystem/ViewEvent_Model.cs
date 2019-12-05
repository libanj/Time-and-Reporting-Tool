using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TimeManagementReportingSystem
{
    public class ViewEvent_Model
    {
        public List<Event> GetEventsFromDatesSpecified(string startDate, string endDate)
        {
            List<Event> eventsToAdd = new List<Event>();

            foreach (var _event in EventsDataHandler.GetInstance().events)
            {
                Trace.WriteLine(_event.Date);
                DateTime fromRealDate = Convert.ToDateTime(startDate);
                DateTime toRealDate = Convert.ToDateTime(endDate);
                DateTime currentRealEventDate = Convert.ToDateTime(_event.Date);

                if (currentRealEventDate.Year == fromRealDate.Year && currentRealEventDate.Year == toRealDate.Year &&
                    currentRealEventDate.Month >= fromRealDate.Month && currentRealEventDate.Month <= toRealDate.Month
                    && currentRealEventDate.Day >= fromRealDate.Day && currentRealEventDate.Day <= toRealDate.Day)
                {
                    eventsToAdd.Add(_event);
                }
            }

            return eventsToAdd;
        }
    }
}
