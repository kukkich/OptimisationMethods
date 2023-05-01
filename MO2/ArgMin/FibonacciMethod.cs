﻿using MO1;

namespace MO2.ArgMin;

public class FibonacciMethod : IArgMinSeeker
{
    private readonly MinimumIntervalFinder _intervalFinder = new();

    public double Find(Func<double, double> f)
    {
        var minimumInterval = _intervalFinder.FindMinimumInterval(1d, f);

        var a = minimumInterval.Left;
        var b = minimumInterval.Right;

        var k = 1;

        for (; (b - a) / MethodsConfig.Eps > CalcFibonacciNumber(k); ++k) ;

        var n = k - 2;

        var x1 = a + CalcFibonacciNumber(n) / CalcFibonacciNumber(n + 2) * (b - a);
        var x2 = a + CalcFibonacciNumber(n + 1) / CalcFibonacciNumber(n + 2) * (b - a);

        var f1Value = f(x1);
        var f2Value = f(x2);

        MethodsConfig.FCalc += 2;

        for (var i = 1; i != n; i++)
        {
            if (f1Value < f2Value)
            {
                b = x2;
                x2 = x1;
                f2Value = f1Value;
                x1 = a + CalcFibonacciNumber(n - i + 1) / CalcFibonacciNumber(n - i + 3) * (b - a);
                f1Value = f(x1);
            }
            else if (f1Value > f2Value)
            {
                a = x1;
                x1 = x2;
                f1Value = f2Value;
                x2 = a + CalcFibonacciNumber(n - i + 2) / CalcFibonacciNumber(n - i + 3) * (b - a);
                f2Value = f(x2);
            }

            MethodsConfig.FCalc++;
        }

        return (x1 + x2) / 2;
    }

    private static double CalcFibonacciNumber(int n)
    {
        return (Math.Pow((1 + Math.Sqrt(5)) / 2, n) - Math.Pow((1 - Math.Sqrt(5)) / 2, n)) / Math.Sqrt(5);
    }
}