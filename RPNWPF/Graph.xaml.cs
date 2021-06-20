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
    public partial class Graph : Window
    {
        private Point delta = new Point(0, 0);
        private double zoom = 1;
        FunctionDrawer drawer;
        
        public Graph(string function)
        {

            InitializeComponent();
            drawer = new FunctionDrawer(function, MainCanvas);
            
        }
        private void Draw()
        {
            MainCanvas.Children.Clear();
            drawer.Drawfunction(zoom, delta);
        }

        private void MainCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            MainCanvas.Width = this.Width;
            MainCanvas.Height = this.Height - 150;
            Draw();
        }

        private void MainCanvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (zoom > 200) { }
                if (zoom < 2)
                    zoom += 0.1;
                else
                    zoom +=1;
            }
            else
            {
                if (zoom > 2)
                    zoom -= 1;
                else if (zoom > 0.1)
                    zoom -= 0.1;
            }
            Zoom.Text = Math.Round(zoom, 2) .ToString();
            Draw();
        }

        private void MainCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            delta.X = 0;
            delta.Y = 0;
            switch (e.Key)
            {
                case Key.Up:
                    delta.Y = -5;

                    break;
                case Key.Down:
                    delta.Y = 5;
                    break;
                case Key.Left:
                    delta.X = 5;
                    break;
                case Key.Right:
                    delta.X = -5;
                    break;
                default:
                    break;
            }
            Draw();
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(MainCanvas);
            Point origin = drawer.GetPoint();
            Xcoord.Text = Math.Round((mousePos.X - origin.X) / zoom, 3).ToString();
            Ycoord.Text = Math.Round((origin.Y - mousePos.Y) / zoom, 3).ToString();
        }
    }
}
