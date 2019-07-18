using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace nsJPK_Generator
{
    static class Program
    {
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        static void Main()
        {
            if (!EventLog.SourceExists("MySource"))
            {
                EventLog.CreateEventSource("MySource", "MyNewLog");
                Console.WriteLine("CreatedEventSource");
                return;
            }
            // Create an EventLog instance and assign its source.
            EventLog myLog = new EventLog();
            myLog.Source = "MySource";

            // Write an informational entry to the event log.    
            myLog.WriteEntry("Writing to event log.");

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service1()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
