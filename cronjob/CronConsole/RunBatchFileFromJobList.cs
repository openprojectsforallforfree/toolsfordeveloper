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

namespace CronConsole
{
    public class RunBatchFileFromJobList
    {
        private static readonly CronDaemon cron_daemon = new CronDaemon();
        public static void RunBatchFileFormJob()
        {
            try
            {
                LogTrace.WriteErrorLog("______Started_________");
                var jobs = System.IO.File.ReadAllLines(Path.Combine(Application.StartupPath, "jobs.txt"));
                foreach (var job in jobs)
                {
                    var c = job.Split(new string[] { " " }, StringSplitOptions.None);
                    if (c.Length > 4)
                    {
                        var cronNotation = string.Join(" ", c.Take(5).ToArray());
                        var file = string.Join(" ", c.Skip(5).ToArray());
                        ThreadStart t = new ThreadStart(() => Execute(file));
                        //string cronNotation = "0 16 * * *";// "* * * * *";
                        LogTrace.WriteInfoLog("Run :" + file + " at ");
                        LogTrace.WriteInfoLog(ExpressionDescriptor.GetDescription(cronNotation));
                        cron_daemon.AddJob(cronNotation, t);
                        try
                        {
                            cron_daemon.Start();
                        }
                        catch (Exception ex)
                        {
                            LogTrace.WriteErrorLog(ex.Message);
                        }
                    }
                    else
                    {
                        LogTrace.WriteErrorLog("Ignoring : " + job);
                    }
                }
            }
            catch (Exception ex)
            {
                LogTrace.WriteErrorLog(ex.Message);
            }
        }

        static void Execute(string filename)
        {
            LogTrace.WriteInfoLog("Running:" + filename);
            Process.Start(filename);
        }

    }
}
