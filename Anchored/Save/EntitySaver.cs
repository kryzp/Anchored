using Anchored.Areas;
using Anchored.Debug.Console;
using Anchored.Streams;
using Anchored.World;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;

namespace Anchored.Save
{
	public abstract class EntitySaver : Saver
	{
		public static bool Loading;

		public void SmartSave(List<Entity> a, FileWriter writer)
		{
			var all = new List<Entity>();

			foreach (var e in a)
			{
				if (e != null)
				{
					if (e.Type != null)
					{
						if (e.Type.Serializable)
						{
							all.Add(e);
						}
					}
				}
			}

			writer.WriteInt32(all.Count);

			foreach (var entity in all)
			{
				SaveEntityType(entity.Type, writer);
				SaveEntity(entity, writer);
			}
		}

		protected virtual void SaveEntityType(EntityType type, FileWriter writer)
		{
			writer.WriteString(type.GetType().FullName?.Replace("Anchored.", ""));
			type.Save(writer);
		}

		protected virtual void SaveEntity(Entity entity, FileWriter writer)
		{
			writer.WriteString(entity.Name);

			writer.Cache = true;
			entity.Save(writer);
			writer.Cache = false;

			writer.WriteUInt16((UInt16)writer.CacheSize);
			writer.Flush();
		}

		public override void Load(EntityWorld world, FileReader reader)
		{
			int count = reader.ReadInt32();

			for (int ii = 0; ii < count; ii++)
			{
				var type = ReadEntityType(reader);
				ReadEntity(world, reader, type);
			}
		}

		protected virtual EntityType ReadEntityType(FileReader reader)
		{
			var type = reader.ReadString();
			EntityType entityType = (EntityType)Activator.CreateInstance(Type.GetType($"Anchored.{type}", true, false));
			
			if (entityType == null)
			{
				DebugConsole.Error($"Could not create EntityType: \'Anchored.{type}\'");
				return null;
			}

			entityType.Load(reader);

			return entityType;
		}
		
		protected virtual void ReadEntity(EntityWorld world, FileReader reader, EntityType type)
		{
			var name = reader.ReadString();
			var size = reader.ReadUInt16();
			var position = reader.Position;

			try
			{
				Entity entity = world.AddEntity(name);
				type.Create(entity);
				entity.Load(reader);

				var sum = reader.Position - position - size;

				if (sum != 0)
					reader.Position -= sum;
			}
			catch (Exception e)
			{
				DebugConsole.Error($"Failed to load: \'{type}\'");
				DebugConsole.Error(e);

				reader.Position = position + size;
			}
		}

		protected EntitySaver(SaveType type)
			: base(type)
		{
		}
	}
}
