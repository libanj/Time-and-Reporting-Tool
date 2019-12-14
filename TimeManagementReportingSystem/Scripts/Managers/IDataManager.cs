using System;
using System.Collections.Generic;
using System.Text;

namespace TimeManagementReportingSystem
{
    public interface IDataManager
    {
        void Read();
        void Write(Event eventToAdd);
    }
}
