using System;
using Photon.Economy;

namespace Photon.EconomicJustify.Launcher;

class Help : Command
{
    public override string Name => "help";

    public override void Execute(Conversions engnie, int index, params string[] input)
    {
        if (input.Length > ++index) InvalidArgument(input[index]);
        else Console.WriteLine(HELP);
    }

    private const string HELP = @"
commands:

	interest-rate [percentage]	'set/get interest-rate'
	period-level y|m|d			'set period level'

	list						'show event list'

	money [no] period-index value [percentage]
	money [no] period-start period-end value [percentage]
								'set/remove events'
	money clear					'clear events'

	result period-index
	result period-start period-end
								'show result'

	version						'show app version'

factors:						'independent functions'

	af [percentage] value		'a to f factor'
	ap [percentage] value		'a to f factor'
	fa [percentage] value		'f to a factor'
	fp [percentage] value		'f to p factor'
	pa [percentage] value		'p to a factor'
	pf [percentage] value		'p to f factor'

percentage:		number[%][-[period-level]period-level]
";
}