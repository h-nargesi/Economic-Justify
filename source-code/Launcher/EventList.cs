using System;

namespace Photon.EconomicJustify.Launcher;

class EventList : Command
{
    public override string Name => "list";

    public override void Execute(Conversions engnie, int index, params string[] input)
    {
        if (input.Length > ++index) InvalidArgument(input[index]);
        else foreach(var e in engnie.Events)
        {
            Console.WriteLine(e);
        }
    }
}