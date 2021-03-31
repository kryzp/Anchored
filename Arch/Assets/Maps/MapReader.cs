using Arch.Assets.Maps.Serialization;
using Microsoft.Xna.Framework.Content;
using System;

namespace Arch.Assets.Maps
{
	public class MapReader : ContentTypeReader<Map>
	{
		protected override Map Read(ContentReader reader, Map existingInstance)
		{
			MapJson data = new MapJson();
			data.Name = reader.ReadString();
			data.MapWidth = reader.ReadInt32();
			data.MapHeight = reader.ReadInt32();
			data.MasterLevel = reader.ReadInt32();
			data.Levels = ReadLevels(reader);
			data.Entities = ReadEntities(reader);
			data.Layers = ReadLayers(reader);

			return new Map(data);
		}

		private LevelJson[] ReadLevels(ContentReader reader)
		{
			Int32 length = reader.ReadInt32();
			LevelJson[] result = new LevelJson[length];

			for (int ii = 0; ii < length; ii++)
			{
				result[ii] = new LevelJson();
				result[ii].Height = reader.ReadInt32();
			}

			return result;
		}

		private MapEntityJson[] ReadEntities(ContentReader reader)
		{
			Int32 length = reader.ReadInt32();
			MapEntityJson[] result = new MapEntityJson[length];

			for (int ii = 0; ii < length; ii++)
			{
				result[ii] = new MapEntityJson();
				result[ii].Name = reader.ReadString();
				result[ii].Type = reader.ReadString();
				result[ii].Level = reader.ReadInt32();
				result[ii].X = reader.ReadInt32();
				result[ii].Y = reader.ReadInt32();
				result[ii].Z = reader.ReadInt32();
			}

			return result;
		}

		private LayerJson[] ReadLayers(ContentReader reader)
		{
			Int32 length = reader.ReadInt32();
			LayerJson[] result = new LayerJson[length];

			for (int ii = 0; ii < length; ii++)
			{
				result[ii] = new LayerJson();
				result[ii].Name = reader.ReadString();
				result[ii].Type = reader.ReadString();
				result[ii].ID = reader.ReadInt32();
				result[ii].Level = reader.ReadInt32();
				result[ii].Width = reader.ReadUInt32();
				result[ii].Height = reader.ReadUInt32();
				result[ii].TilesetName = reader.ReadString();
				result[ii].Opacity = reader.ReadSingle();
				result[ii].Repeat = reader.ReadBoolean();
				result[ii].Distance = reader.ReadInt32();
				result[ii].YDistance = reader.ReadInt32();
				result[ii].TileSize = reader.ReadInt32();
				result[ii].MoveSpeed.X = reader.ReadSingle();
				result[ii].MoveSpeed.Y = reader.ReadSingle();

				Int32 dataLengthY = reader.ReadInt32();
				Int32 dataLengthX = reader.ReadInt32();

				for (int yy = 0; yy < dataLengthY; yy++)
				{
					for (int xx = 0; xx < dataLengthX; xx++)
					{
						result[ii].Data[yy, xx] = reader.ReadUInt16();
					}
				}
			}

			return result;
		}
	}
}
