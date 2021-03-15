using Anchored.Debug.Console;
using Anchored.Streams;
using Anchored.World;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Anchored.Save
{
	public static class SaveManager
	{
		public static byte CurrentSlot = 0;
		public static string SlotName = "Debug"; // temp

		public const short Version = 0;

		public static Saver[] Savers;

		public static void Init()
		{
			DebugConsole.Log($"Save directory is '{GetSaveFilePath(SlotName)}'");

			Savers = new Saver[5];
			//Savers[(int)SaveType.Global] = new GlobalSave();
			//Savers[(int)SaveType.Game] = new GameSave();
			Savers[(int)SaveType.Level] = new LevelSave();
			//Savers[(int)SaveType.Player] = new PlayerSave();
			//Savers[(int)SaveType.Statistics] = new StatisticsSaver();

			var saveDirectory = new FileHandle(GetSaveFilePath(SlotName));

			if (!saveDirectory.Exists())
			{
				saveDirectory.MakeDirectory();
				DebugConsole.Log("Creating the save directory...");
			}
		}
		public static string GetSaveFilePath(string user)
		{
			return System.IO.Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
				"Anchored",
				"Saves",
				user
			);
		}

		public static Saver ForType(SaveType type)
		{
			return Savers[(int)type];
		}

		public static void Save(EntityWorld world, SaveType saveType)
		{
			var p = GetSaveFilePath(SlotName);
			var file = new FileInfo(p);

			file.Directory?.Create();

			var stream = GetWriter(p);

			stream.WriteInt16(Version);
			stream.WriteByte((byte)saveType);

			ForType(saveType).Save(world, stream);
			stream.Close();
		}

		public static void Load(EntityWorld world, SaveType saveType, string path = null)
		{
			var save = GetFileHandle(GetSaveFilePath(SlotName));

			if (save.Exists())
			{
				var stream = GetReader(save.FullPath);

				var version = stream.ReadInt16();
				stream.ReadByte();

				ForType(saveType).Load(world, stream);
			}
		}

		public static FileHandle GetFileHandle(string path)
		{
			return new FileHandle(path);
		}

		private static FileWriter GetWriter(string path)
		{
			return new FileWriter(path);
		}

		private static FileReader GetReader(string path)
		{
			return new FileReader(path);
		}
	}
}
