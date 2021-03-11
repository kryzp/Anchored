using Anchored.World.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.Debug.Console.Commands
{
	public class NoClipCommand : ConsoleCommand
	{
		public NoClipCommand()
			: base("noclip", "noclip", "Disables Collisions", "<opt:bool>")
		{
		}

		public override void Run(string[] args)
		{
			if (args.Length > 1)
			{
				DebugConsole.Error("Too many arguments given.");
				return;
			}

			var mover = DebugConsole.World.GetComponent<Player>().Entity.GetComponent<Mover>();

			if (args.Length == 0)
			{
				mover.ResolveCollisions = !mover.ResolveCollisions;
			}
			else if (args.Length == 1)
			{
				if (Boolean.TryParse(args[0], out bool result))
				{
					mover.ResolveCollisions = result;
				}
			}

			if (mover.ResolveCollisions)
				DebugConsole.Log("Disabled NoClip");
			else
				DebugConsole.Log("Enabled NoClip");
		}
	}
}
