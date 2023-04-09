using MO1.Core;

namespace MO1;

public class IterationInformer
{
    public static void Inform(int iteration, double x1, double x2, double a, double b, double f1Value, double f2Value)
    {
        Console.WriteLine($"{iteration} {x1} {x2} {f1Value} {f2Value} {a} {b} {b - a}");
    }

    public static void Inform(int iteration, double x, double fValue)
    {
        Console.WriteLine($"{iteration} {x} {fValue}");
    }
}