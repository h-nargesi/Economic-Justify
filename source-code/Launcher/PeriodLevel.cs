using System;

namespace Photon.EconomicJustify.Launcher;

class PeriodLevel : Command
{
    public override string Name => "period-level";

    public override void Execute(Conversions engnie, int index, params string[] input)
    {
        index += 1;
        if (input.Length <= index) Console.WriteLine(engnie.PeriodLevel);
        else if (input.Length > index + 1) InvalidArgument(input[index + 1]);
        else engnie.PeriodLevel = Parse(input[index]);
    }

    private PeriodLevels Parse(string input) => input switch
    {
        nameof(PeriodLevels.year) or "y" => PeriodLevels.year,
        nameof(PeriodLevels.month) or "m" => PeriodLevels.month,
        nameof(PeriodLevels.day) or "d" => PeriodLevels.day,
        _ => throw new ArgumentOutOfRangeException(nameof(Name), $"Not expected direction value: {input}"),
    };
}