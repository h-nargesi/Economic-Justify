using System;
using System.Collections.Generic;

namespace Photon.EconomicJustify;

public struct Period
{
    public Period(int period, double value)
    {
        start = end = period;
        this.value = value;
        interest = null;
    }
    public Period(int start, int end, double value)
    {
        if (end < start)
            throw new ArgumentOutOfRangeException("start, end", "The start must be less than the end.");
        this.start = start;
        this.end = end;
        this.value = value;
        interest = null;
    }
    public Period(int period, double value, double interest)
    {
        start = end = period;
        this.value = value;
        this.interest = interest;
    }
    public Period(int start, int end, double value, double interest)
    {
        if (end < start)
            throw new ArgumentOutOfRangeException("start, end", "The start must be less than the end.");
        this.start = start;
        this.end = end;
        this.value = value;
        this.interest = interest;
    }

    public int start, end;
    public double value;
    public double? interest;

    public bool SingleTime
    {
        get { return start.Equals(end); }
    }
    public int PeriodTime
    {
        get { return end - start; }
    }

    public override readonly string ToString()
    {
        var result = ToString(start);
        if (!start.Equals(end)) result += " to " + ToString(end);

        result += ":\t$ " + ToString(value);

        return result;
    }
    public override readonly bool Equals(object obj)
    {
        if (obj is Period period &&
            start == period.start &&
            end == period.end)
            return true;
        
        else return false;
    }
    public override readonly int GetHashCode()
    {
        return start.GetHashCode() ^ end.GetHashCode();
    }
    public static bool operator ==(Period left, Period right)
    {
        return left.Equals(right);
    }
    public static bool operator !=(Period left, Period right)
    {
        return !(left == right);
    }

    private static string ToString(int value)
    {
        return value < 0 ? value.ToString() : " " + value;
    }
    private static string ToString(double value)
    {
        return value < 0 ? value.ToString() : " " + value;
    }
}

public class PeriodCompere : IComparer<Period>
{
    public int Compare(Period x, Period y)
    {
        if (x.start < y.start) return -1;
        else if (x.start > y.start) return 1;
        // equal start
        else if (x.end < y.end) return -1;
        else if (x.end > y.end) return 1;
        // equal start and end
        else return 0;
    }
}