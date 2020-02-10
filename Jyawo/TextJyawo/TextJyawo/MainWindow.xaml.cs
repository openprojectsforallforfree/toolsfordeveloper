using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TextJyawo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnReplace_Click(object sender, RoutedEventArgs e)
        {
            var find = txtFind.Text;
            var replace = txtReplace.Text;
            var input = txtInput.Text;
            var delimiter = txtDelimiter.Text;
            var output = string.Empty;
            var temp = string.Empty;

            var findS = find.Split(new string[] { delimiter }, StringSplitOptions.None);
            string[] lines = replace.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            if (findS.Length < 1)
            {
                return;
            }
            foreach (var item in lines)
            {
                if (item.Trim().Length > 0)
                {
                    var lstitemS = item.Trim().Split(new string[] { delimiter }, StringSplitOptions.None);
                    temp = input;
                    var loop = lstitemS.Length;
                    if (findS.Length < loop)
                    {
                        loop = findS.Length;
                    }
                    for (int i = 0; i < loop; i++)
                    {
                        if (findS[i].Length < 1)
                        {
                            continue;
                        }
                        temp = temp.Replace(findS[i], lstitemS[i]);
                    }
                    output += temp + Environment.NewLine + Environment.NewLine;
                }
            }
            if (chkCopyToClipBoard.IsChecked.Value)
            {
                Clipboard.SetText(output);
            }
           
            txtOutput.Text = output;
        }

        

        private void txtReplace_KeyUp(object sender, KeyEventArgs e)
        {
            btnReplace_Click(null, null);
        }

        private void txtInput_KeyUp(object sender, KeyEventArgs e)
        {
            btnReplace_Click(null, null);
        }

        private void txtFind_KeyUp(object sender, KeyEventArgs e)
        {
            btnReplace_Click(null, null);
        }
    }
}
