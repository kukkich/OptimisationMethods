using MO2.Core;

namespace MO2;

public class IterationInformer
{
    public static void Inform(int iteration, Point point, double fValue, Point direction, double lambda, double xDiff,
        double yDiff, double fDiff, double angle, Point gradient, Matrix matrix)
    {
        Console.WriteLine(
            $"{iteration} {point.X:F8} {point.Y:F8} {fValue:E8} ({direction.X:F8},{direction.Y:F8}) {lambda:F8} {xDiff:E8} {yDiff:E8} {fDiff:E8} {angle:F8} ({gradient.X:F8},{gradient.Y:F8}) [{matrix[0, 0]:F8},{matrix[0, 1]:F8},{matrix[1, 0]:F8},{matrix[1, 1]:F8}]");
    }

    public static void Inform(int iteration, Point point, double fValue, Point direction, double lambda, double xDiff,
        double yDiff, double fDiff, double angle, Point gradient, HesseMatrix matrix)
    {
        Console.WriteLine(
            $"{iteration} {point.X:F8} {point.Y:F8} {fValue:E8} ({direction.X:F8},{direction.Y:F8}) {lambda:F8} {xDiff:E8} {yDiff:E8} {fDiff:E8} {angle:F8} ({gradient.X:F8},{gradient.Y:F8}) [{matrix[0, 0]:F8},{matrix[0, 1]:F8},{matrix[1, 0]:F8},{matrix[1, 1]:F8}]");
    }
}