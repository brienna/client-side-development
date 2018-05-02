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

namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        public Window1(String text1, String text2) { 
        
            InitializeComponent();
            fName.Text = text1;
            lName.Text = text2;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).setFirstName(GetFirstName());
            ((MainWindow)Application.Current.MainWindow).setLastName(GetLastName());
            this.Close();
            //https://stackoverflow.com/questions/14433935/passing-data-between-wpf-forms
        }

        public string GetFirstName()
        {
            return fName.Text;
        }

        public string GetLastName()
        {
            return lName.Text;
        }

    }
}
