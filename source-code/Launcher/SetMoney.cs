using System;

namespace Photon.EconomicJustify.Launcher;

class SetMoney : Command
{
    public override string Name => "money";

    public override void Execute(Conversions engnie, int index, params string[] input)
    {
        index += 1;
        if (input.Length <= index) InvalidArgument();

        if (input[index] == "no") Remove(engnie, index, input);
        else if (input[index] == "clear") Clear(engnie, index, input);
        else Add(engnie, --index, input);
    }

    private void Add(Conversions engnie, int index, params string[] input)
    {
        var first_value = ReadInt(ref index, input);
        var has_second = ReadIntTry(ref index, input, out var second_value);
        var has_value = ReadDoubleTry(ref index, input, out var value);
        var has_percentage = ReadPrecentageTry(ref index, input, out var percentage);
        if (input.Length > ++index) InvalidArgument(input[index]);

        if (!has_value)
        {
            has_second = false;
            value = second_value;
        }

        if (has_second)
        {
            if (has_percentage)
            {
                engnie.SetMoney(first_value, second_value, value, percentage);
            }
            else
            {
                engnie.SetMoney(first_value, second_value, value);
            }
        }
        else
        {
            if (has_percentage)
            {
                engnie.SetMoney(first_value, value, percentage);
            }
            else
            {
                engnie.SetMoney(first_value, value);
            }
        }
    }

    private void Remove(Conversions engnie, int index, params string[] input)
    {
        var first_value = ReadInt(ref index, input);
        var has_second = ReadIntTry(ref index, input, out var second_value);
        if (input.Length > ++index) InvalidArgument(input[index]);

        if (has_second) engnie.RemoveMoney(first_value, second_value);
        else engnie.RemoveMoney(first_value);
    }

    private void Clear(Conversions engnie, int index, params string[] input)
    {
        if (input.Length > ++index) InvalidArgument(input[index]);
        engnie.RemoveAll();
    }
}