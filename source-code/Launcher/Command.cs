using System;

namespace Photon.EconomicJustify.Launcher;

abstract class Command
{
    public abstract string Name { get; }

    public abstract void Execute(Conversions engnie, int index, params string[] input);

    protected void InvalidArgument()
    {
        throw new ArgumentException(Name);
    }

    protected void InvalidArgument(string value)
    {
        throw new ArgumentOutOfRangeException(Name, $"Not expected input: {value}");
    }

    protected int ReadInt(ref int index, string[] input)
    {
        index += 1;
        if (input.Length <= index) InvalidArgument();

        if (!int.TryParse(input[index], out var value))
            InvalidArgument(input[index]);

        return value;
    }

    protected static bool ReadIntTry(ref int index, string[] input, out int value)
    {
        index += 1;
        if (input.Length <= index)
        {
            index -= 1;
            value = -1;
            return false;
        }

        if (int.TryParse(input[index], out value)) return true;
        else
        {
            index -= 1;
            return false;
        }
    }

    protected double ReadDouble(ref int index, string[] input)
    {
        index += 1;
        if (input.Length <= index) InvalidArgument();

        if (!double.TryParse(input[index], out var value))
            InvalidArgument(input[index]);

        return value;
    }

    protected static bool ReadDoubleTry(ref int index, string[] input, out double value)
    {
        index += 1;
        if (input.Length <= index)
        {
            index -= 1;
            value = -1;
            return false;
        }

        if (double.TryParse(input[index], out value)) return true;
        else
        {
            index -= 1;
            return false;
        }
    }

    protected Precentage ReadPrecentage(ref int index, string[] input)
    {
        index += 1;
        if (input.Length <= index) InvalidArgument();

        if (!Precentage.TryParse(input[index], out var value))
            InvalidArgument(input[index]);

        return value;
    }

    protected static bool ReadPrecentageTry(ref int index, string[] input, out Precentage value)
    {
        index += 1;
        if (input.Length <= index)
        {
            index -= 1;
            value = new Precentage();
            return false;
        }

        if (Precentage.TryParse(input[index], out value)) return true;
        else
        {
            index -= 1;
            return false;
        }
    }

    private string GetDebuggerDisplay()
    {
        return Name;
    }
}