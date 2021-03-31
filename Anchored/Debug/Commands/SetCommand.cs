using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.Debug.Commands
{
	class SetCommand : ConsoleCommand
	{
		public SetCommand()
			: base("setvar", "set", "Sets a variable to a value", "<variable> <value>")
		{
		}

		public override void Run(string[] args)
		{
			if (args.Length == 0)
			{
				DebugConsole.Error("Not enough arguments given.");
				return;
			}

			string toSet = args[0];

			if (args.Length == 1)
			{
				DebugConsole.Error("Not enough arguments given.");
				return;
			}

			if (args.Length > 2)
			{
				DebugConsole.Error("Too many arguments given.");
				return;
			}

			string setTo = args[1];
			
			if (Boolean.TryParse(setTo, out bool parsedBoolVal))
			{
				Set<bool>(toSet, parsedBoolVal);
			}
			else if (Single.TryParse(setTo, out float parsedFloatVal))
			{
				Set<float>(toSet, parsedFloatVal);
			}
			else if (setTo[0] == '"' && setTo[setTo.Length-1] == '"')
			{
				string parsedStringVal = setTo[1..(setTo.Length-1)];
				Set<string>(toSet, parsedStringVal);
			}
			else
			{
				DebugConsole.Error("Couldn't parse input into any viable type! If you're setting it to a string, make sure to use \" at the beginning and end of the variable!");
			}
		}

		private void Set<T>(string var, T to)
		{
			DebugConsole.SetVariable(var, to);
			string name = 
				(typeof(T) == typeof(bool))
					? to.ToString().ToLower()
					: (typeof(T) == typeof(string))
						? "\"" + to.ToString() + "\""
						: to.ToString();
			
			DebugConsole.Log($"Set {var} to {name}.");
		}
	}
}
