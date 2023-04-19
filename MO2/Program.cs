using MO2;
using MO2.ArgMin;
using System.Globalization;
using static System.Console;
using static System.Math;

Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

IArgMinSeeker method = new ParabolaMethod();

var xmin = method.Find(x => 2*x*x*x - 3*x + 1);

//Func<Point, double> function = p =>
//{
//    var (x, y) = p;
//    return Pow(x - 3, 2) + Pow(y - 2, 2) + 1d;
//};

//var point = new Point(13d, -2d);

Func<Point, double> function = p =>
{
    var (x, y) = p;
    return 100*Pow(y - Pow(x, 2), 2) + Pow(1 - x, 2);
};

var point = new Point(0, 0);

var newtonMethod = new NewtonMethod(function, method);
var pMin = newtonMethod.Find(point);

WriteLine(pMin);

var bfgs = new BFGS(method);

pMin = bfgs.FindMinimum(function, point, new[,]
{
    {1d, 0d},
    {0d, 1d}
});

Write(pMin);