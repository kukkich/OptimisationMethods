using MO2;
using MO2.ArgMin;
using MO3;
using MO3.Methods;
using System.Globalization;
using static System.Math;
using Barrier = MO3.Methods.Barrier;
Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

var hookeJeeves = new HookeJeeves(new FibonacciMethod());
var penalty = new Penalty(hookeJeeves);
var barrier = new Barrier(hookeJeeves);

Func<Point, double> g = p => -1 - p.X + p.Y;
Func<Point, double> h = p => p.X + p.Y;
Func<Point, double> f = p => 2 * Pow(p.X - p.Y, 2) + 14 * Pow(p.Y - 3, 2);

Func<Point, double> G = p => Pow(0.5 * (g(p) + Abs(g(p))), 1);
Func<Point, double> H = p => Pow(Abs(h(p)), 2);

var point = new Point(0d, 0d);
penalty.FindMinimum(f, H, G, point, 2d);

point = new Point(0d, 0d);
G = p => -(1d / g(p));
G = p => -Log(-g(p));
barrier.FindMinimum(f, G, point, 2d);