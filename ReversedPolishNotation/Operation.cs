using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversedPolishNotation
{
    public abstract class Operation
    {
        public abstract string Name { get; }
        public abstract int CountParams { get; }
        public abstract double Calculate(double[] @params);
        public abstract int Prior { get; }
        public override string ToString() { return Name; }

    }
    public class Plus : Operation
    {
        public override string Name => "+";
        public override int CountParams => 2;
        public override double Calculate(double[] @params) { return @params[1] + @params[0]; }
        public override int Prior => 3;
    }

    public class Minus : Operation
    {
        public override string Name => "-";
        public override int CountParams => 2;
        public override double Calculate(double[] @params) { return @params[1] - @params[0]; }
        public override int Prior => 3;
    }
    public class Mult : Operation
    {
        public override string Name => "*";
        public override int CountParams => 2;
        public override double Calculate(double[] @params) { return @params[1] * @params[0]; }
        public override int Prior => 2;
    }
    public class Div : Operation
    {
        public override string Name => "/";
        public override int CountParams => 2;
        public override double Calculate(double[] @params) { return @params[1] / @params[0]; }
        public override int Prior => 2;
    }

}
