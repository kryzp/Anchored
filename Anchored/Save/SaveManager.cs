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
		public static string SlotName = "debug"; // temp

		public const short Version = 0;

		public static Saver[] Savers;

		public static void Init()
		{
			DebugConsole.Log($"Save directory is '{GetSaveFilePath(SlotName)}'");

			// these are here because i plan to do them in the future, so i dont forget lol

			Savers = new Saver[4];
			//Savers[(int)SaveType.Global] = new GlobalSave();
			//Savers[(int)SaveType.Game] = new GameSave();
			Savers[(int)SaveType.Level] = new LevelSave();
			//Savers[(int)SaveType.Player] = new PlayerSave();

			var saveDirectory = new FileHandle(GetSaveDirectory());

			if (!saveDirectory.Exists())
			{
				saveDirectory.MakeDirectory();
				DebugConsole.Log("Creating the save directory...");
			}
		}

		public static string GetKeybindFilePath()
		{
			return System.IO.Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
				"Anchored",
				"keybinds.json"
			);
		}

		public static string GetSaveDirectory()
		{
			return System.IO.Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
				"Anchored",
				"Saves"
			);
		}

		public static string GetSaveFilePath(string user)
		{
			return System.IO.Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
				"Anchored",
				"Saves",
				user,
				$"{user}.asav"
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

		public static void Load(EntityWorld world, SaveType saveType)
		{
			var save = GetFileHandle(GetSaveFilePath(SlotName));

			if (save.Exists())
			{
				var stream = GetReader(save.FullPath);

				var version = stream.ReadInt16();
				var readSaveType = stream.ReadByte();

				if (readSaveType != (byte)saveType)
				{
					DebugConsole.Error("Read SaveType did NOT match its loader type!");
					return;
				}

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
