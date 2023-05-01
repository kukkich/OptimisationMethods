using MO1;
using System.Globalization;

Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

var function = new Func<double, double>(x => Math.Pow(x - 4d, 2d));

var minimumIntervalFinder = new MinimumIntervalFinder();

var minimumInterval = minimumIntervalFinder.FindMinimumInterval(-2, function);

Console.WriteLine($"{minimumInterval.Left} {minimumInterval.Right}");

minimumInterval.Right = 20;
minimumInterval.Left = -2;

//var dichotomyMethod = new DichotomyMethod();

//double minimum;

//minimum = dichotomyMethod.FindMinimum(minimumInterval, function);

//Console.WriteLine(minimum);

//var goldenSectionMethod = new GoldenSectionMethod();

//minimum = goldenSectionMethod.FindMinimum(minimumInterval, function);

//Console.WriteLine(minimum);

//var fibonacciMethod = new FibonacciMethod();

//minimum = fibonacciMethod.FindMinimum(minimumInterval, function);

//Console.Write(minimum);