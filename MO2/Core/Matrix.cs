namespace MO2.Core;

public class Matrix
{
    private readonly double[,] _values;

    public Matrix(double[,] values)
    {
        _values = values;
    }

    public Matrix() : this(new double[2, 2]) { }

    private double a => _values[0, 0];
    private double b => _values[0, 1];
    private double c => _values[1, 0];
    private double d => _values[1, 1];

    public double this[int row, int column] => _values[row, column];

    public Matrix Inverse()
    {
        var det = 1d / (a * d - b * c);

        return new Matrix(new[,]
        {
            {d / det, -b / det},
            {-c / det, a / det}
        });
    }

    public static Matrix operator +(Matrix matrix1, Matrix matrix2)
    {
        return new Matrix(new[,]
        {
            {matrix1[0,0] + matrix2[0,0], matrix1[0,1] + matrix2[0,1]},
            {matrix1[1,0] + matrix2[1,0], matrix1[1,1] + matrix2[1,1]}
        });
    }

    public static Matrix operator *(double coef, Matrix matrix)
    {
        return new Matrix(new[,]
        {
            {coef * matrix[0, 0], coef * matrix[0, 1]},
            {coef * matrix[1, 0], coef * matrix[1, 1]},
        });
    }

    public static Point operator *(Matrix matrix, Point point)
    {
        return new Point(matrix[0, 0] * point.X + matrix[0, 1] * point.Y,
            matrix[1, 0] * point.X + matrix[1, 1] * point.Y);
    }

    public static Matrix operator *(Matrix matrix1, Matrix matrix2)
    {
        return new Matrix(new double[,]
        {
            {
                matrix1[0, 0] * matrix2[0, 0] + matrix1[0, 1] * matrix2[1, 0],
                matrix1[0, 0] * matrix2[0, 1] + matrix1[0, 1] * matrix2[1, 1]
            },
            {
                matrix1[1, 0] * matrix2[0, 0] + matrix1[1, 1] * matrix2[1, 0],
                matrix1[0, 0] * matrix2[1, 1] + matrix1[1, 1] * matrix2[1, 1]
            }
        });
    }

    public static Matrix operator /(Matrix matrix, double coef)
    {
        return 1d / coef * matrix;
    }
}