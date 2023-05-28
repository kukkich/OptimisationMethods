using MO1;
using MO2;

namespace MO3.Methods;

public class Barrier
{
    private readonly HookeJeeves _hookeJeeves;

    public Barrier(HookeJeeves hookeJeeves)
    {
        _hookeJeeves = hookeJeeves;
    }

    public Point FindMinimum(Func<Point, double> function, Func<Point, double> g, Point point,
        double r)
    {
        var i = 0;
        r *= 2;
        do
        {
            r /= 2;
            double Q(Point x) => function(x) + r * g(x);

            point = _hookeJeeves.FindMinimum(Q, point);
            i++;
            var v = r * g(point);

        } while (Math.Abs(r * g(point)) > MethodsConfig.EpsPB && !double.IsNaN(Math.Abs(r * g(point))) &&
                 !double.IsInfinity(Math.Abs(r * g(point))));
        IterationInformer.Inform(i, MethodsConfig.FCalc, point, function(point));
        MethodsConfig.FCalc = 0;
        return point;
    }
}