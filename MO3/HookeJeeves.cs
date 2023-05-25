using System.Security.Cryptography.X509Certificates;
using MO1;
using MO2;
using MO2.ArgMin;

namespace MO3;

public class HookeJeeves
{
    private readonly IArgMinSeeker _argMinSeeker;

    public HookeJeeves(IArgMinSeeker argMinSeeker)
    {
        _argMinSeeker = argMinSeeker;
    }

    public Point FindMinimum(Func<Point, double> function, Point point, double delta)
    {
        Point prevPoint;
        do
        {
            do
            {
                prevPoint = point;
                point = ExploratorySearch(function, point, delta);
                delta /= 2;
            } while (prevPoint == point);

            point = PatternSearch(function, prevPoint, point);
        } while (!ShouldStop(function, prevPoint, point));
        

        return point;
    }

    private Point ExploratorySearch(Func<Point, double> function, Point point, double delta)
    {
        var value = function(point);
        point = FindX(function, point, delta, value);
        value = function(point);
        point = FindY(function, point, delta, value);

        return point;
    }

    private Point PatternSearch(Func<Point, double> function, Point prevPoint, Point point)
    {
        var lambda = _argMinSeeker.Find(l => function(prevPoint + l * (point - prevPoint)));
        return prevPoint + lambda * (point - prevPoint);
    }

    private Point MoveX(Point point, double delta)
    {
        return point with { X = point.X + delta };
    }

    private Point FindX(Func<Point, double> function, Point point, double delta, double value)
    {
        var testPoint = MoveX(point, delta);
        var testValue = function(testPoint);

        if (testValue > value)
        {
            testPoint = MoveX(point, -delta);
            testValue = function(testPoint);
            if (testValue < value)
            {
                point = testPoint;
            }
        }
        else
        {
            point = testPoint;
        }

        return point;
    }

    private Point MoveY(Point point, double delta)
    {
        return point with { Y = point.Y + delta };
    }

    private Point FindY(Func<Point, double> function, Point point, double delta, double value)
    {
        var testPoint = MoveY(point, delta);
        var testValue = function(testPoint);

        if (testValue > value)
        {
            testPoint = MoveY(point, -delta);
            testValue = function(testPoint);
            if (testValue < value)
            {
                point = testPoint;
            }
        }
        else
        {
            point = testPoint;
        }

        return point;
    }

    private bool ShouldStop(Func<Point, double> function, Point point, Point nextPoint)
    {
        return Math.Abs(function(nextPoint) - function(point)) < MethodsConfig.Eps;
    }
}