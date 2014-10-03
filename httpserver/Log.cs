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


        //WriteInfo Log! string message gør det muligt jeg selv kan skrive info om loggen.
        public static void WriteInfo(string message)
        {
            if (!EventLog.SourceExists(Source))
            {
                EventLog.CreateEventSource(Source, sLog);
            }

            string machineName = "."; // this computer

            using (EventLog log = new EventLog(sLog, machineName, Source))
            {

                log.WriteEntry(message, EventLogEntryType.Information);

            }

        }
    }
}
