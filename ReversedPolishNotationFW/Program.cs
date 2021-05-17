using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace ReversedPolishNotationFW { 

    class Program
    {
        static void Main(string[] args)
        {
            
            string expression = GetExpression(out double start, out double  step, out double end);
            
            var rpn = new RPN();
            Stack<object> stackForCalculate = rpn.Reverse(expression, out string strRPN);
            var calculator = new Calculator(start, step, end);
            Dictionary<double, double> answer = calculator.GetAnswer(stackForCalculate);
            ConsoleWriter.OutData(expression, strRPN, answer);

        }
        private static string GetExpression (out double start, out double step, out double end)
        {
            start = 0;
            step = 0;
            end = 0;
            string[] file = File.ReadAllLines("input.txt");
            if (file.Length == 4)
            {
               if (!(double.TryParse(file[1], out start) 
                    && (double.TryParse(file[2], out step)) 
                    && (double.TryParse(file[3], out end))))
               {
                    throw new Exception("в строках 2-4 нельзя преобразовать числа");
               }
            }
            if (file.Length != 1 && file.Length != 4)
            {
                throw new Exception("Неправильный ввод данных");
            }
            return file[0];
        }
       

    }
}
