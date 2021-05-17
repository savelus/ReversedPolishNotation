using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPNLogic
{
    public class Argument
    {
        public string Name => "x";
        public double Value { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
