using Anchored.Areas;
using Anchored.Debug.Console;
using Anchored.Streams;
using Anchored.World;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.Assets
{
	public class PrefabLoader
	{
		public List<PrefabData> Data = new List<PrefabData>();

		public void Load(FileReader reader)
		{
			Data.Clear();

			var count = reader.ReadInt32();
			var lastType = "";

			for (var ii = 0; ii < count; ii++)
			{
				string type = reader.ReadString();

				if (type == null)
					type = lastType;

				ReadEntity(reader, type);

				lastType = type;
			}
		}

		private void ReadEntity(FileReader reader, string type)
		{
			var t = Type.GetType($"Anchored.{type}", true, false);

			var prefab = new PrefabData();
			prefab.Type = t;

			var size = reader.ReadInt16();
			prefab.Data = new byte[size];

			for (var ii = 0; ii < size; ii++)
				prefab.Data[ii] = reader.ReadByte();

			Data.Add(prefab);
		}
	}
}
