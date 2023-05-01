using MO1;
using MO2.ArgMin;
using static System.Math;

namespace MO2;

public class NewtonMethod
{
    public const double EpsF = 1e-3;
    public const double EpsX = 1e-3;
    public const double EpsGrad = 1e-3;

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

        MethodsConfig.FCalc = 2;

        var hesse = new HesseMatrix(_func, start);

        var inverseHesse = hesse.Inverse();

        MethodsConfig.FCalc += 8;

        var direction = inverseHesse.Multiply(grad);

        var lambda = _argMinSeeker.Find(l => _func(start - l * direction));

        var nextPoint = start - lambda * direction;

        var i = 1;

        IterationInformer.Inform(i, nextPoint, _func(nextPoint), direction, lambda, Abs(nextPoint.X - start.X),
            Abs(nextPoint.Y - start.Y), Abs(_func(nextPoint) - _func(start)),
            Acos((nextPoint.X * direction.X + nextPoint.Y * direction.Y) /
                 Sqrt(nextPoint.X * nextPoint.X + nextPoint.Y * nextPoint.Y) *
                 Sqrt(direction.X * direction.X + direction.Y * direction.Y)), grad, hesse);

        var gradNext = MathHelper.Gradient(_func, nextPoint);

        MethodsConfig.FCalc += 2;

        while (!ShouldStop(start, nextPoint, gradNext))
        {
            start = nextPoint;
            grad = gradNext;

            hesse = new HesseMatrix(_func, start);

            MethodsConfig.FCalc += 8;

            inverseHesse = hesse.Inverse();

            direction = inverseHesse.Multiply(grad);

            lambda = _argMinSeeker.Find(l => _func(start - l * direction));

            nextPoint = start - lambda * direction;

            IterationInformer.Inform(++i, nextPoint, _func(nextPoint), direction, lambda, Abs(nextPoint.X - start.X),
                Abs(nextPoint.Y - start.Y), Abs(_func(nextPoint) - _func(start)),
                Acos((nextPoint.X * direction.X + nextPoint.Y * direction.Y) /
                     Sqrt(nextPoint.X * nextPoint.X + nextPoint.Y * nextPoint.Y) *
                     Sqrt(direction.X * direction.X + direction.Y * direction.Y)), grad, hesse);

            gradNext = MathHelper.Gradient(_func, nextPoint);

            MethodsConfig.FCalc += 2;
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