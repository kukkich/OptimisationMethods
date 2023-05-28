using MO2;

namespace MO3;

public class IterationInformer
{
    public static void Inform(int iteration, int fCalculation, Point point, double value)
    {
        Console.WriteLine(
            $"{iteration} {fCalculation} {point.X:F8} {point.Y:F8} {value:E14}");
    }
}