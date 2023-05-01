using MO1;
using MO2.ArgMin;
using MO2.Core;
using static System.Math;

namespace MO2;

public class BFGS
{
    private readonly IArgMinSeeker _argMinSeeker;

    public BFGS(IArgMinSeeker argMinSeeker)
    {
        _argMinSeeker = argMinSeeker;
    }

    public Point FindMinimum(Func<Point, double> function, Point point, double[,] startEta)
    {
        var eta = new Matrix((double[,])startEta.Clone());

        var i = 1;

        var gradF = MathHelper.Gradient(function, point);

        MethodsConfig.FCalc = 2;

        do
        {
            var direction = eta * gradF;

            var lambda = _argMinSeeker.Find(l => function(point - l * direction));

            var nextPoint = point - lambda * direction;

            IterationInformer.Inform(i, nextPoint, function(nextPoint), direction, lambda, Abs(nextPoint.X - point.X),
                Abs(nextPoint.Y - point.Y), Abs(function(nextPoint) - function(point)),
                Acos((nextPoint.X * direction.X + nextPoint.Y * direction.Y) /
                     Sqrt(nextPoint.X * nextPoint.X + nextPoint.Y * nextPoint.Y) *
                     Sqrt(direction.X * direction.X + direction.Y * direction.Y)), gradF, eta);

            var deltaX = nextPoint - point;

            var gradFNext = MathHelper.Gradient(function, nextPoint);

            MethodsConfig.FCalc += 2;

            var deltaG = gradFNext - gradF;

            var deltaEta = (deltaX - eta * deltaG) * (deltaX - eta * deltaG) / Point.Multiply(deltaX - eta * deltaG, deltaG);

            eta += deltaEta;

            point = nextPoint;

            gradF = gradFNext;

            i++;

        } while (gradF.Norm > MethodsConfig.GradEps);

        return point;
    }
}