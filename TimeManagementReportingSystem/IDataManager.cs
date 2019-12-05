using System;
using System.Collections.Generic;
using System.Text;

namespace TimeManagementReportingSystem
{
    public interface IDataManager
    {
        void Read(string fileName);
        void Write(Event eventToAdd, string fileName);
    }
}
