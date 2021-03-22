using Anchored.Debug.Console;
using Anchored.Streams;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.Assets
{
	public static class Locales
	{
		public enum ClientLanguage
		{
			None,
			En,
			Fr,
			Sp,
		}

		private static bool LoadedFallback;

		public static Dictionary<string, string> Map = null;
		public static Dictionary<string, string> Fallback = new Dictionary<string, string>();
		public static Dictionary<ClientLanguage, Dictionary<string, string>> Loaded = new Dictionary<ClientLanguage, Dictionary<string, string>>();

		public static ClientLanguage PrefferedClientLanguage = ClientLanguage.En;
		public static ClientLanguage Current = ClientLanguage.None;

		public static void Load(ClientLanguage locale)
		{
			if (!LoadedFallback)
			{
				LoadRaw(ClientLanguage.En, "locales\\en.json", true);
				LoadedFallback = true;
			}

			if (Current == locale)
				return;

			Current = locale;

			if (!Loaded.ContainsKey(locale))
				LoadRaw(locale, $"locales\\{locale}.json");

			if (Loaded.ContainsKey(locale))
				Map = Loaded[locale];
		}

		public static string Get(string key, bool eng = false)
		{
			return (!eng && Map.ContainsKey(key)) ? Map[key] : GetEnglish(key);
		}

		public static string GetEnglish(string key)
		{
			return (Fallback.ContainsKey(key)) ? Fallback[key] : key;
		}

		public static bool Contains(string key)
		{
			return Map.ContainsKey(key) || Fallback.ContainsKey(key);
		}

		private static void LoadRaw(ClientLanguage lang, string path, bool backup = false)
		{
			if (Loaded.TryGetValue(lang, out var cached))
			{
				Map = cached;
				return;
			}

			FileHandle file = FileHandle.FromRoot(path);

			if (!file.Exists())
			{
				DebugConsole.Error($"Locale \'{path}\' was now found!");
				return;
			}

			try
			{
				var root = JsonConvert.DeserializeObject<Dictionary<string, string>>(file.ReadAll());

				cached = new Dictionary<string, string>();
				Loaded[lang] = cached;

				foreach (var entry in root)
					cached[entry.Key] = entry.Value.ToString();

				if (backup)
				{
					Fallback = cached;
				}
				else
				{
					Loaded[lang] = cached;
					Map = cached;
				}
			}
			catch (Exception e)
			{
				DebugConsole.Error(e);
			}
		}
	}
}
