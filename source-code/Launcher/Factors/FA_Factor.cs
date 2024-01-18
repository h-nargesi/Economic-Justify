using System;

namespace Photon.EconomicJustify.Launcher;

class FA_Factor : Command
{
    public override string Name => "fa";

    public override void Execute(Conversions engnie, int index, params string[] input)
    {
        var has_percentage = ReadPrecentageTry(ref index, input, out var percentage);
        var value = ReadInt(ref index, input);
        if (input.Length > ++index) InvalidArgument(input[index]);

        object result;
        if (has_percentage) result = Conversions.FA_Factor(percentage, value);
        else result = engnie.FA_Factor(value);

        Console.WriteLine(result);
    }
}