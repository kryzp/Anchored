using Anchored.Debug.Console;
using Anchored.Save;
using Anchored.Streams;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Anchored
{
	// todo: move to json?
	public static class KeyBinds
	{
		private class SimpleVirtualButton
		{
			[JsonProperty("keys")]
			public List<string> Keys = new List<string>();

			[JsonProperty("mousebuttons")]
			public List<string> MouseButtons = new List<string>();

			[JsonProperty("gamepadbuttons")]
			public List<string> GamePadButtons = new List<string>();
		}

		private static Dictionary<string, VirtualButton> buttons = new Dictionary<string, VirtualButton>();

		public static void Save()
		{
			JsonSerializer serializer = new JsonSerializer();
			
			using (StreamWriter sw = new StreamWriter(SaveManager.GetKeybindFilePath()))
			{
				using (JsonWriter writer = new JsonTextWriter(sw))
				{
					serializer.Serialize(writer, buttons, typeof(Dictionary<string, VirtualButton>));
				}
			}
		}

		public static void Load()
		{
			string path = SaveManager.GetKeybindFilePath();
			FileHandle file = new FileHandle(path);

			if (!file.Exists())
			{
				DebugConsole.Error($"Locale \'{path}\' was not found!");
				return;
			}

			try
			{
				var root = JsonConvert.DeserializeObject<Dictionary<string, SimpleVirtualButton>>(file.ReadAll());

				foreach (var pair in root)
				{
					var name = pair.Key;
					var btn = pair.Value;

					VirtualButton vBtn = new VirtualButton();

					foreach (var key in btn.Keys)
						vBtn.Keys.Add((Keys)Enum.Parse(typeof(Keys), key, true));

					foreach (var mb in btn.MouseButtons)
						vBtn.MouseButtons.Add((MouseButton)Enum.Parse(typeof(MouseButton), mb, true));

					foreach (var gpb in btn.GamePadButtons)
						vBtn.GamePadButtons.Add((GamePadButton)Enum.Parse(typeof(GamePadButton), gpb, true));

					buttons.Add(name, vBtn);
				}
			}
			catch (Exception e)
			{
				DebugConsole.Error(e);
			}
		}

		public static bool IsDown(string name, bool ignoreGui = false, PlayerIndex index = PlayerIndex.One)
		{
			return Input.IsDown(buttons[name], ignoreGui, index);
		}

		public static bool IsPressed(string name, bool ignoreGui = false, PlayerIndex index = PlayerIndex.One)
		{
			return Input.IsPressed(buttons[name], ignoreGui, index);
		}

		public static bool IsReleased(string name, bool ignoreGui = false, PlayerIndex index = PlayerIndex.One)
		{
			return Input.IsReleased(buttons[name], ignoreGui, index);
		}
	}
}
