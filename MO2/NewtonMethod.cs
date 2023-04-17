using MO1;
using MO2.ArgMin;
using static System.Math;

namespace MO2;

public class NewtonMethod
{
    public const double EpsF = 1e-14;
    public const double EpsX = 1e-14;
    public const double EpsGrad = 1e-14;

    private readonly Func<Point, double> _func;
    private readonly IArgMinSeeker _argMinSeeker;
    private readonly MinimumIntervalFinder _intervalFinder = new();

    public NewtonMethod(Func<Point, double> func, IArgMinSeeker argMinSeeker)
    {
        _func = func;
        _argMinSeeker = argMinSeeker;
    }

    public Point Find(Point start)
    {

        var grad = MathHelper.Gradient(_func, start);
        var inverseHesse = new HesseMatrix(_func, start).Inverse();

        var direction = inverseHesse.Multiply(grad);

        var lambda = _argMinSeeker.Find(l => _func(start - l * direction));

        var nextPoint = start - lambda * direction;
        var gradNext = MathHelper.Gradient(_func, nextPoint);

        Console.WriteLine($"({start.X:F3}, {start.Y:F3}) + {lambda:F3}*({direction.X:F3}, {direction.Y:F3}) ");

        while (!ShouldStop(start, nextPoint, gradNext))
        {
            start = nextPoint;
            grad = gradNext;
            //Console.WriteLine(start);

            grad = MathHelper.Gradient(_func, start);
            inverseHesse = new HesseMatrix(_func, start).Inverse();

            direction = inverseHesse.Multiply(grad);

            lambda = _argMinSeeker.Find(l => _func(start - l * direction));

            nextPoint = start - lambda * direction;
            gradNext = MathHelper.Gradient(_func, nextPoint);
            Console.WriteLine($"({start.X:F3}, {start.Y:F3}) + {lambda:F3}*({direction.X:F3}, {direction.Y:F3}) ");
        }

        return nextPoint;
    }

    private bool ShouldStop(Point prev, Point current, Point gradNext)
    {
        if (double.IsNaN(prev.X)) return true;

        return Abs(_func(prev) - _func(current)) <= EpsF ||
               Abs(prev.X - current.X) < EpsX && Abs(prev.X - current.X) < EpsX ||
               Abs(gradNext.X + gradNext.Y) < EpsGrad;
    }
}