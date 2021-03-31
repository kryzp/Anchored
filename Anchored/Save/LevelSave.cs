using Anchored.World;
using Arch.Streams;
using Arch.World;

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
