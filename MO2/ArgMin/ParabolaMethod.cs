using MO1;

namespace MO2.ArgMin;

public class ParabolaMethod : IArgMinSeeker
{
    private readonly MinimumIntervalFinder _intervalFinder = new();

    public double Find(Func<double, double> f)
    {
        var lambda = 1d;
        var lambdaInterval = _intervalFinder.FindMinimumInterval(lambda, f);

        var (x1, x2, x3) = (
            lambdaInterval.Left,
            (lambdaInterval.Left + lambdaInterval.Right) / 2d,
            lambdaInterval.Right
            );
        var (y1, y2, y3) = (f(x1), f(x2), f(x3));

        MethodsConfig.FCalc += 3;

        var a = (y3 - (x3 * (y2 - y1) + x2 * y1 - x1 * y2) / (x2 - x1)) / (x3 * (x3 - x1 - x2) + x1 * x2);
        var b = (y2 - y1) / (x2 - x1) - a * (x1 + x2);

        return -1d * b / (2d * a);
    }
}