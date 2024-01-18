using System;

namespace Photon.EconomicJustify.Launcher;

class InterestRate : Command
{
    public override string Name => "interest-rate";

    public override void Execute(Conversions engnie, int index, params string[] input)
    {
        index += 1;
        if (input.Length <= index) Console.WriteLine(engnie.InterestRate);
        else if (input.Length > index + 1) InvalidArgument(input[index + 1]);
        else engnie.InterestRate = Precentage.Parse(input[index]);
    }
}