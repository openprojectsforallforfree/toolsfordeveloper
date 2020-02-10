using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CronNET;
namespace test
{
    class Program
    {

        private static readonly CronDaemon cron_daemon = new CronDaemon();            

        static void Main(string[] args)
        {
            ThreadStart t = new ThreadStart(() => Execute(@"D:\a.bat"));

            cron_daemon.AddJob("* * * * *", t);
            cron_daemon.Start();
            // Wait and sleep forever. Let the cron daemon run.
            while(true) Thread.Sleep(6000);
        }

        static void Execute(string filename)
        {
            Process.Start(filename);
        }
    }
}
