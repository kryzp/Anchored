using Anchored.World.Types;
using System;
using System.Collections.Generic;

namespace Anchored.World
{
	public static class EntityTypes
	{
		private static List<int> entityIDs = new List<int>();
		private static List<EntityTypeData> entityTypes = new List<EntityTypeData>()
		{
			new EntityTypeData("Player", typeof(PlayerType)),
			new EntityTypeData("Camera", typeof(CameraType))
		};

		static EntityTypes()
		{
		}

		public static List<int> GetIDs()
		{
			entityIDs = MakeIDs();
			return entityIDs;
		}

		public static string NameOf(Int32 id)
		{
			foreach (var type in entityTypes)
			{
				if (id == type.ID)
				{
					return type.Name;
				}
			}

			return "<untitled>";
		}

		public static EntityType NewTypeOf(Int32 id)
		{
			foreach (var type in entityTypes)
			{
				if (id == type.ID)
				{
					return (EntityType)Activator.CreateInstance(type.Type);
				}
			}

			return null;
		}

		public static bool IsValid(Int32 id)
		{
			foreach (var type in entityTypes)
			{
				if (id == type.ID)
				{
					return true;
				}
			}

			return false;
		}

		public static int Hash(string str)
		{
			UInt16 hash = 7521;
			foreach (char c in str)
			{
				if (c == '\0')
					break;

				hash = (UInt16)(((hash << 5) + hash) + c);
			}
			return hash;
		}

		private static List<int> MakeIDs()
		{
			List<Int32> ids = new List<Int32>();
			foreach (var type in entityTypes)
				ids.Add(type.ID);
			return ids;
		}

		private struct EntityTypeData
		{
			public string Name;
			public Int32 ID => Hash(Name);
			public Type Type;

			public EntityTypeData(string name, Type type)
			{
				this.Name = name;
				this.Type = type;
			}
		}
	}
}
