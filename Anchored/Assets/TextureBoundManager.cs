using Arch.Util;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Arch.Streams;
using Arch;
using Arch.Assets.Textures;

namespace Anchored.Assets.Textures
{
	public static class TextureBoundManager
	{
		public class TextureBoundJson
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

				get
				{
					return Utility.ConvertRectangleToString(Bounds);
				}
			}
			
			public Rectangle Bounds;
		}
		
		private static Dictionary<string, List<TextureBoundJson>> textureBounds = new Dictionary<string, List<TextureBoundJson>>();

		public static void Load()
		{
			FileHandle file = FileHandle.FromRoot("data\\texture_bounds.json");
			
			if (!file.Exists())
			{
				Log.Error($"\'texture_bounds.json\' was not found!");
				return;
			}
			
			string json = file.ReadAll();
			LoadFromJson(json);
		}

		public static void Destroy()
		{
			textureBounds.Clear();
		}

		public static void Add(string sheet, string texture, Rectangle bounds)
		{
			if (!textureBounds.ContainsKey(sheet))
				textureBounds.Add(sheet, new List<TextureBoundJson>());
			textureBounds[sheet].Add(new TextureBoundJson()
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
				var bounds = JsonConvert.DeserializeObject<List<TextureBoundJson>>(json);

				foreach (var bound in bounds)
					Add(bound.Sheet, bound.Texture, bound.Bounds);
			}
			catch (Exception e)
			{
				Log.Error(e);
			}
		}
	}
}
