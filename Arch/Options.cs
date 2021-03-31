using Newtonsoft.Json;
using System.Collections.Generic;

namespace Arch
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

		public static void Load(string path)
		{
			KeyBinds.Load(path);
		}

		public static void Save(string path)
		{
			KeyBinds.Save(path);
		}
	}
}
