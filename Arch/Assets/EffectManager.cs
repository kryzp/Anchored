using Arch.Streams;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Arch.Assets
{
	public static class EffectManager
	{
		public static Dictionary<string, Effect> Map = new Dictionary<string, Effect>();

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

			Map[handle.NameWithoutExtension] = AssetManager.Content.Load<Effect>($"fx\\{id}");
		}

		public static void Destroy()
		{
			foreach (var e in Map)
				e.Value.Dispose();

			Map.Clear();
		}

		public static Effect Get(string id)
		{
			return Map.TryGetValue(id, out var o) ? o : null;
		}
	}
}
