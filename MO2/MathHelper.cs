namespace MO2;

public static class MathHelper
{
    private const double DerivativeStep = 1e-15;

    public static Point Gradient(Func<Point, double> f, Point p)
    {
        return new Point(
            X: (f(p with {X = p.X + DerivativeStep}) - f(p with {X = p.X - DerivativeStep}))
               / (2 * DerivativeStep),
            Y: (f(p with { Y = p.Y + DerivativeStep }) - f(p with { Y = p.Y - DerivativeStep }))
               / (2 * DerivativeStep)
            );
    }
}