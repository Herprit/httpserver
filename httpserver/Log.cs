using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using httpserver;

namespace EventLogExample
{
    /*
     * Adapted from http://support.microsoft.com/kb/307024
     */
   public class Log
    {
        const string Source = "HttpServer Online";
        const string sLog = "Application";
        const string Message = "Sample Event blah";

        public void Logging()
        {
            if (!EventLog.SourceExists(Source))
            {
                EventLog.CreateEventSource(Source, sLog);
            }

            //WriteToLogUsingStaticMethods();
            WriteToLogUsingObjectMethods();
        }

        private static void WriteToLogUsingStaticMethods()
        {
            EventLog.WriteEntry(Source, Message);
            int eventId = 234;
            EventLog.WriteEntry(Source, Message, EventLogEntryType.Warning, eventId);
        }

        private static void WriteToLogUsingObjectMethods()
        {
            string machineName = "."; // this computer
            using (EventLog log = new EventLog(sLog, machineName, Source))
            {
                log.WriteEntry("Server Startet");
                //log.WriteEntry("Hello again", EventLogEntryType.Information);
                //log.WriteEntry("Hello again again", EventLogEntryType.Information, 14593);
            }
        }
    }
}
