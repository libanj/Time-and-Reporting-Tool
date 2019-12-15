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

        [Test]
        [TestCase("10:50", true)]
        [TestCase("01:60", false)]
        [TestCase("22:00", true)]
        [TestCase("24:00", false)]
        public void Test_That_Event_The_Time_Is_Valid(string time, bool expected)
        {
            Task task = new Task("Liban", "liban1808@gmail.com", "14/12/2019", time, "London", 100);

            IStringValidator timeValidator = new TimeStringValidator();

            bool actual = timeValidator.IsValid(task.Time);
            Assert.AreEqual(expected, actual);
        }
    }
}
