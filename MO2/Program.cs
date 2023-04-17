using MO2;
using MO2.ArgMin;
using static System.Console;
using static System.Math;

IArgMinSeeker method = new ParabolaMethod();

var xmin = method.Find(x => 2*x*x*x - 3*x + 1);

var newtonMethod = new NewtonMethod(p =>
{
    var (x, y) = p;
    return Pow(x - 3, 2) + Pow(y-2, 2) + 1d;
}, method);
var pMin = newtonMethod.Find(new Point(13d, -2d));

Write(pMin);