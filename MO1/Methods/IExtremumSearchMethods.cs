using MO1.Core;

namespace MO1.Methods;

public interface IExtremumSearchMethod
{
    public double FindMinimum(Interval minimumInterval, Func<double, double> function);
}