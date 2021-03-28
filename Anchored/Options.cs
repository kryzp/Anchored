using Anchored.Save;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored
{
	public class OptionsJsonData
	{
		[JsonProperty("keybinds")]
		public Dictionary<string, KeyBinds.SimpleVirtualButton> KeyBinds;
	}

	public static class Options
	{
		public static KeyBinds KeyBinds;

		static Options()
		{
			KeyBinds = new KeyBinds();
		}

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
