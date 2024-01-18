using System;

namespace Photon.EconomicJustify.Launcher;

class PA_Factor : Command
{
    public override string Name => "pa";

    public override void Execute(Conversions engnie, int index, params string[] input)
    {
        var has_percentage = ReadPrecentageTry(ref index, input, out var percentage);
        var value = ReadInt(ref index, input);
        if (input.Length > ++index) InvalidArgument(input[index]);

        object result;
        if (has_percentage) result = Conversions.PA_Factor(percentage, value);
        else result = engnie.PA_Factor(value);

        Console.WriteLine(result);
    }
}