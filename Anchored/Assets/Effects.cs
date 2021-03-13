using Anchored.Streams;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.Assets
{
	public static class Effects
	{
		public static Dictionary<string, Effect> EffectMap = new Dictionary<string, Effect>();

		public static void Load()
		{
			var shaderDir = FileHandle.FromRoot("fx\\");

			if (shaderDir.Exists())
			{
				foreach (var h in shaderDir.ListFileHandles())
				{
					if (h.Extension == ".xnb")
					{
						LoadEffect(h);
					}
				}
			}
		}

		private static void LoadEffect(FileHandle handle)
		{
			string folder = handle.FullPath;
			folder = folder.Remove(handle.FullPath.Length - handle.Name.Length, handle.Name.Length);
			folder = folder.Remove(0, (AssetManager.Root + "\\fx").Length);

			string id = folder + handle.NameWithoutExtension;

			EffectMap[handle.NameWithoutExtension] = AssetManager.Content.Load<Effect>($"fx\\{id}");
		}

		public static void Destroy()
		{
			foreach (var e in EffectMap)
				e.Value.Dispose();

			EffectMap.Clear();
		}

		public static Effect Get(string id)
		{
			return EffectMap.TryGetValue(id, out var o) ? o : null;
		}
	}
}
