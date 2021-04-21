using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading.Tasks;

namespace ReversedPolishNotation
{
    public class RPN
    {
        public string Reverse (string expression, ref double answer)
        {
            expression = expression.Replace(" ", "");
            List<object> parsedLine = Parse(expression);
            CheckLine(parsedLine);//выбрасывать ошибку
            var reversedLine = GetReversedLine(parsedLine);
            return StringReversedLine(reversedLine.ToArray());
        } 
        
        private List<object> Parse(string expression)
        {
            List<object> parsedLine = new List<object>();

            string tempNum = ""; 
            for (int i = 0; i < expression.Length; i++)
            {
                if ("+-*/".Contains(expression[i]))
                {
                    parsedLine = ParseNumbers(ref tempNum, parsedLine);
                    parsedLine.Add(ChooseOperation(expression[i]));
                }
                else if ("()".Contains(expression[i]))
                {
                    parsedLine = ParseNumbers(ref tempNum, parsedLine);
                    parsedLine.Add(ChooseBracket(expression[i]));
                }
                else if (IsNumbers(expression[i]) || (".,".Contains(expression[i]) && IsNumbers(expression[i - 1]) && IsNumbers(expression[i + 1])))
                {
                    tempNum += expression[i].ToString();
                }
            }
            parsedLine = ParseNumbers(ref tempNum, parsedLine);
            return parsedLine;
        }
        private List<object> ParseNumbers (ref string tempNum, List<object> parsedLine)
        {
            if (tempNum != "")
            {
                double number = double.Parse(tempNum, CultureInfo.InvariantCulture); //7,5 => 75  7.5 => 7.5
                parsedLine.Add(number);
                tempNum = "";
            }
            return parsedLine;
        }
        private bool IsNumbers (char sym)
        {         
            return (sym >= '0' && sym <= '9');
        }
        private Operation ChooseOperation(char op)
        {
            Operation operation = null;
            switch (op)
            {
                case ('+'):
                    operation = new Plus();
                    break;
                case ('-'):
                    operation = new Minus();
                    break;
                case ('*'):
                    operation = new Mult();
                    break;
                case ('/'):
                    operation = new Div();
                    break;
            }
            return operation;
        }
        private Bracket ChooseBracket(char br)
        {
            Bracket bracket = null;
            switch (br)
            {
                case ('('):
                    bracket = new OpenBracket();
                    break;
                case (')'):
                    bracket = new CloseBracket();
                    break;
            }
            return bracket;
        }
        private void CheckLine (List<object> parsedLine) 
        {
            int countOperation = 0, countBracket = 0, countNumbers = 0;
            foreach (var symbol in parsedLine)
            {

                if (symbol is double)
                {
                    countNumbers += 1;
                }
                else
                {
                    if (symbol is OpenBracket) countBracket += 1;
                    else if (symbol is CloseBracket ) countBracket -= 1;
                    else if (symbol is Operation) countOperation += 1;
                }
            }
            if (!(countBracket == 0 && countNumbers - countOperation == 1))
            {
                throw new Exception("некорректная строка");
            }
        } 
        private Stack<object> GetReversedLine (List<object> parsedLine)
        {
            var outputStack = new Stack<object>();
            var operationsStack= new Stack<object>();
            int numberOfSymbol = 0;
            while (numberOfSymbol < parsedLine.Count())
            {
                if (parsedLine[numberOfSymbol] is double)
                {
                    outputStack.Push(parsedLine[numberOfSymbol]);
                    numberOfSymbol++;                     
                }
                else if (parsedLine[numberOfSymbol] is Bracket)
                {
                    if (parsedLine[numberOfSymbol] is OpenBracket)
                    {
                        operationsStack.Push(parsedLine[numberOfSymbol]);
                        numberOfSymbol++;
                    }
                    else if (parsedLine[numberOfSymbol] is CloseBracket)
                    {
                        if (operationsStack.Peek() is OpenBracket)
                        {
                            operationsStack.Pop();
                            numberOfSymbol++;
                        }
                        else 
                        {
                            outputStack.Push(operationsStack.Pop());
                        }
                    }
                }
                else if (parsedLine[numberOfSymbol] is Operation operation)
                {
                    if (operationsStack.Count == 0 || (operationsStack.Peek() is OpenBracket) ||
                       (operationsStack.Peek() as Operation).Prior > operation.Prior)
                    {
                        operationsStack.Push(operation);
                        numberOfSymbol++;
                    }
                    else
                    {
                        outputStack.Push(operationsStack.Pop());
                        operationsStack.Push(operation);
                        numberOfSymbol++;
                    }
                }
                continue; //сделать работу с аргумнтом
            }
            while (operationsStack.Count != 0)
            {
                outputStack.Push(operationsStack.Pop());
            }
            return outputStack;
        }
        private string StringReversedLine (object[] reversedLine)
        {
            Array.Reverse(reversedLine);
            int countSymbolinStack = reversedLine.Length;
            StringBuilder @string = new StringBuilder();
            foreach (var element in reversedLine)
            {
                @string.Append(element.ToString());
                @string.Append(" ");
            }
            return @string.ToString();

        }
    }
}
