using Arch.Streams;
using System.Collections.Generic;
using Arch.Assets.Maps.Serialization;
using Arch.Assets.Maps;
using Arch.Assets;

namespace Anchored.Assets.Maps
{
	public class MapManager
	{
		private static Dictionary<string, AnchoredMap> maps = new Dictionary<string, AnchoredMap>();

		public static void Load()
		{
			var mapDir = FileHandle.FromRoot("maps\\");

			if (mapDir.Exists())
				LoadTileMaps(mapDir);
		}

		private static void LoadTileMaps(FileHandle handle)
		{
			foreach (var h in handle.ListFileHandles())
				LoadTileMap(h);

			foreach (var h in handle.ListDirectoryHandles())
				LoadTileMap(h);
		}

		private static void LoadTileMap(FileHandle handle)
		{
			string folder = handle.FullPath;
			folder = folder.Remove(handle.FullPath.Length - handle.Name.Length, handle.Name.Length);
			folder = folder.Remove(0, (AssetManager.Root + "\\maps").Length);

			string id = folder + handle.NameWithoutExtension;

			Map data = AssetManager.Content.Load<Map>($"maps\\{id}");
			AnchoredMap map = new AnchoredMap(data);

			maps[id] = map;
		}

		public static void Destroy()
		{
			maps.Clear();
		}

		public static AnchoredMap Get(string id)
		{
			if (maps.TryGetValue(id, out var map))
				return map;

			return null;
		}
	}
}
