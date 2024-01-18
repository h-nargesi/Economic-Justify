using System;
using System.Linq;
using Microsoft.VisualBasic;
using Photon.EconomicJustify.Launcher;

namespace Photon.EconomicJustify;

class Program
{
    static Conversions engine = null;
    static string Complete = string.Empty;

    [STAThread]
    static void Main()
    {
        Console.WriteLine("Economy Factors Convertion");
        var command_launcher = new CommandLauncher();

        do
        {
            try
            {
                if (engine == null)
                {
                    Console.Write("interest-rate: ");
                    engine = new Conversions(Console.ReadLine());
                }

                PrintInfo();
                var command = Complete + Console.ReadLine();

                if (string.IsNullOrWhiteSpace(command)) continue;

                var completing = command.EndsWith('?');
                if (completing)
                {
                    command = command.Remove(command.Length - 1);
                }

                var command_sequence = command.Split(' ')
                    .Where(x => !string.IsNullOrEmpty(x))
                    .ToArray();

                if (!command_sequence.Any())
                {
                    continue;
                }

                if (completing)
                {
                    var comment = command_launcher.Commplete(ref command_sequence[^1]);
                    if (!string.IsNullOrWhiteSpace(comment)) Console.WriteLine(comment);
                    Complete = string.Join(' ', command_sequence);
                }
                else
                {
                    Complete = string.Empty;
                    command_launcher.Execute(engine, command_sequence);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
        while (true);
    }

    static void PrintInfo()
    {
        Console.Write($"({engine.Events.Count}) {engine.InterestRate} {Complete}");
    }

    static void HandleException(Exception ex)
    {
        var color = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(ex.Message);
        Console.WriteLine(ex.StackTrace);
        Console.ForegroundColor = color;
        Console.WriteLine();
    }
}
