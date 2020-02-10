using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for AddDynamically.xaml
    /// </summary>
    public partial class AddDynamically : Window
    {
        public AddDynamically()
        {
            InitializeComponent();
        }
    }
    public class SearchEntryViewmodel
    {
        //Properties for Binding to Combobox and Textbox goes here
    }

    public class SearchViewModel
    {
        public ObservableCollection<SearchEntryViewmodel> MySearchItems { get; set; }
        public ICommand AddSearchItem { get; }
    }
}
