using System;

namespace Photon.EconomicJustify;

public struct Precentage
{
    public Precentage(double value)
    {
        Real = value;
    }

    public double Real { get; private set; }

    public static implicit operator double(Precentage pr)
    {
        return pr.Real;
    }
    public static implicit operator Precentage(double value)
    {
        return new Precentage(value);
    }

    public static Precentage Parse(string value)
    {
        if (!TryParse(value, out var result))
        {
            throw new Exception("Invalid value.");
        }

        return result;
    }
    public static bool TryParse(string value, out Precentage result)
    {
        result = new Precentage();

        if (value == null || value.Length == 0)
        {
            return false;
        }

        value = value.Trim();
        var conv = value.Split('-');

        if (conv.Length > 2)
        {
            return false;
        }

        double real;
        value = conv[0].TrimEnd();
        if (value.EndsWith("%"))
        {
            if (!double.TryParse(value[..^1].TrimEnd(), out real))
            {
                return false;
            }

            real /= 100;
        }
        else if (!double.TryParse(value, out real))
        {
            return false;
        }
        result = new Precentage(real);

        if (conv.Length == 2)
        {
            if (conv[1].Length != 1 && conv[1].Length != 2)
            {
                return false;
            }

            if (conv[1].Length != 2) conv[1] = "y" + conv[1];
            PeriodLevels from, to;

            switch (conv[1][0])
            {
                case 'y': from = PeriodLevels.year; break;
                case 'm': from = PeriodLevels.month; break;
                case 'd': from = PeriodLevels.day; break;
                default: return false;
            }

            switch (conv[1][1])
            {
                case 'y': to = PeriodLevels.year; break;
                case 'm': to = PeriodLevels.month; break;
                case 'd': to = PeriodLevels.day; break;
                default: return false;
            }

            result = Convert(from, result, to);
        }

        return true;
    }
    public static Precentage Convert(PeriodLevels from, Precentage prct, PeriodLevels to)
    {
        var interest = new Precentage();
        switch (from)
        {
            case PeriodLevels.year:
                switch (to)
                {
                    case PeriodLevels.year: interest = prct; break;
                    case PeriodLevels.month: interest = prct.Real / 12; break;
                    case PeriodLevels.day: interest = prct.Real / 365; break;
                }
                break;
            case PeriodLevels.month:
                switch (to)
                {
                    case PeriodLevels.year: interest = Math.Pow(1 + prct.Real / 12, 12) - 1; break;
                    case PeriodLevels.month: interest = prct; break;
                    case PeriodLevels.day: interest = prct.Real / 30; break;
                }
                break;
            case PeriodLevels.day:
                switch (to)
                {
                    case PeriodLevels.year: interest = Math.Pow(1 + prct.Real / 365, 365) - 1; break;
                    case PeriodLevels.month: interest = Math.Pow(1 + prct.Real / 30, 30) - 1; ; break;
                    case PeriodLevels.day: interest = prct; break;
                }
                break;
        }
        return interest;
    }
    public override readonly string ToString()
    {
        return (Real * 100) + " %";
    }
}