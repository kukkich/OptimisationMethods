namespace MO1.Core;

public record struct Interval
{
    private double _leftPoint;
    private double _rightPoint;

    public Interval(double leftPoint, double rightPoint)
    {
        _leftPoint = leftPoint;
        _rightPoint = rightPoint;
    }

    public double LeftPoint
    {
        get => _leftPoint;
        set
        {
            if (_rightPoint - value > 0) _leftPoint = value;
            else
            {
                _rightPoint = _leftPoint;
                _leftPoint = value;
            }
        }
    }

    public double RightPoint
    {
        get => _rightPoint;
        set
        {
            if (value - _leftPoint > 0) _rightPoint = value;
            else
            {
                _rightPoint = _leftPoint;
                _leftPoint = value;
            }
        }
    }
}
