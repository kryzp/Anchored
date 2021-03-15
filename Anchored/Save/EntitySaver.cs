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
				SaveEntity(entity, writer);
			}
		}

		protected virtual void SaveEntity(Entity entity, FileWriter writer)
		{
			writer.WriteString(entity.Type.GetType().FullName.Replace("Anchored.", ""));
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
				ReadEntity(world, reader);
			}
		}

		protected virtual void ReadEntity(EntityWorld world, FileReader reader)
		{
			var type = reader.ReadString();
			var name = reader.ReadString();

			var size = reader.ReadUInt16();
			var position = reader.Position;

			try
			{
				EntityType entityType = (EntityType)Activator.CreateInstance(Type.GetType($"Anchored.{type}", true, false));

				Entity entity = world.AddEntity(name);
				entityType.Create(entity);
				entity.Load(reader);

				var sum = reader.Position - position - size;

				if (sum != 0)
					reader.Position -= sum;
			}
			catch (Exception e)
			{
				DebugConsole.Error($"Failed to load: \'{type}\'");
				DebugConsole.Error(e.Message);

				reader.Position = position + size;
			}
		}

		protected EntitySaver(SaveType type)
			: base(type)
		{
		}
	}
}
