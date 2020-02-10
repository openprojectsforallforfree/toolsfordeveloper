using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CronConsole
{
  public  class Utility
    {
        #region Hide Show
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        #endregion
        public static void ShowMe(bool show)
        {
            var handle = GetConsoleWindow();
            if (show)
            {
                ShowWindow(handle, SW_SHOW);
            }
            else
            {
                ShowWindow(handle, SW_HIDE);
            }
        }

        

    }
}
