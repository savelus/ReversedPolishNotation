using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversedPolishNotationFW
{
    public static class ConsoleWriter
    {
        public static void OutData (string example, string reversedLine, Dictionary<double, double> answers)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.CursorVisible = false;
            Console.Clear();
            Console.WriteLine($"Вы ввели: {example}");
            Console.WriteLine($"RPN выглядит так: {reversedLine}");
            if (answers.Count == 1)
            {
                Console.WriteLine($"{example} = {answers.Values.ToArray()[0]}");
            }
            else
            {
                WriteHead();
                foreach(var pair in answers)
                {
                    string X = pair.Key.ToString();
                    int lengthX = X.ToString().Length;
                    string answer = Math.Round(pair.Value, 4).ToString();
                    int lengthAnswer =answer.Length;
                    Console.WriteLine();
                    Console.WriteLine($"║{new string(' ', (5 - lengthX) / 2)}{X}{new string(' ', 5 - lengthX - (5 - lengthX) / 2)}║" +
                        $"{new string(' ', (10 - lengthAnswer) / 2)}{answer}{new string(' ', 10 - lengthAnswer - (10 - lengthAnswer) / 2)}║");
                    Console.Write($"╟{new string('─', 5)}╫{new string('─', 10)}╢");
                }
                Console.CursorLeft = 0;
                Console.WriteLine($"╚{new string('═', 5)}╩{new string('═', 10)}╝");
            }
            Console.Read();
        }
        private static void WriteHead()
        {
            Console.WriteLine($"╔{new string ('═', 5)}╦{new string ('═', 10)}╗");
            Console.WriteLine($"║  X  ║  Answer  ║");
            Console.Write($"╠{new string ('═', 5)}╬{new string ('═', 10)}╣");
        }
    }
}
