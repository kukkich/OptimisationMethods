using MO1.Core;

namespace MO1.Methods;

public class GoldenSectionMethod : IExtremumSearchMethod
{
    public double FindMinimum(Interval minimumInterval, Func<double, double> function)
    {
        var a = minimumInterval.LeftPoint;
        var b = minimumInterval.RightPoint;

        var x1 = a + 0.381966011d * (b - a);
        var x2 = b - 0.381966011d * (b - a);

        var f1Value = function(x1);
        var f2Value = function(x2);

        if (f1Value < f2Value)
        {
            b = x2;
            x2 = x1;
            f2Value = f1Value;
            x1 = a + 0.381966011d * (b - a);
            f1Value = function(x1);
        }
        else if (f1Value > f2Value)
        {
            a = x1;
            x1 = x2;
            f1Value = f2Value;
            x2 = b - 0.381966011d * (b - a);
            f2Value = function(x2);
        }

        IterationInformer.Inform(1, x1, x2, a, b, f1Value, f2Value);

        for (var i = 2; Math.Abs(b - a) > MethodsConfig.Eps; i++)
        {
            if (f1Value < f2Value)
            {
                b = x2;
                x2 = x1;
                f2Value = f1Value;
                x1 = a + 0.381966011d * (b - a);
                f1Value = function(x1);
            }
            else if (f1Value > f2Value)
            {
                a = x1;
                x1 = x2;
                f1Value = f2Value;
                x2 = b - 0.381966011d * (b - a);
                f2Value = function(x2);
            }

            IterationInformer.Inform(i, x1, x2, a, b, f1Value, f2Value);
        }

        return (a + b) / 2;
    }
}