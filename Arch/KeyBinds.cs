using Arch.Streams;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Arch
{
	public class KeyBinds
	{
		public class SimpleVirtualButton
		{
			[JsonProperty("keys")]
			public List<string> Keys = new List<string>();

			[JsonProperty("mousebuttons")]
			public List<string> MouseButtons = new List<string>();

			[JsonProperty("gamepadbuttons")]
			public List<string> GamePadButtons = new List<string>();
		}

		internal void Save(object p)
		{
			throw new NotImplementedException();
		}

		private Dictionary<string, VirtualButton> buttons = new Dictionary<string, VirtualButton>();

		public void Save(string path)
		{
			JsonSerializer serializer = new JsonSerializer();
			
			using (StreamWriter sw = new StreamWriter(path))
			{
				using (JsonWriter writer = new JsonTextWriter(sw))
				{
					serializer.Serialize(writer, buttons, typeof(Dictionary<string, VirtualButton>));
				}
			}
		}

		public void Load(string path)
		{
			FileHandle file = new FileHandle(path);

			if (!file.Exists())
			{
				Log.Error($"Locale \'{path}\' was not found!");
				return;
			}

			try
			{
				var options = JsonConvert.DeserializeObject<OptionsJsonData>(file.ReadAll());

				foreach (var pair in options.KeyBinds)
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
				Log.Error(e);
			}
		}

		public bool IsDown(string name, bool ignoreGui = false, PlayerIndex index = PlayerIndex.One)
		{
			return Input.IsDown(buttons[name], ignoreGui, index);
		}

		public bool IsPressed(string name, bool ignoreGui = false, PlayerIndex index = PlayerIndex.One)
		{
			return Input.IsPressed(buttons[name], ignoreGui, index);
		}

		public bool IsReleased(string name, bool ignoreGui = false, PlayerIndex index = PlayerIndex.One)
		{
			return Input.IsReleased(buttons[name], ignoreGui, index);
		}
	}
}
