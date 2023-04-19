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

    public NewtonMethod(Func<Point, double> func, IArgMinSeeker argMinSeeker)
    {
        _func = func;
        _argMinSeeker = argMinSeeker;
    }

    public Point Find(Point start)
    {

        var grad = MathHelper.Gradient(_func, start);

        var hesse = new HesseMatrix(_func, start);

        var inverseHesse = hesse.Inverse();

        var direction = inverseHesse.Multiply(grad);

        var lambda = _argMinSeeker.Find(l => _func(start - l * direction));

        var nextPoint = start - lambda * direction;

        var gradNext = MathHelper.Gradient(_func, nextPoint);

        IterationInformer.Inform(1, nextPoint, _func(nextPoint), direction, lambda, Abs(nextPoint.X - start.X),
            Abs(nextPoint.Y - start.Y), Abs(_func(nextPoint) - _func(start)),
            Acos((start.X * direction.X + start.Y * direction.Y) /
                 Sqrt(start.X * start.X + start.Y * start.Y) *
                 Sqrt(direction.X * direction.X + direction.Y * direction.Y)), gradNext, hesse);

        //Console.WriteLine($"({start.X:F3}, {start.Y:F3}) + {lambda:F3}*({direction.X:F3}, {direction.Y:F3}) ");

        while (!ShouldStop(start, nextPoint, gradNext))
        {
            start = nextPoint;
            grad = gradNext;
            //Console.WriteLine(start);

            hesse = new HesseMatrix(_func, start);

            inverseHesse = hesse.Inverse();

            direction = inverseHesse.Multiply(grad);

            lambda = _argMinSeeker.Find(l => _func(start - l * direction));

            nextPoint = start - lambda * direction;

            gradNext = MathHelper.Gradient(_func, nextPoint);

            IterationInformer.Inform(1, start, _func(start), direction, lambda, Abs(nextPoint.X - start.X),
                Abs(nextPoint.Y - start.Y), Abs(_func(nextPoint) - _func(start)),
                Acos((start.X * direction.X + start.Y * direction.Y) /
                     Sqrt(start.X * start.X + start.Y * start.Y) *
                     Sqrt(direction.X * direction.X + direction.Y * direction.Y)), grad, hesse);

            //Console.WriteLine($"({start.X:F3}, {start.Y:F3}) + {lambda:F3}*({direction.X:F3}, {direction.Y:F3}) ");
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