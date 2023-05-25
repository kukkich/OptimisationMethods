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
        Point penalties)
    {
        penalties *= 2;
        do
        {
            penalties /= 2;
            Func<Point, double> q = x => function(x) + penalties.X * g(x) + penalties.Y * g(x);

            point = _hookeJeeves.FindMinimum(q, point, 1e-19);

        } while (g(point) > MethodsConfig.Eps);
        return point;
    }
}