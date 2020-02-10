using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bsoft.Common;
using CronConsole;
using CronExpressionDescriptor;
using CronNET;

namespace CronService
{
    public partial class CronService : ServiceBase
    {
        public CronService()
        {
            InitializeComponent();
        }
        private static readonly CronDaemon cron_daemon = new CronDaemon();
        protected override void OnStart(string[] args)
        {
            RunBatchFileFromJobList.RunBatchFileFormJob();
        }

        protected override void OnStop()
        {
            cron_daemon.Stop();
            LogTrace.WriteErrorLog("______Stopped_________");
        }

      
    }
}
