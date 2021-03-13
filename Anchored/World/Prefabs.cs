using Anchored.Assets;
using Anchored.Debug.Console;
using Anchored.Save;
using Anchored.Streams;
using Anchored.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Anchored.World
{
	public static class Prefabs
	{
		private static Dictionary<string, Prefab> loaded = new Dictionary<string, Prefab>();
		private static List<string> paths = new List<string>();
		private static PrefabLoader saver = new PrefabLoader();

		public static void Reload()
		{
			loaded.Clear();
			Load();
		}

		public static void Load()
		{
			Load(FileHandle.FromRoot("data\\prefabs\\"));
		}

		public static Prefab Get(string id)
		{
			return (
				(loaded.TryGetValue(id, out var pfb))
					? (pfb)
					: (null)
			);
		}

		private static void Load(FileHandle handle)
		{
			if (!handle.Exists())
				return;

			if (handle.IsDirectory())
			{
				foreach (var file in handle.ListFileHandles())
					Load(file);

				foreach (var file in handle.ListDirectoryHandles())
					Load(file);

				return;
			}

			DebugConsole.Log($"Loading Prefab: \'{handle.FullPath}\'");

			try
			{
				var prefab = new Prefab();
				var stream = new FileReader(handle.FullPath);

				var version = stream.ReadInt16();

				if (version > SaveManager.Version)
				{
					DebugConsole.Error($"Unknown version: {version}");
				}
				else if (version < SaveManager.Version)
				{
					// todo: do something lol
				}

				saver.Load(stream);

				prefab.Data = ArrayUtils.Clone(saver.Data);
				saver.Data.Clear();

				loaded[handle.NameWithoutExtension] = prefab;
			}
			catch (Exception e)
			{
				DebugConsole.Error($"Failed to load Prefab: \'{handle.NameWithoutExtension}\'");
				DebugConsole.Error(e.Message);
			}
		}

		private static void OnChanged(object sender, FileSystemEventArgs args)
		{
			DebugConsole.Log($"Reloading: \'{args.FullPath}\'");
			Load(new FileHandle(args.FullPath));
		}

		public static void Destroy()
		{
			loaded.Clear();
		}
	}
}
