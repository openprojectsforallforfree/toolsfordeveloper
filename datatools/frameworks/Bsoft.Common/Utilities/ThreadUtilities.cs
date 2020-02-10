using System.Windows.Forms;

namespace Bsoft.Threading
{
    public static class ThreadUtilities
    {
        private delegate void SetTextCallback(Form f, Control ctrl, string text);

        private delegate void SetVisibleCallback(Form f, Control ctrl, bool visible);

        private delegate void SetProgressCallback(Form f, ProgressBar ctrl,  int max, int current);

        private delegate void SetCloseApplications(Form f);

        /// <summary>
        /// Set text property of various controls
        /// </summary>
        /// <param name="form">The calling form</param>
        /// <param name="ctrl"></param>
        /// <param name="text"></param>
        public static void SetText(Form form, Control ctrl, string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (ctrl.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                form.Invoke(d, new object[] { form, ctrl, text });
            }
            else
            {
                ctrl.Text = text;
            }
        }

        public static void SetVisible(Form form, Control ctrl, bool visible)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (ctrl.InvokeRequired)
            {
                SetVisibleCallback d = new SetVisibleCallback(SetVisible);
                form.Invoke(d, new object[] { form, ctrl, visible });
            }
            else
            {
                ctrl.Visible = visible;
            }
        }

        public static void SetProgress(Form form, ProgressBar progressBar1, int Maximum, int current)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (progressBar1.InvokeRequired)
            {
                SetProgressCallback d = new SetProgressCallback(SetProgress);
                form.Invoke(d, new object[] { form, progressBar1,  Maximum, current });
            }
            else
            {
                progressBar1.Maximum = Maximum;
                if (progressBar1.Value + current <= progressBar1.Maximum)
                {
                    progressBar1.Value += current;
                }
                else
                {
                    progressBar1.Value = progressBar1.Maximum;
                }
                progressBar1.Refresh();
            }
        }

        public static void CloseApplications(Form form)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (form.InvokeRequired)
            {
                SetCloseApplications d = new SetCloseApplications(CloseApplications);
                form.Invoke(d, new object[] { form });
            }
            else
            {
                form.Close();
            }
        }
    }
}