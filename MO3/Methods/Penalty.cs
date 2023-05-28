using MO1;
using MO2;

namespace MO3.Methods;

public class Penalty
{
    private readonly HookeJeeves _hookeJeeves;

    public Penalty(HookeJeeves hookeJeeves)
    {
        _hookeJeeves = hookeJeeves;
    }

    public Point FindMinimum(Func<Point, double> function, Func<Point, double> h, Func<Point, double> g, Point point,
        double r)
    {
        var i = 0;
        do
        {
            double Q(Point x) => function(x) + r * g(x) + r * h(x);

            point = _hookeJeeves.FindMinimum(Q, point);
            r *= r;
            i++;
        } while (Math.Abs(g(point) + h(point)) > MethodsConfig.EpsPB);
        IterationInformer.Inform(i, MethodsConfig.FCalc, point, function(point));
        MethodsConfig.FCalc = 0;
        return point;
    }
}