using Arch.Streams;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arch.Assets
{
	public static class FontManager
	{
		public static Dictionary<string, SpriteFont> Map = new Dictionary<string, SpriteFont>();

		public static void Load()
		{
			var shaderDir = FileHandle.FromRoot("fonts\\");

			if (shaderDir.Exists())
			{
				foreach (var h in shaderDir.ListFileHandles())
				{
					if (h.Extension == ".xnb")
					{
						LoadFont(h);
					}
				}
			}
		}

		public static void Destroy()
		{
			Map.Clear();
		}

		private static void LoadFont(FileHandle handle)
		{
			string folder = handle.FullPath;
			folder = folder.Remove(handle.FullPath.Length - handle.Name.Length, handle.Name.Length);
			folder = folder.Remove(0, (AssetManager.Root + "\\fonts").Length);

			string id = folder + handle.NameWithoutExtension;

			Map[handle.NameWithoutExtension] = AssetManager.Content.Load<SpriteFont>($"fonts\\{id}");
		}

		public static SpriteFont Get(string id)
		{
			return Map.TryGetValue(id, out var o) ? o : null;
		}
	}
}
