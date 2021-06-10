using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading.Tasks;


namespace RPNLogic
{
    public class RPN
    {
        public Dictionary<double, double> GetAnswer (string expression, out string strRPN, double start, double step, double end)
        {
            expression = expression.Replace(" ", "");
            List<object> parsedLine = Parse(expression);
           // CheckLine(parsedLine);
            var reversedLine = GetReversedLine(parsedLine, out strRPN);
            var calculator = new Calculator(start, step, end);
            return calculator.GetDictionary(reversedLine);
        }
        private List<object> Parse(string expression)
        {
            List<object> parsedLine = new List<object>();

            string tempNum = "";
            for (int i = 0; i < expression.Length; i++)
            {
                if ("+-*/%".Contains(expression[i]))
                {
                    parsedLine = ParseNumbers(ref tempNum, parsedLine);
                    parsedLine.Add(ChooseOperation(expression[i]));
                }
                else if ("()".Contains(expression[i]))
                {
                    parsedLine = ParseNumbers(ref tempNum, parsedLine);
                    parsedLine.Add(ChooseBracket(expression[i]));
                }
                else if (Char.IsDigit(expression[i])
                        || (".,".Contains(expression[i])
                            && Char.IsDigit(expression[i - 1])
                            && Char.IsDigit(expression[i + 1])))
                {
                    tempNum += expression[i].ToString();
                }
                else if (char.IsLetter(expression[i]))
                {
                    if (expression[i] == 'x')
                    {
                        parsedLine.Add(new Argument());
                    }
                    else if ("sincostan".Contains(expression.Substring(i, 3)))
                    {
                        parsedLine.Add(GetTextOperation(expression.Substring(i, 3)));
                        i += 2;
                    }
                    else
                    {
                        throw new Exception($"неопознанный символ {expression[i]} ") ; 
                    } 
                    
                }
            }
            parsedLine = ParseNumbers(ref tempNum, parsedLine);
            return parsedLine;
        }
        private Operation GetTextOperation(string txtOperation)
        {
            switch (txtOperation)
            {
                case ("sin"):
                    return new Sin();
                case ("cos"):
                    return new Cos();
                case ("tan"):
                    return new Tan();
                default:
                    throw new Exception($"Нет такой операции {txtOperation}");
                    
            }
        }
        private List<object> ParseNumbers (ref string tempNum, List<object> parsedLine)
        {
            if (tempNum != "")
            {
                double number = double.Parse(tempNum);
                parsedLine.Add(number);
                tempNum = "";
            }
            return parsedLine;
        }
        private Operation ChooseOperation(char op)
        {
            Operation operation = null;
            switch (op)
            {
                case ('%'):
                    operation = new Per();
                    break;
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

                if (symbol is double || symbol is Argument)
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
        private Stack<object> GetReversedLine (List<object> parsedLine, out string strRPN)
        {
            var reverseStack = new Stack<object>();
            var operationsStack= new Stack<object>();
            int numberOfSymbol = 0;
            while (numberOfSymbol < parsedLine.Count())
            {
                if (parsedLine[numberOfSymbol] is double || parsedLine[numberOfSymbol] is Argument)
                {
                    reverseStack.Push(parsedLine[numberOfSymbol]);
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
                            reverseStack.Push(operationsStack.Pop());
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
                        reverseStack.Push(operationsStack.Pop());
                        operationsStack.Push(operation);
                        numberOfSymbol++;
                    }
                }
            }
            while (operationsStack.Count != 0)
            {
                reverseStack.Push(operationsStack.Pop());
            }
            strRPN = StringReversedLine(reverseStack.ToArray());
            return reverseStack;
        }
        private string StringReversedLine (object[] reversedLine)
        {
            Array.Reverse(reversedLine);
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
