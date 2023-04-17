namespace MO2.ArgMin;

public interface IArgMinSeeker
{
    public double Find(Func<double, double> f);
}