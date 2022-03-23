using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMiCS_Labs.Tools
{
    class HordesMethod : IEquationMethod
    {
        readonly double epsilon;
        public HordesMethod(double epsilon)
        {
            this.epsilon = epsilon;
        }
        public double FindX(Equation eq)
        {
            double a = eq.Min;
            double b = eq.Max;
            double Fa = eq.Func(a);
            double Fb = eq.Func(b);

            double x = a - Fa * ((b - a) / (Fb - Fa));
            double Fx = eq.Func(x);

            if (epsilon < Math.Abs(eq.Func(x)))
            {
                if (Fa * Fx < 0)
                {
                    eq.Max = x;
                    FindX(eq);
                }
                else if (Fb * Fx < 0)
                {
                    eq.Min = x;
                    FindX(eq);
                }
            }

            return x;
        }
    }
}
