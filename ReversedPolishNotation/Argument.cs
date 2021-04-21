using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversedPolishNotation
{
    public class Argument
    {
        public string Name { get { return "x"; } }
        public double Value { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
