using MO2.Core;

namespace MO2;

public readonly record struct Point(double X, double Y)
{
    public double Norm => Math.Sqrt(X * X + Y * Y);

    public static Point operator *(Point point, double coef)
    {
        return new Point(point.X * coef, point.Y * coef);
    }
    public static Point operator *(double coef, Point point)
    {
        return new Point(point.X * coef, point.Y * coef);
    }
    public static Matrix operator *(Point point1, Point point2)
    {
        return new Matrix(new[,]
        {
            {point1.X * point2.X, point1.X * point2.Y},
            {point1.Y * point2.X, point1.Y * point2.Y}
        });
    }

    public static double Multiply(Point point1, Point point2)
    {
        return point1.X * point2.X + point1.Y * point2.Y;
    }

    public static Point operator +(Point p, Point q)
    {
        return new Point(p.X + q.X, p.Y + q.Y);
    }
    public static Point operator -(Point p, Point q)
    {
        return new Point(p.X - q.X, p.Y - q.Y);
    }
};