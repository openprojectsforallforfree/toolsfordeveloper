using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CronExpressionDescriptor;
using CronNET;
using Bsoft.Common;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace CronConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            RunCron w = new RunCron();
            w.RunCronJob(Path.Combine(Application.StartupPath, "jobs.txt"));
        }
    }
}
