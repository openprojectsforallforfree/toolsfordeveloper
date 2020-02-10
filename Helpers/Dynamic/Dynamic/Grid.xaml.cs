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
using System.Windows.Shapes;

namespace Dynamic
{
    /// <summary>
    /// Interaction logic for Grid.xaml
    /// </summary>
    public partial class Grid : Window
    {
        List<checkedBoxIte> item = new List<checkedBoxIte>();
        public Grid()
        {
            InitializeComponent();
            for (int i = 0; i < 5; i++)
            {
                checkedBoxIte ite = new checkedBoxIte();
                ite.MyString = i.ToString();
                //ite.MyBool = true;
                item.Add(ite);
            }
          //dataGrid1.ItemsSource = item;
        }


    }

    public class checkedBoxIte
    {
        public string MyString { get; set; }
        public bool MyBool { get; set; }
    }
}
