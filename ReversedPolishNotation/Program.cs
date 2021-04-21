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
            string expression = File.ReadAllText("input.txt");
            double answer = 0;
            var rpn = new RPN();
            string output = rpn.Reverse(expression, ref answer);
            Console.WriteLine(output);
            Console.Read();

        }
    }
}
