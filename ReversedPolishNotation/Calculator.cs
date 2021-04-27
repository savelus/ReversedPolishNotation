using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversedPolishNotation
{
    class Calculator
    {
        public double Start { get; private set; }
        public double Step { get; private set; }
        public double End { get; private set; }

        public Calculator(double start, double step, double end)
        {
            if (start != end && end == 0)
                throw new Exception("Шаг функции не может быть равен 0");
            
            Start = start;
            Step = step;
            End = end;
        }
        public Dictionary<double, double> GetAnswer(Stack<object> RPN)
        {

            var answerDictionary = new Dictionary<double, double>();
            if (Step == 0 || (Start + Step > End))
            {
                answerDictionary.Add(Start, Calculate(RPN, Start));
            }
            else
            {
                for ( ; Start <= End; Start += Step)
                {
                    answerDictionary.Add(Start, Calculate(RPN, Start));
                }
            }
            return answerDictionary;
        }
        private double Calculate(Stack<object> RPN, double argument)
        {
            var tempRPN = new Stack<object>(RPN);
            Stack<double> numbers = new Stack<double>();
            while (tempRPN.Count != 0)
            {
                if (tempRPN.Peek() is double)
                {
                    numbers.Push((double)tempRPN.Pop());
                }
                else if (tempRPN.Peek() is Argument)
                {
                    numbers.Push(argument);
                    tempRPN.Pop();
                }
                else
                {
                    Operation operation = tempRPN.Pop() as Operation;
                    if (operation.CountParams == 1)
                    {
                        double[] @params = { numbers.Pop() };
                        numbers.Push(operation.Calculate(@params));
                    }
                    else if (operation.CountParams == 2)
                    {
                        double[] @params = { numbers.Pop(), numbers.Pop() };
                        numbers.Push(operation.Calculate(@params));
                    }
                }
            }
            return (double)numbers.Peek();
        }
    }
}
