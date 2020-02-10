using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace QuotesDisplay
{
    public partial class QuoteOfDay : Form
    {
        public QuoteOfDay()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.TopMost = Properties.Settings.Default.OnTop;

            System.Timers.Timer aTimer = new System.Timers.Timer();
            int interval =   Properties.Settings.Default.IntervalSeconds ;
            aTimer.Interval = interval * 1000;
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
           
            aTimer.Enabled = true;

            string curDir = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            this.webBrowser1.Url = new Uri(String.Format("file:///{0}/content.html", curDir));
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
    }
}
