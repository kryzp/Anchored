using Anchored.Areas;
using Anchored.World;
using Arch.Streams;
using Arch.World;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.Save
{
	public abstract class Saver
	{
		public abstract void Load(EntityWorld world, FileReader reader);
		public abstract void Save(EntityWorld world, FileWriter writer);

		public readonly SaveType SaveType;

		public Saver(SaveType type)
		{
			SaveType = type;
		}

		public virtual FileHandle GetHandle()
		{
			return new FileHandle(SaveManager.GetSaveFilePath(SaveManager.SlotName));
		}

		public virtual void Delete()
		{
			var handle = GetHandle();

			if (handle.Exists())
				handle.Delete();
		}
	}
}
