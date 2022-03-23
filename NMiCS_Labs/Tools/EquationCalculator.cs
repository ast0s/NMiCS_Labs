using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMiCS_Labs.Tools
{
    class EquationCalculator
    {
        IEquationMethod method;
        public EquationCalculator(double epsilon) => method = new HordesMethod(epsilon);
        public IEnumerable<double> SolveEquation(Equation eq)
        {
            var (equations, roots) = DivideIntoSegments(eq);
            return equations.Select(x => method.FindX(x)).Concat(roots).OrderBy(x => x);
        }
        public (IEnumerable<Equation>, IEnumerable<double>) DivideIntoSegments(Equation eq)
        {
            List<Equation> equations = new List<Equation>();
            List<double> roots = new List<double>();
            RecursiveDivideIntoSegments(eq, ref equations, ref roots, 10 * (int)Math.Abs(eq.Max - eq.Min), false);
            return (equations, roots);
        }
        private void RecursiveDivideIntoSegments(
            Equation eq,
            ref List<Equation> equations,
            ref List<double> roots,
            int segments_count,
            bool isAlright = false)
        {
            var divided = Enumerable.Range(0, segments_count + 1)
                .Select(i => eq.Min + ((eq.Max - eq.Min) / segments_count * i))
                .Select(x => (X: x, F: eq.Func(x), Fd: eq.GetDerivativeX(x)))
                .ToArray();
            bool enteredRecursion = false;

            for (int i = 0; i < divided.Length - 1; i++)
            {
                if (divided[i].F is 0)
                {
                    roots.Add(divided[i].F);
                    continue;
                }
                if (divided[i].F * divided[i + 1].F < 0)
                {
                    for (int j = 0; j < divided.Length - 1; j++)
                    {
                        if (divided[j].Fd * divided[j + 1].Fd < 0)
                        {
                            RecursiveDivideIntoSegments(new Equation(divided[i].X, divided[i + 1].X, eq.Func), ref equations, ref roots, segments_count, false);
                            enteredRecursion = true;
                        }
                    }
                    if (!isAlright)
                    {
                        RecursiveDivideIntoSegments(new Equation(divided[i].X, divided[i + 1].X, eq.Func), ref equations, ref roots, segments_count, true);
                        enteredRecursion = true;
                    }
                }
            }
            if (divided[divided.Length - 1].F is 0)
            {
                roots.Add(divided[divided.Length - 1].F);
            }
            else if (!enteredRecursion)
            {
                equations.Add(eq);
            }
        }
    }
}
