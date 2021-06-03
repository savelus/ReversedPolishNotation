using NUnit.Framework;
using RPNLogic;
using System.Collections.Generic;


namespace RPNTests
{
    public class Tests
    {
        RPN rpn = new RPN();
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("5*8+10/2-5", ExpectedResult = "5 8 * 10 2 / 5 - + ")]
        [TestCase("5* 8 + 10 / 2 - 5     ", ExpectedResult = "5 8 * 10 2 / 5 - + ")]
        [TestCase("5*(8+10)/2-5", ExpectedResult = "5 8 10 + * 2 / 5 - ")]

        public string RPN_Test(string expression)
        {
            var reversedLine = rpn.Reverse(expression, out string strRPN);
            return strRPN;
        }
      
    }
}