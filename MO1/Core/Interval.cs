namespace MO1.Core;

public record struct Interval
{
    private double _left;
    private double _right;

    public Interval(double left, double right)
    {
        _left = left;
        _right = right;
    }

    public double Left
    {
        get => _left;
        set
        {
            if (_right - value > 0) _left = value;
            else
            {
                _left = _right;
                _right = value;
            }
        }
    }

    public double Right
    {
        get => _right;
        set
        {
            if (value - _left > 0) _right = value;
            else
            {
                _right = _left;
                _left = value;
            }
        }
    }
}
