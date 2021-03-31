using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.Debug.Commands
{
	public class ReloadCommand : ConsoleCommand
	{
		public ReloadCommand()
			: base("reload", "rl", "Reloads a given asset", "<type:(map,spr,tex,fnt)> <name/all>")
		{
		}

		public override void Run(string[] args)
		{
			throw new NotImplementedException();
		}
	}
}
