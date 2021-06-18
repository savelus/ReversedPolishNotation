using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RPNWPF
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Graph : Window
    {
        FunctionDrawer drawer;
        public Graph(string function)
        {

            InitializeComponent();
            drawer = new FunctionDrawer(function, MainCanvas);
            
        }
        public void Setpoints()
        {
            MainCanvas.Children.Clear();
            drawer.Drawfunction(5);
        }

        private void MainCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            MainCanvas.Width = this.Width;
            MainCanvas.Height = this.Height;
            Setpoints();
        }
    }
}
