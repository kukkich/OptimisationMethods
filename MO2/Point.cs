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

    public static Point operator +(Point p, Point q)
    {
        return new Point(p.X + q.X, p.Y + q.Y);
    }
    public static Point operator -(Point p, Point q)
    {
        return new Point(p.X - q.X, p.Y - q.Y);
    }
};