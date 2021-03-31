using Arch.Streams;
using System.Collections.Generic;
using Arch.Assets.Maps.Serialization;
using Arch.Assets.Maps;

namespace Arch.Assets
{
	public class MapManager
	{
		private static Dictionary<string, Map> maps = new Dictionary<string, Map>();

		internal static void Load()
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

			MapJson data = AssetManager.Content.Load<MapJson>($"maps\\{id}");
			Map map = new Map(data);

			maps[id] = map;
		}

		public static void Destroy()
		{
			maps.Clear();
		}

		public static Map Get(string id)
		{
			if (maps.TryGetValue(id, out var map))
				return map;

			return null;
		}
	}
}
