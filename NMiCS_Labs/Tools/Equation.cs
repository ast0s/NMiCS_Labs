using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMiCS_Labs.Tools
{
    class Equation
    {
        public double Min, Max;
        public Func<double, double> Func;

        public Equation(double min, double max, Func<double, double> func)
        {
            Min = min;
            Max = max;
            Func = func;
        }
        public double GetDerivativeX(double x, double delta_x = 0.01) => (Func(x + delta_x) - Func(delta_x)) / delta_x;
    }
}
