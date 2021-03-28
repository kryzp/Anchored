using Anchored.Graphics.TileMaps;
using Anchored.Streams;
using Anchored.Util;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;

namespace Anchored.Assets
{
	public class TileMapManager
	{
		private static Dictionary<string, TileMapData> maps = new Dictionary<string, TileMapData>();

		internal static void Load()
		{
			var mapDir = FileHandle.FromRoot("maps\\tilemaps\\");

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
			folder = folder.Remove(0, (AssetManager.Root + "\\maps\\tilemaps").Length);

			string id = folder + handle.NameWithoutExtension;

			TileMapData tilemap = AssetManager.Content.Load<TileMapData>($"maps\\tilemaps\\{id}");
			maps[id] = tilemap;
		}

		public static void Destroy()
		{
			maps.Clear();
		}

		public static TileMapData Get(string id)
		{
			if (maps.TryGetValue(id, out var map))
				return map;

			return null;
		}
	}
}
