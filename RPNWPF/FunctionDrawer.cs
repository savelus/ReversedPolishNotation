using RPNLogic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using Point = System.Windows.Point;

namespace RPNWPF
{

    class FunctionDrawer
    {
        public string Function { get; }
        private Canvas canvas;
        public FunctionDrawer(string func, Canvas canv)
        {
            Function = func;
            canvas = canv;
        }

        private Brush lineColor = Brushes.Black;
        private Brush axisColor = Brushes.Black;
        private Brush pointColor = Brushes.Red;

        public void Drawfunction(double scale)
        {
            var rpn = new RPN();
            Dictionary<double, double> answerRPN = rpn.GetAnswer(Function, out string exp, (-canvas.Width / 2), scale, (canvas.Width / 2));
            Point? startPoint = null;
            for (double i = -canvas.Width / 2; i < canvas.Width / 2; i += scale)
            {
                if (answerRPN.ContainsKey(i))
                {
                    if (startPoint != null)
                    {
                        var line = new Line();
                        line.Stroke = lineColor;
                        line.StrokeThickness = scale;
                        line.X1 = startPoint.Value.X;
                        line.Y1 = startPoint.Value.Y;
                        line.X2 = i + canvas.Width / 2;
                        line.Y2 = -answerRPN[i] + canvas.Height / 2;
                        canvas.Children.Insert(0, line);
                    }
                    
                    var ellipse = new Ellipse();
                    ellipse.Width = scale;
                    ellipse.Height = scale;
                    ellipse.Fill = pointColor;
                    Canvas.SetLeft(ellipse, i + canvas.Width / 2 - scale /2);
                    Canvas.SetTop(ellipse, -answerRPN[i] + canvas.Height / 2 - scale / 2);
                    startPoint = new Point(i + canvas.Width / 2, -answerRPN[i] + canvas.Height / 2);
                    canvas.Children.Insert(0, ellipse);
                }
                else
                {
                    startPoint = null;
                }
            }
        }
    }
}
