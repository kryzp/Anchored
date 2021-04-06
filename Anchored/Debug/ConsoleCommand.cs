using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.Debug
{
	public abstract class ConsoleCommand
	{
		public enum Access
		{
			Everyone,
			Testers,
			Developers
		}

		public Access RunPermission = Access.Everyone;
		public string Name;
		public string ShortName;
		public string Description;
		public string Usage;

		public ConsoleCommand(string name, string shortName = "", string description = "", string usage = "")
		{
			this.Name = name;
			this.ShortName = shortName;
			this.Description = description;
			this.Usage = usage;
		}

		public abstract void Run(string[] args);

		public virtual string AutoComplete(string input)
		{
			return "";
		}
	}
}
