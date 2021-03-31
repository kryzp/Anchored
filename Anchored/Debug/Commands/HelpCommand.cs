using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.Debug.Commands
{
	class HelpCommand : ConsoleCommand
	{
		public HelpCommand()
			: base("help", "man", "Gives info about a command", "<opt:cmd>")
		{
		}

		public override void Run(string[] args)
		{
			if (args.Length == 0)
			{
				foreach (var cmd in DebugConsole.Commands)
				{
					string name = cmd.Name;
					string shortName = cmd.ShortName;
					string desc = cmd.Description;
					string args2 = cmd.Usage;

					string output = $"* {name}";

					if (shortName != "")
						output += $", {shortName}";

					if (args2 != "")
						output += $" {args2}";

					output += $" : {desc}";

					DebugConsole.Log(output);
				}
			}
			else if (args.Length == 1)
			{
				ConsoleCommand command = null;

				foreach (var cmd in DebugConsole.Commands)
				{
					if (cmd.Name == args[0] || cmd.ShortName == args[0])
					{
						command = cmd;
					}
				}

				if (command != null)
				{
					string name = command.Name;
					string shortName = command.ShortName;
					string desc = command.Description;
					string args2 = command.Usage;

					string output = $"* {name}";

					if (shortName != "")
						output += $", {shortName}";

					if (args2 != "")
						output += $" {args2}";

					output += $" : {desc}";

					DebugConsole.Log(output);
				}
			}
			else
			{
				DebugConsole.Error("Too many arguments given.");
				return;
			}
		}
	}
}
