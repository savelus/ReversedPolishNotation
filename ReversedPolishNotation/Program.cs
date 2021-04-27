using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace ReversedPolishNotation
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string expression = GetExpression(out double start, out double  step, out double end);
            
            double answer = 0;
            var rpn = new RPN();
            string output = rpn.Reverse(expression, ref answer);
            
            Console.WriteLine(output);
            Console.Read();

        }
        private static string GetExpression (out double start, out double step, out double end)
        {
            start = 0;
            step = 0;
            end = 0;
            string[] file = File.ReadAllLines("input.txt");
            if (file.Length == 4) Calculator.ParseArgumentData(new string[] { file[1], file[2], file[3] }, ref start, ref step, ref end);
            else if (file.Length != 1 && file.Length != 4) throw new Exception("Данные введены неправильно.");
            return file[0];
        }
       

    }
}
