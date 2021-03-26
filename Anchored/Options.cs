using Anchored.Save;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored
{
	public static class Options
	{
		public static KeyBinds KeyBinds = new KeyBinds();

		public static void Load()
		{
			KeyBinds.Load(SaveManager.GetOptionsFilePath());
		}

		public static void Save()
		{
			KeyBinds.Save(SaveManager.GetOptionsFilePath());
		}
	}
}
