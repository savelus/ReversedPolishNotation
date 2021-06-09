using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using RPNLogic;

namespace ReversedPolishNotation
{

    class Program
    {
        static void Main(string[] args)
        {

            string expression = GetExpression(out double start, out double step, out double end);

            var rpn = new RPN();
            var answerDictionary = rpn.GetAnswer(expression, out string strRPN, start, step, end);
            ConsoleWriter.OutData(expression, strRPN, answerDictionary);
        }
        private static string GetExpression(out double start, out double step, out double end)
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
