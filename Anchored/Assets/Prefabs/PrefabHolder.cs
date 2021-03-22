using System;
using System.Collections.Generic;
using System.IO;
using Anchored.Debug.Console;
using Anchored.Save;
using Anchored.Streams;
using Anchored.Util;
using Anchored.World;

namespace Anchored.Assets.Prefabs
{
    public static class PrefabHolder
    {
        private static Dictionary<string, Prefab> loaded = new Dictionary<string, Prefab>();
        private static List<string> paths = new List<string>();
        private static PrefabSaver saver = new PrefabSaver();

        public static void Reload()
        {
            loaded.Clear();
            Load();
        }
		
        public static void Load()
        {
            Load(FileHandle.FromRoot("prefabs\\"));
        }

        public static Prefab Get(string id)
        {
            return loaded.TryGetValue(id, out var fab) ? fab : null;
        }

        private static void Load(FileHandle handle) {
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

            if (handle.Extension != ".lvl")
                return;

            DebugConsole.Log($"Loading prefab: \'{handle.FullPath}\'");
			
            try
            {
                var prefab = new Prefab();
                var reader = new FileReader(handle.FullPath);

                var version = reader.ReadInt16();

                if (version > SaveManager.Version) {
                    DebugConsole.Error($"Unknown version: {version}");
                } else if (version < SaveManager.Version)
                {
                    // do something on it
                }

                if (reader.ReadByte() != (byte)SaveType.Level)
                    return;

                saver.Load(new EntityWorld(), reader);

                prefab.Datas = ArrayUtils.Clone(saver.Datas);
                saver.Datas.Clear();

                loaded[handle.NameWithoutExtension] = prefab;
            }
            catch (Exception e)
            {
                DebugConsole.Error($"Failed to load prefab: \'{handle.NameWithoutExtension}\'");
                DebugConsole.Error(e);
            }
        }
		
        private static void OnChanged(object sender, FileSystemEventArgs args)
        {
            DebugConsole.Engine($"Reloading: \'{args.FullPath}\'");
            Load(new FileHandle(args.FullPath));
        }

        public static void Destroy()
        {
            loaded.Clear();
        }
    }
}