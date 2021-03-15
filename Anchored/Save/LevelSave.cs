using Anchored.Areas;
using Anchored.Streams;
using Anchored.World;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.Save
{
	public class LevelSave : EntitySaver
	{
		public override void Save(EntityWorld world, FileWriter writer)
		{
			SmartSave(world.Entities, writer);
		}
		
		public override FileHandle GetHandle()
		{
			return new FileHandle(SaveManager.GetSaveFilePath(SaveManager.SlotName));
		}

		public LevelSave()
			: base(SaveType.Level)
		{
		}
	}
}
