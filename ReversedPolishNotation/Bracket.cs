using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversedPolishNotation
{
    public abstract class Bracket
    {
        public abstract string Name { get; }
    }
    public class OpenBracket : Bracket
    {
        public override string Name => "(";
    }
    public class CloseBracket : Bracket
    {
        public override string Name => ")";
    }

}
