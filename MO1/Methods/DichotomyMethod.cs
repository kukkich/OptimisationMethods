using MO1.Core;

namespace MO1.Methods;

public class DichotomyMethod : IExtremumSearchMethod
{
    public double FindMinimum(Interval minimumInterval, Func<double, double> function)
    {
        var a = minimumInterval.LeftPoint;
        var b = minimumInterval.RightPoint;

        var x1 = (a + b - MethodsConfig.Delta) / 2;
        var x2 = (a + b + MethodsConfig.Delta) / 2;

        var f1Value = function(x1);
        var f2Value = function(x2);

        if (f1Value < f2Value)
        {
            b = x2;
        }
        else if (f1Value > f2Value)
        {
            a = x1;
        }

        IterationInformer.Inform(1, x1, x2, a, b, f1Value, f2Value);

        for (var i = 2; Math.Abs(b - a) > MethodsConfig.Eps; i++)
        {
            x1 = (a + b - MethodsConfig.Delta) / 2;
            x2 = (a + b + MethodsConfig.Delta) / 2;

            f1Value = function(x1);
            f2Value = function(x2);

            if (f1Value < f2Value)
            {
                b = x2;
            }
            else if (f1Value > f2Value)
            {
                a = x1;
            }

            IterationInformer.Inform(i, x1, x2, a, b, f1Value, f2Value);
        }

        return (a + b) / 2;
    }
}