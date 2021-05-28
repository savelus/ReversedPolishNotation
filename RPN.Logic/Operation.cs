using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPNLogic
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
    public class Sin : Operation
    {
        public override string Name => "sin";
        public override int CountParams => 1;
        public override double Calculate(double[] @params) { return Math.Sin(Math.PI * @params[0] / 180); }
        public override int Prior => 1;
    }
    public class Cos : Operation
    {
        public override string Name => "cos";
        public override int CountParams => 2;
        public override double Calculate(double[] @params) { return Math.Cos(Math.PI * @params[0] / 180); }
        public override int Prior => 1;
    }
    public class Tan : Operation
    {
        public override string Name => "tan";
        public override int CountParams => 2;
        public override double Calculate(double[] @params) { return Math.Tan(Math.PI * @params[0] / 180); }
        public override int Prior => 1;
    }



}
