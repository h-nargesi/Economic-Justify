using System;

namespace Photon.EconomicJustify.Launcher;

class Result : Command
{
    public override string Name => "result";

    public override void Execute(Conversions engnie, int index, params string[] input)
    {
        var first_value = ReadInt(ref index, input);
        var has_second = ReadIntTry(ref index, input, out var second_value);
        var has_double = false;
        var second_double_value = -1D;
        if (!has_second)
        {
            has_double = ReadDoubleTry(ref index, input, out second_double_value);
        }
        if (input.Length > ++index) InvalidArgument(input[index]);

        object result;
        if (has_second) result = engnie.Result(first_value, second_value);
        else if (has_double) result = engnie.Result(first_value, second_double_value);
        else result = engnie.Result(first_value);

        Console.WriteLine(result);
    }
}