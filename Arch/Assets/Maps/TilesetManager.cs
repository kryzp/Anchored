using Arch.Streams;
using System.Collections.Generic;

namespace Arch.Assets.Maps
{
	public class TilesetManager
	{
		private static Dictionary<string, Tileset> maps = new Dictionary<string, Tileset>();

		public static void Load()
		{
			var mapDir = FileHandle.FromRoot("tilesets\\");

			if (mapDir.Exists())
				LoadTilesets(mapDir);
		}

		private static void LoadTilesets(FileHandle handle)
		{
			foreach (var h in handle.ListFileHandles())
				LoadTileset(h);

			foreach (var h in handle.ListDirectoryHandles())
				LoadTileset(h);
		}

		private static void LoadTileset(FileHandle handle)
		{
			string folder = handle.FullPath;
			folder = folder.Remove(handle.FullPath.Length - handle.Name.Length, handle.Name.Length);
			folder = folder.Remove(0, (AssetManager.Root + "\\tilesets").Length);

			string id = folder + handle.NameWithoutExtension;

			Tileset ts = AssetManager.Content.Load<Tileset>($"tilesets\\{id}");

			maps[id] = ts;
		}

		public static void Destroy()
		{
			maps.Clear();
		}

		public static Tileset Get(string id)
		{
			if (maps.TryGetValue(id, out var map))
				return map;

			return null;
		}
	}
}
