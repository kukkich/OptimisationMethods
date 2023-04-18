using System.ComponentModel.DataAnnotations;
using MO1;
using MO2.ArgMin;
using MO2.Core;

namespace MO2;

public class BFGS
{
    private readonly IArgMinSeeker _argMinSeeker;

    public BFGS(IArgMinSeeker argMinSeeker)
    {
        _argMinSeeker = argMinSeeker;
    }

    public Point FindMinimum(Func<Point, double> function, Point x, double[,] startEta)
    {
        var eta = new Matrix((double[,])startEta.Clone());

        var i = 0;

        Point gradF;

        do
        {
            gradF = MathHelper.Gradient(function, x);

            var direction = eta * gradF;

            var lambda = _argMinSeeker.Find(l => function(x - l * direction));

            Console.WriteLine($"({x.X:F3}, {x.Y:F3}) + {lambda:F3}*({direction.X:F3}, {direction.Y:F3}) ");

            var xNext = x - lambda * direction;

            var deltaX = xNext - x;

            var gradFNext = MathHelper.Gradient(function, xNext);

            var deltaG = gradFNext - gradF;

            var deltaEta = (deltaX - eta * deltaG) * (deltaX - eta * deltaG) / Point.Multiply(deltaX - eta * deltaG, deltaG);

            eta += deltaEta;

            x = xNext;

            gradF = gradFNext;

            i++;
        } while (gradF.Norm > MethodsConfig.GradEps);

        return x;
    }
}