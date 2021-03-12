using Anchored.Streams;
using Anchored.Util;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
using System.Collections.Generic;
using System.IO;

namespace Anchored.Assets
{
	public class TileMaps
	{
		private static Dictionary<string, TiledMap> maps = new Dictionary<string, TiledMap>();

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

			TiledMap tilemap = AssetManager.Content.Load<TiledMap>($"maps\\{id}");
			maps[id] = tilemap;
		}

		public static void Destroy()
		{
			maps.Clear();
		}

		public static TiledMap Get(string id)
		{
			if (maps.TryGetValue(id, out var map))
				return map;

			return null;
		}
	}
}
