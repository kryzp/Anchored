using Anchored.Util;
using Microsoft.Xna.Framework;
using MonoGame.Extended.TextureAtlases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Anchored.Debug.Console;
using Anchored.Streams;
using Newtonsoft.Json;

namespace Anchored.Assets.Textures
{
	public static class TextureBoundManager
	{
		public class TextureBounds
		{
			[JsonProperty("sheet")]
			public string Sheet;
			
			[JsonProperty("texture")]
			public string Texture;

			[JsonProperty("bounds")]
			public string StringBounds
			{
				set
				{
					Bounds = Utility.ConvertStringToRectangle(value);
				}
			}
			
			public Rectangle Bounds;
		}
		
		private static Dictionary<string, List<TextureBounds>> textureBounds = new Dictionary<string, List<TextureBounds>>();

		public static void Load()
		{
			FileHandle file = FileHandle.FromRoot("data\\texture_bounds.json");
			
			if (!file.Exists())
			{
				DebugConsole.Error($"\'texture_bounds.json\' was not found!");
				return;
			}
			
			string json = file.ReadAll();
			LoadFromJson(json);

			Console.WriteLine("");
		}

		public static void Add(string sheet, string texture, Rectangle bounds)
		{
			if (!textureBounds.ContainsKey(sheet))
				textureBounds.Add(sheet, new List<TextureBounds>());
			textureBounds[sheet].Add(new TextureBounds()
			{
				Sheet = sheet,
				Texture = texture,
				Bounds = bounds
			});
		}

		public static Rectangle GetBounds(string sheet, string texture)
		{
			return textureBounds[sheet].Find(x => x.Texture == texture).Bounds;
		}

		public static TextureRegion Get(string sheet, string texture)
		{
			return TextureManager.Get(sheet, GetBounds(sheet, texture));
		}

		private static void LoadFromJson(string json)
		{
			try
			{
				var bounds = JsonConvert.DeserializeObject<List<TextureBounds>>(json);

				foreach (var bound in bounds)
					Add(bound.Sheet, bound.Texture, bound.Bounds);
			}
			catch (Exception e)
			{
				DebugConsole.Error(e);
			}
		}
	}
}
