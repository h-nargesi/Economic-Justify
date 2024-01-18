using System;
using Photon.Economy;

namespace Photon.EconomicJustify.Launcher;

class Version : Command
{
    public override string Name => "version";

    public override void Execute(Conversions engnie, int index, params string[] input)
    {
        if (input.Length > ++index) InvalidArgument(input[index]);
        else Console.WriteLine(Functions.GetVersion());
    }
}