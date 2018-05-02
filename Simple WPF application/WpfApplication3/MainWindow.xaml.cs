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

namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // To set initial focus to first tab index
            Loaded += (sender, e) =>
                MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (panel.Visibility == System.Windows.Visibility.Visible) {
                panel.Visibility = System.Windows.Visibility.Hidden;
                buttonShowHide.Content = "Show";
            } else {
                panel.Visibility = System.Windows.Visibility.Visible;
                buttonShowHide.Content = "Hide";
            }
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Can't get rid of this without screwing up WPF App
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            // Check text boxes to see if they are empty or not
            if (lastName.Text == "") {
                lastName.Background = System.Windows.Media.Brushes.Pink;
                lastName.Focus();
            } else {
                lastName.Background = System.Windows.Media.Brushes.White;
            }

            if (firstName.Text == "") {
                firstName.Background = System.Windows.Media.Brushes.Pink;
                firstName.Focus();
            } else {
                firstName.Background = System.Windows.Media.Brushes.White;
            }



            // If both are not empty, go ahead and show popup window
            if (firstName.Text != "" && lastName.Text != "")
            {
                // Check names in popup window
                Window1 wind = new WpfApplication3.Window1(firstName.Text, lastName.Text);
                wind.Closing += changeButton;
                wind.ShowDialog();
            }
        }

        // Changes button after popup window closes
        private void changeButton(object sender2, EventArgs e2) {
            this.button1.Content = "Submit Name";
            this.button1.Click -= button1_Click;
            this.button1.Click += submitName;
            // And disables firstname and lastname from being changed any more
            firstName.IsReadOnly = true;
            lastName.IsReadOnly = true;
        }

        // Submits name (only closes window for this assignment)
        private void submitName(object sender, RoutedEventArgs e) {
            this.Close();
        }

        // Set first name
        public void setFirstName(string name)
        {
            firstName.Text = name;
        }

        // Set last name
        public void setLastName(string name) {
            lastName.Text = name;
        }

    }
}

// Different button to kill app, after close popup window
// Move cursor to the empty text box
