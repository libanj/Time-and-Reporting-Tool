using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;

namespace TimeManagementReportingSystem
{
    public class XMLManager : IDataManager
    {
        private const string fileName = "\\events.xml";

        public void Read()
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            string filepath = currentDirectory + fileName;

            XmlDocument doc = new XmlDocument();
            doc.Load(filepath);

            foreach (XmlNode node in doc.SelectNodes("Events/Event"))
            {
                string name = node.SelectSingleNode("Name").InnerText;
                string contact = node.SelectSingleNode("Contact").InnerText;
                string location = node.SelectSingleNode("Location").InnerText;
                string date = node.SelectSingleNode("Date").InnerText;
                int timeUsage = Convert.ToInt32(node.SelectSingleNode("Duration").InnerText);
                EventType eventType = (EventType)Convert.ToInt32(node.SelectSingleNode("EventType").InnerText);

                if (node.SelectSingleNode("IsComplete") != null)
                {
                    bool isComplete = Convert.ToBoolean(Convert.ToInt32(node.SelectSingleNode("IsComplete").InnerText));

                    Task taskToAddFromFile = new Task(name, contact, date, location, timeUsage, isComplete);

                    taskToAddFromFile.EventType = eventType;

                    EventsDataHandler.GetInstance().events.Add(taskToAddFromFile);
                }
                else if (node.SelectSingleNode("Recipient") != null)
                {
                    string recipient = node.SelectSingleNode("Recipient").InnerText;

                    Appointment appointmentToAddFromFile = new Appointment(name, contact, date, location, timeUsage, recipient);

                    appointmentToAddFromFile.EventType = eventType;

                    EventsDataHandler.GetInstance().events.Add(appointmentToAddFromFile);
                }
            }

            PrintAllEvents();
        }

        private void PrintAllEvents()
        {
            foreach (var eventInList in EventsDataHandler.GetInstance().events)
            {
                Trace.WriteLine(eventInList.ToString());
            }
        }

        public void Write(Event eventToAdd)
        {
            // Get current working directory
            string currentDirectory = Directory.GetCurrentDirectory();

            if (File.Exists(currentDirectory + fileName))
            {
                AppendData(fileName, eventToAdd);
            }
            else
            {
                SaveInitialData(eventToAdd);
            }
        }

        private void AppendData(string filename, Event eventToAdd)
        {
            // Get current working directory
            string currentDirectory = Directory.GetCurrentDirectory();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(currentDirectory + filename);

            // Create a node for the payer
            XmlNode subRootNote = xmlDoc.CreateElement("Event");

            // Name
            XmlNode nameNode = xmlDoc.CreateElement("Name");
            nameNode.InnerText = eventToAdd.Name;
            subRootNote.AppendChild(nameNode);

            // Contact 
            XmlNode contactNode = xmlDoc.CreateElement("Contact");
            contactNode.InnerText = eventToAdd.Name;
            subRootNote.AppendChild(contactNode);

            // DateTime
            XmlNode dateTimeNode = xmlDoc.CreateElement("Date");
            dateTimeNode.InnerText = eventToAdd.Date;
            subRootNote.AppendChild(dateTimeNode);

            // Location
            XmlNode locationTimeNode = xmlDoc.CreateElement("Location");
            locationTimeNode.InnerText = eventToAdd.Location;
            subRootNote.AppendChild(locationTimeNode);

            //check if appointment or task
            Type typeOfEvent = eventToAdd.GetType();
            XmlNode node = null;

            if (typeOfEvent == typeof(Task))
            {
                node = xmlDoc.CreateElement("IsComplete");
                node.InnerText = Convert.ToInt32(((Task)eventToAdd).IsComplete).ToString();
            }
            else if (typeOfEvent == typeof(Appointment))
            {
                node = xmlDoc.CreateElement("Recipient");
                node.InnerText = ((Appointment)eventToAdd).Recipient;
            }
            subRootNote.AppendChild(node);

            // Duration
            XmlNode durationNode = xmlDoc.CreateElement("Duration");
            durationNode.InnerText = eventToAdd.TimeUsage.ToString();
            subRootNote.AppendChild(durationNode);

            // Event Type
            XmlNode eventTypeNode = xmlDoc.CreateElement("EventType");
            eventTypeNode.InnerText = Convert.ToInt32(eventToAdd.EventType).ToString();
            subRootNote.AppendChild(eventTypeNode);

            xmlDoc.DocumentElement.AppendChild(subRootNote);
            xmlDoc.Save(currentDirectory + filename);
        }

        private void SaveInitialData(Event eventToAdd)
        {
            // Get current working directory
            string currentDirectory = Directory.GetCurrentDirectory();

            // Create a new file in the working directory
            XmlTextWriter textWriter = new XmlTextWriter(currentDirectory + fileName, Encoding.UTF8);

            try
            {
                CreatXMLContent(eventToAdd, textWriter);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
            finally
            {
                textWriter.Close();
            }
        }

        private void CreatXMLContent(Event eventToAdd, XmlTextWriter textWriter)
        {
            // format the xml file
            textWriter.Formatting = Formatting.Indented;

            // Opens the document 
            textWriter.WriteStartDocument();

            // Write the root element for all events
            textWriter.WriteStartElement("Events");

            // Write current event root element 
            textWriter.WriteStartElement("Event");

            // Name
            textWriter.WriteStartElement("Name");
            textWriter.WriteString(eventToAdd.Name);
            textWriter.WriteEndElement();

            // Contact
            textWriter.WriteStartElement("Contact");
            textWriter.WriteString(eventToAdd.Contact);
            textWriter.WriteEndElement();

            // DateTime
            textWriter.WriteStartElement("Date");
            textWriter.WriteString(eventToAdd.Date);
            textWriter.WriteEndElement();

            textWriter.WriteStartElement("Location");
            textWriter.WriteString(eventToAdd.Location);
            textWriter.WriteEndElement();

            textWriter.WriteStartElement("Duration");
            textWriter.WriteString(eventToAdd.TimeUsage.ToString());
            textWriter.WriteEndElement();

            textWriter.WriteStartElement("EventType");
            textWriter.WriteString(Convert.ToInt32(eventToAdd.EventType).ToString());
            textWriter.WriteEndElement();

            //check if appointment or task
            Type typeOfEvent = eventToAdd.GetType();

            if (typeOfEvent == typeof(Task))
            {
                textWriter.WriteStartElement("IsComplete");
                textWriter.WriteString(Convert.ToInt32(((Task)eventToAdd).IsComplete).ToString());
                textWriter.WriteEndElement();
            }
            else if (typeOfEvent == typeof(Appointment))
            {
                textWriter.WriteStartElement("Recipient");
                textWriter.WriteString((((Appointment)eventToAdd).Recipient.ToString()));
                textWriter.WriteEndElement();
            }

            textWriter.WriteEndElement(); // event
            textWriter.WriteEndElement(); //events

            // Ends the document. 
            textWriter.WriteEndDocument();
        }
    }
}
