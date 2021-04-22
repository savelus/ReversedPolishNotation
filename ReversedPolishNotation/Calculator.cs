using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversedPolishNotation
{
    class Calculator
    {
        public static void ParseArgumentData(string[] numbers, ref double start, ref double step, ref double end)
        {

            if (!(double.TryParse(numbers[0], out start) && double.TryParse(numbers[0], out step) && double.TryParse(numbers[0], out end)))
                throw new Exception("Данные для аргумента введены не правильно");

        }
    }
}
