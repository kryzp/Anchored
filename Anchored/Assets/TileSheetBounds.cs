using Anchored.Util;
using Microsoft.Xna.Framework;
using MonoGame.Extended.TextureAtlases;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.Assets
{
	public static class TileSheetBounds
	{
		private static Dictionary<string, Dictionary<string, Rectangle>> textureBounds = new Dictionary<string, Dictionary<string, Rectangle>>();

		// todo: this should probably be loaded from some external .json file

		/*
		{
			{
				"sheet": "<name>"
				"texture": "<name>"
				"bounds": "<bounds>"
			}
		}
		*/

		public static void Load()
		{
			Add("test_sheet", "tree1", new Rectangle(0, 16, 16*3, 16*4));

			Add("ui\\checkbox", "unchecked", new Rectangle(0, 0, 9, 9));
			Add("ui\\checkbox", "checked", new Rectangle(9, 0, 9, 9));
		}

		public static void Add(string sheet, string texture, Rectangle bounds)
		{
			if (!textureBounds.ContainsKey(sheet))
				textureBounds.Add(sheet, new Dictionary<string, Rectangle>());
			textureBounds[sheet].Add(texture, bounds);
		}

		public static Rectangle GetBounds(string sheet, string texture)
		{
			return textureBounds[sheet][texture];
		}

		public static TextureRegion Get(string sheet, string texture)
		{
			return Textures.Get($"tilesheets\\{sheet}", GetBounds(sheet, texture));
		}
	}
}
