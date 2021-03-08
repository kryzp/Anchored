﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.Debug.Console.Commands
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
			object varToSetTo = null;

			if (args.Length == 1)
			{
				if (DebugConsole.HasVariable<bool>(toSet))
				{
					varToSetTo = !DebugConsole.GetVariable<bool>(args[0]);
					goto set;
				}

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
				varToSetTo = parsedBoolVal;
			}
			else if (Single.TryParse(setTo, out float parsedFloatVal))
			{
				varToSetTo = parsedFloatVal;
			}
			else if (setTo[0] == '"' && setTo[setTo.Length-1] == '"')
			{
				varToSetTo = setTo;
			}
			else
			{
				DebugConsole.Error("Couldn't parse input into any viable type! If you're setting it to a string, make sure to use \" at the beginning and end of the variable!");
				return;
			}

		set:
			DebugConsole.SetVariable(toSet, varToSetTo);
			DebugConsole.Log($"Set {toSet} to {varToSetTo}.");
		}
	}
}