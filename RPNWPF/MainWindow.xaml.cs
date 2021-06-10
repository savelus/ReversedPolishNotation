﻿using System;
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
using RPNLogic;

namespace testWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> Clicks = new List<string>();
        public string TextExpression { get; private set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string LastClick = (string)((Button)sender).Content;
            Clicks.Add(LastClick);
            CreateTextExpression();
        }
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            if (Clicks.Count !=0)
            {
                Clicks.RemoveAt(Clicks.Count - 1);
            }
            CreateTextExpression();
        }
        private void AllClearButton_Click(object sender, RoutedEventArgs e)
        {
            Clicks.Clear();
            CreateTextExpression();
        }
        
        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            var answer = new Dictionary<double, double>();
            var rpn = new RPN();
            if (TextExpression.Contains('x'))
            {

            }
            else
            {
                answer = rpn.GetAnswer(TextExpression, out string strRPN, 0, 0, 0);
                TextExpression += $" = {answer[0]}";
                TBExpression.Text = TextExpression;
            }
        }

        private void CreateTextExpression()
        {
            TextExpression = string.Join("", Clicks);
            TBExpression.Text = TextExpression;
        }
    }
}
