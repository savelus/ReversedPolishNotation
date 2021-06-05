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

namespace testWPF
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

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            TBAnswer.Text += ((Button)sender).Content;
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FunctionButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ArgumentButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BracketButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
