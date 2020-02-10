using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bsoft.Common;
using CronExpressionDescriptor;
using CronNET;
using Microsoft.Win32;

namespace CronConsole
{
    public class RunCron
    {
        private CronDaemon cron_daemon = new CronDaemon();
        public RunCron()
        {
            Microsoft.Win32.SystemEvents.SessionSwitch += new Microsoft.Win32.SessionSwitchEventHandler(SystemEvents_SessionSwitch);
        }

        void SystemEvents_SessionSwitch(object sender, Microsoft.Win32.SessionSwitchEventArgs e)
        {
            if (e.Reason == SessionSwitchReason.SessionLock)
            {
                LogTrace.WriteInfoLog(string.Format("locked"));
                cron_daemon.Stop();
            }
            else if (e.Reason == SessionSwitchReason.SessionUnlock)
            {
                LogTrace.WriteInfoLog(string.Format("unlocked"));
                cron_daemon.Start();
            }
        }


        void Execute(string filename)
        {
            LogTrace.WriteInfoLog(string.Format("Started {0}", filename));
            Process.Start(filename);
        }

        public void RunCronJob(string jobFile)
        {
            var jobs = System.IO.File.ReadAllLines(jobFile);
            foreach (var job in jobs)
            {
                if (job.Trim().Length > 0 && !job.StartsWith("--"))
                {
                    try
                    {
                        var c = job.Split(new string[] { " " }, StringSplitOptions.None);
                        var cronNotation = string.Join(" ", c.Take(5).ToArray());
                        var file = string.Join(" ", c.Skip(5).ToArray());
                        ThreadStart t = new ThreadStart(() => Execute(file));
                        //string cronNotation = "0 16 * * *";// "* * * * *";
                        string log = string.Format("Run {0} {1}", file, ExpressionDescriptor.GetDescription(cronNotation));
                        LogTrace.WriteInfoLog(log);
                        Console.WriteLine(log);
                        cron_daemon.AddJob(cronNotation, t);
                        cron_daemon.Start();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(job + ":" + ex.Message);
                    }
                }
            }
            Console.WriteLine("Continue ? (Y)");
            string cont = Console.ReadLine();
            if (cont.Length == 0 || cont.ToUpper() == "Y")
            {
                Console.WriteLine("Hide Window ? (Y)");
                string hide = Console.ReadLine();
                if (hide.Length == 0 || hide.ToUpper() == "Y")
                {
                    Utility.ShowMe(false);
                }
                Console.WriteLine("Started Cron...");
                while (true) Thread.Sleep(6000);
            }
            else
            {
                Application.Exit();
            }
        }

    }
}
