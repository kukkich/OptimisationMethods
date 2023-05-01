using MO1;
using MO2;
using MO2.ArgMin;
using System.Globalization;
using static System.Console;
using static System.Math;

Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

IArgMinSeeker method = new ParabolaMethod();
//method = new FibonacciMethod();

Func<Point, double> function = p =>
{
    var (x, y) = p;
    return -2d / (1d + Pow((x - 1d) / 2d, 2) + Pow(y - 2d, 2)) - 1d / (1d + Pow((x - 3d) / 3d, 2) + Pow((x - 1d) / 3d, 2));
};

function = p =>
{
    var (x, y) = p;
    return 100 * Pow(y - Pow(x, 2), 2) + Pow(1d - x, 2);
};

var point = new Point(2, 2);

var newtonMethod = new NewtonMethod(function, method);
var pMin = newtonMethod.Find(point);

WriteLine(pMin);
WriteLine(MethodsConfig.FCalc);

var bfgs = new BFGS(method);

pMin = bfgs.FindMinimum(function, point, new[,]
{
    {1d, 0d},
    {0d, 1d}
});

WriteLine(pMin);
Write(MethodsConfig.FCalc);