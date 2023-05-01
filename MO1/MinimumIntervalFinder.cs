using MO1.Core;

namespace MO1;

public class MinimumIntervalFinder
{
    public Interval FindMinimumInterval(double startPoint, Func<double, double> function)
    {
        var xPrev = startPoint;
        var xNext = 0d;
        var h = 0d;

        var fValue = function(xPrev);

        MethodsConfig.FCalc++;

        if (fValue > function(xPrev + MethodsConfig.MinimumIntervalDelta))
        {
            xNext = xPrev + MethodsConfig.MinimumIntervalDelta;
            h = MethodsConfig.MinimumIntervalDelta;
        }
        else if (fValue > function(xPrev - MethodsConfig.MinimumIntervalDelta))
        {
            xNext = xPrev - MethodsConfig.MinimumIntervalDelta;
            h = -MethodsConfig.MinimumIntervalDelta;
            MethodsConfig.FCalc++;
        }

        MethodsConfig.FCalc++;

        xPrev = xNext;
        h *= 2;
        xNext = xPrev + h;

        var minimumInterval = new Interval(xPrev, xNext);

        IterationInformer.Inform(1, xPrev, function(xPrev));

        var i = 2;

        for (; function(xPrev) > function(xNext); i++)
        {
            MethodsConfig.FCalc++;

            xPrev = xNext;
            h *= 2;
            xNext = xPrev + h;

            minimumInterval.Left = xPrev;
            minimumInterval.Right = xNext;

            IterationInformer.Inform(i, xPrev, function(xPrev));
        }

        IterationInformer.Inform(i, xNext, function(xNext));

        minimumInterval.Left = xPrev - h / 2;

        return minimumInterval;
    }
}