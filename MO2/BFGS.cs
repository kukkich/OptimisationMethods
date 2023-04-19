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

        Point gradF;

        do
        {
            gradF = MathHelper.Gradient(function, point);

            var direction = eta * gradF;

            var lambda = _argMinSeeker.Find(l => function(point - l * direction));

            //Console.WriteLine($"({point.X:F3}, {point.Y:F3}) + {lambda:F3}*({direction.X:F3}, {direction.Y:F3}) ");

            var nextPoint = point - lambda * direction;

            var deltaX = nextPoint - point;

            var gradFNext = MathHelper.Gradient(function, nextPoint);

            var deltaG = gradFNext - gradF;

            var deltaEta = (deltaX - eta * deltaG) * (deltaX - eta * deltaG) / Point.Multiply(deltaX - eta * deltaG, deltaG);

            eta += deltaEta;

            IterationInformer.Inform(i, nextPoint, function(nextPoint), direction, lambda, Abs(nextPoint.X - point.X),
                Abs(nextPoint.Y - point.Y), Abs(function(nextPoint) - function(point)),
                Acos((point.X * direction.X + point.Y * direction.Y) /
                     Sqrt(point.X * point.X + point.Y * point.Y) *
                     Sqrt(direction.X * direction.X + direction.Y * direction.Y)), gradFNext, eta);

            point = nextPoint;

            gradF = gradFNext;

            i++;
        } while (gradF.Norm > MethodsConfig.GradEps);

        return point;
    }
}