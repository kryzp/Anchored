using Anchored.Streams;

namespace Anchored.World
{
	public abstract class EntityType
	{
		public bool Serializable = false;

		public virtual void Create(Entity entity)
		{
			entity.Type = this;
		}

		public virtual void Save(FileWriter stream)
		{
		}
		
		public virtual void Load(FileReader stream)
		{
		}
	}
}
