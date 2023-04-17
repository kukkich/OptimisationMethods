namespace MO2;

public class HesseMatrix
{
    public double this[int row, int column] => _values[row, column];
    private const double DerivativeStep = 1e-7;
    private readonly double[,] _values = new double[2, 2];

    private double a => _values[0, 0];
    private double b => _values[0, 1];
    private double c => _values[1, 0];
    private double d => _values[1, 1];


    public HesseMatrix(Func<Point, double> f, Point point)
    {

        _values[0, 0] = f(point with { X = point.X + 2 * DerivativeStep })
                        - 2d * f(point)
                        + f(point with { X = point.X - 2 * DerivativeStep });
        _values[1, 1] = f(point with { Y = point.Y + 2 * DerivativeStep })
                        - 2d * f(point)
                        + f(point with { Y = point.Y - 2 * DerivativeStep });
        _values[0, 0] /= 4 * DerivativeStep * DerivativeStep;
        _values[1, 1] /= 4 * DerivativeStep * DerivativeStep;

        _values[0, 1] = f(new Point(X: point.X + DerivativeStep, Y: point.Y + DerivativeStep))
                         - f(new Point(X: point.X - DerivativeStep, Y: point.Y + DerivativeStep))
                         - f(new Point(X: point.X + DerivativeStep, Y: point.Y - DerivativeStep))
                         + f(new Point(X: point.X - DerivativeStep, Y: point.Y - DerivativeStep));
        _values[0, 1] /= 4 * DerivativeStep * DerivativeStep;
        _values[1, 0] = _values[0, 1];
    }

    public InverseHesse Inverse()
    {
        var det = 1d / (a * d - b * c);

        var inversed = new double[2, 2];
        inversed[0, 0] = d / det;
        inversed[0, 1] = -b / det;
        inversed[1, 0] = -c / det;
        inversed[1, 1] = a / det;
        return new InverseHesse(inversed);
    }

    public class InverseHesse
    {
        public double this[int row, int column] => _values[row, column];
        private readonly double[,] _values;

        private double a => _values[0, 0];
        private double b => _values[0, 1];
        private double c => _values[1, 0];
        private double d => _values[1, 1];

        internal InverseHesse(double[,] values)
        {
            _values = values;
        }

        public Point Multiply(Point v)
        {
            return new Point(
                X: a*v.X + b * v.Y,
                Y: c*v.X + d * v.Y
            );
        }
    }
}