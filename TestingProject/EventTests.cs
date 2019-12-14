using System;
using System.Collections.Generic;
using NUnit.Framework;
using TimeManagementReportingSystem;

namespace TestingProject
{
    [TestFixture]
    public class EventTests
    {
        [Test]
        public void Test_That_Event_Is_Created()
        {
            Task task = new Task("Liban", "liban1808@gmail.com", "14/12/2019", "10:20", "London", 100);
            Assert.IsNotNull(task);
        }
    }
}
