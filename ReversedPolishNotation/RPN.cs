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
            return " ";
        }
        
        private List<object> Parse(string expression)
        {
            List<object> parsedLine = new List<object>();
            string operands = "+-*/^()";
            
            string tempNum = "";
            for (int i = 0; i < expression.Length; i++)
            {
                if (operands.Contains(expression[i]))
                {
                    if (tempNum != "")
                    {
                        double number = double.Parse(tempNum, CultureInfo.InvariantCulture);
                        parsedLine.Add(number);
                        tempNum = "";
                    }
                    parsedLine.Add(expression[i]);

                }
                else if (IsNumbers(expression[i]) || (".,".Contains(expression[i]) && IsNumbers(expression[i - 1]) && IsNumbers(expression[i + 1])))
                {
                    tempNum += expression[i].ToString();
                }

            }
            if (tempNum != "")
            {
                double number = double.Parse(tempNum, CultureInfo.InvariantCulture);
                parsedLine.Add(number);
                tempNum = "";
            }
            return parsedLine;
        }
        private bool IsNumbers (char sym)
        {         
            return (sym >= '0' && sym <= '9');
        }
        private bool CheckLine (List<object> parsedLine)
        {
            int countOperands = 0, countBracket = 0, countNumbers = 0;
            foreach (var symbol in parsedLine)
            {

                if (symbol is double)
                {
                    countNumbers += 1;
                }
                else
                {
                    string sym = symbol.ToString();
                    if (sym == "(") countBracket += 1;
                    else if (sym == ")") countBracket -= 1;
                    else if ("-+*/^".Contains(symbol.ToString())) countOperands += 1;

                }

            }
            return (countBracket == 0 && countNumbers - countOperands == 2);
        }
    }
}
