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
using System.Linq;

namespace RPNWPF
{

    class FunctionDrawer
    {
        public string Function { get; }
        private Canvas canvas;
        private Point origin = new Point();
        public FunctionDrawer(string func, Canvas canv)
        {
            Function = func;
            canvas = canv;
            origin.X = canv.Width/2;
            origin.Y = canv.Height / 2;
        }
        private const int AxisThickness = 2;
        private Brush lineColor = Brushes.Red;
        private Brush axisColor = Brushes.Black;
        private Brush pointColor = Brushes.Red;

        public void Drawfunction(double scale)
        {
            DrawAxes();
            var rpn = new RPN();
            Point? startPoint = null;
            Dictionary<double, double> answerRPN = rpn.GetAnswer(Function, out _, -origin.X, 0.5, canvas.Width - origin.X);
            for (double i = -origin.X; i < canvas.Width - origin.X; i+=0.5)
            {
                if (answerRPN[i] < origin.Y || answerRPN[i] > canvas.Height)
                {
                    if (startPoint != null)
                    {
                        var startEndPoint = new PointCollection()
                        { 
                            startPoint.Value,
                            new Point(i + origin.X, -answerRPN[i] + origin.Y)
                        };
                        DrawLine(startEndPoint[0], startEndPoint[1], lineColor);
                    }
                    startPoint = new Point(i + origin.X, -answerRPN[i] + origin.Y);
                }
                else
                {
                    startPoint = null;
                }
            }
        }
        #region Draw Coordinate System
        private void DrawAxes ()
        {
            PointCollection OX = new PointCollection()
            {
                new Point(0, origin.Y),
                new Point(canvas.Width - 15 - 8, origin.Y)
            };
            PointCollection OY = new PointCollection()
            {
                new Point(origin.X, 8),
                new Point(origin.X, canvas.Height)
            };
            PointCollection OXArrows = new PointCollection()
            {
                new Point(canvas.Width - 15, origin.Y),
                new Point(canvas.Width - 15 - 8, origin.Y - 4),
                new Point(canvas.Width - 15 - 8, origin.Y + 4)
            };
            PointCollection OYArrows = new PointCollection()
            {
                new Point(origin.X, 0),
                new Point(origin.X - 4, 8),
                new Point(origin.X + 4, 8)
            };
            DrawLine(OX[0], OX[1], axisColor);
            DrawArrows(OXArrows);
            DrawLine(OY[0], OY[1], axisColor);
            DrawArrows(OYArrows);
        }

        private void DrawLine(Point start, Point end, Brush color)
        {
            var line = new Line();
            if (start.X < canvas.Width && start.Y < canvas.Height)
            {
                line.X1 = start.X;
                line.X2 = end.X;
                line.Y1 = start.Y;
                line.Y2 = end.Y;
                line.StrokeThickness = 2;
                line.Stroke = color;
                canvas.Children.Insert(0, line);
            }
        }

        private void DrawArrows(PointCollection tops)
        {
            Polygon polygon = new Polygon();
            polygon.Points = tops;
            polygon.Fill = axisColor;
            canvas.Children.Insert(0, polygon);
        }
        #endregion
    }
}
