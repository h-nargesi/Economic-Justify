using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Photon.EconomicJustify.Launcher;

class CommandLauncher : Command
{
    private readonly Dictionary<string, Command> commands = new();

    public CommandLauncher()
    {
        var type = typeof(Command);

        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(type.IsAssignableFrom)
            .Where(t => t != typeof(CommandLauncher))
            .Where(t => t != typeof(Command));

        foreach (var t in types)
        {
            var command = (Command)Activator.CreateInstance(t);
            commands.Add(command.Name, command);
        }
    }

    public override string Name => string.Empty;

    public void Execute(Conversions engnie, params string[] input)
    {
        Execute(engnie, -1, input);
    }

    public override void Execute(Conversions engnie, int index, params string[] input)
    {
        if (commands.TryGetValue(input[++index], out var command))
        {
            command.Execute(engnie, index, input);
        }
        else throw new Exception($"Command not found ({input[index - 1]})");
    }

    public string Commplete(ref string key)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            return "no command is available!";
        }

        var available_commands = new List<string>();
        foreach(var command_keys in commands.Keys)
        {
            if (command_keys.StartsWith(key))
            {
                available_commands.Add(command_keys);
            }
        }
        
        switch(available_commands.Count)
        {
            case 0:
                return "no command is available!";
            case 1:
                key = available_commands[0];
                return string.Empty;
            default:
                return string.Join(' ', available_commands);
        }
    }
}