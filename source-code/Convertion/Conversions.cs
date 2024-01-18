using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;

namespace Photon.EconomicJustify;

public class Conversions
{
    public Conversions(Precentage interest)
    {
        Events = new SortedSet<Period>(new PeriodCompere());
        PeriodLevel = PeriodLevels.month;
        InterestRate = interest;
    }

    public Conversions(string interest)
    {
        Events = new SortedSet<Period>(new PeriodCompere());
        PeriodLevel = PeriodLevels.month;
        InterestRate = Precentage.Parse(interest);
    }


    public Precentage InterestRate { get; set; }

    public PeriodLevels PeriodLevel { get; set; }

    public SortedSet<Period> Events { get; }


    public void SetMoney(int period, double value)
    {
        var p = new Period(period, value);
        if (Events.Contains(p)) Events.Remove(p);
        Events.Add(p);
    }
    public void SetMoney(int period, double value, Precentage interest)
    {
        var p = new Period(period, value, interest.Real);
        if (Events.Contains(p)) Events.Remove(p);
        Events.Add(p);
    }
    public void RemoveMoney(int period)
    {
        Events.Remove(new Period(period, 0));
    }

    public void SetMoney(int start, int end, double value)
    {
        var p = new Period(start, end, value);
        if (Events.Contains(p)) Events.Remove(p);
        Events.Add(p);
    }
    public void SetMoney(int start, int end, double value, Precentage interest)
    {
        var p = new Period(start, end, value, interest.Real);
        if (Events.Contains(p)) Events.Remove(p);
        Events.Add(p);
    }
    public void RemoveMoney(int start, int end)
    {
        Events.Remove(new Period(start, end, 0));
    }

    public void RemoveAll()
    {
        Events.Clear();
    }

    public double Result(int index)
    {
        double value;
        double result = 0;

        foreach (Period period in Events)
        {
            Precentage interest;
            if (period.interest == null) interest = InterestRate;
            else interest = (double)period.interest;

            if (index < period.start)
            {
                value = period.value;
                if (!period.SingleTime) value *= PA_Factor(interest, period.PeriodTime);
                result += value * PF_Factor(interest, period.start - index);
            }
            else if (period.end < index)
            {
                value = period.value;
                if (!period.SingleTime) value *= FA_Factor(interest, period.PeriodTime);
                result += value * FP_Factor(interest, index - period.end);
            }
            else if (period.SingleTime) result += period.value;
            else
            {
                result += period.value * FA_Factor(interest, index - period.start);
                result += period.value * PA_Factor(interest, period.end - index);
            }
        }

        return result;
    }
    public double Result(int start, int end)
    {
        if (end < start)
            throw new ArgumentOutOfRangeException("start, end", "The start must be less than the end.");
        double result = Result(start);
        result *= AP_Factor(end - start);
        return result;
    }
    public Precentage Result(int period, double value)
    {
        var first = Events.FirstOrDefault();

        if (first == default)
        {
            throw new Exception("Events list is empty.");
        }

        var result = Result(first.start);
        return IPF_Factor(result, value, period - first.start);
    }


    public double PF_Factor(int n)
    {
        return 1 / Math.Pow(1 + InterestRate, n);
    }
    public static double PF_Factor(Precentage i, int n)
    {
        return 1 / Math.Pow(1 + i, n);
    }

    public double FP_Factor(int n)
    {
        return Math.Pow(1 + InterestRate, n);
    }
    public static double FP_Factor(Precentage i, int n)
    {
        return Math.Pow(1 + i, n);
    }

    public double PA_Factor(int n)
    {
        double k = Math.Pow(1 + InterestRate, n);
        return (k - 1) / (InterestRate * k);
    }
    public static double PA_Factor(Precentage i, int n)
    {
        double k = Math.Pow(1 + i, n);
        return (k - 1) / (i * k);
    }

    public double AP_Factor(int n)
    {
        double k = Math.Pow(1 + InterestRate, n);
        return InterestRate * k / (k - 1);
    }
    public static double AP_Factor(Precentage i, int n)
    {
        double k = Math.Pow(1 + i, n);
        return i * k / (k - 1);
    }

    public double AF_Factor(int n)
    {
        return InterestRate / (Math.Pow(1 + InterestRate, n) - 1);
    }
    public static double AF_Factor(Precentage i, int n)
    {
        return i / (Math.Pow(1 + i, n) - 1);
    }

    public double FA_Factor(int n)
    {
        return (Math.Pow(1 + InterestRate, n) - 1) / InterestRate;
    }
    public static double FA_Factor(Precentage i, int n)
    {
        return (Math.Pow(1 + i, n) - 1) / i;
    }

    public static double IPF_Factor(double p, double f, int n)
    {
        return Math.Pow(f / p, 1D / n) - 1;
    }

    public override string ToString()
    {
        return base.ToString();
    }
}
