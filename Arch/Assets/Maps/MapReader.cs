using Arch.Assets.Maps.Serialization;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;

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

		private List<LevelJson> ReadLevels(ContentReader reader)
		{
			Int32 length = reader.ReadInt32();
			List<LevelJson> result = new List<LevelJson>();

			for (int ii = 0; ii < length; ii++)
			{
				LevelJson l = new LevelJson();
				l.Height = reader.ReadInt32();
				result.Add(l);
			}

			return result;
		}

		private List<MapEntityJson> ReadEntities(ContentReader reader)
		{
			Int32 length = reader.ReadInt32();
			List<MapEntityJson> result = new List<MapEntityJson>();

			for (int ii = 0; ii < length; ii++)
			{
				MapEntityJson e = new MapEntityJson();
				e.Name = reader.ReadString();
				e.Type = reader.ReadString();
				e.Level = reader.ReadInt32();
				e.X = reader.ReadInt32();
				e.Y = reader.ReadInt32();
				e.Z = reader.ReadInt32();
				e.Settings = ReadSettings(reader);

				result.Add(e);
			}

			return result;
		}

		private List<LayerJson> ReadLayers(ContentReader reader)
		{
			Int32 length = reader.ReadInt32();
			List<LayerJson> result = new List<LayerJson>();

			for (int ii = 0; ii < length; ii++)
			{
				LayerJson l = new LayerJson();
				l.Name = reader.ReadString();
				l.Type = reader.ReadString();
				l.ID = reader.ReadInt32();
				l.Level = reader.ReadInt32();
				l.Width = reader.ReadUInt32();
				l.Height = reader.ReadUInt32();
				l.TilesetName = reader.ReadString();
				l.Opacity = reader.ReadSingle();
				l.Repeat = reader.ReadBoolean();
				l.Distance = reader.ReadInt32();
				l.YDistance = reader.ReadInt32();
				l.TileSize = reader.ReadInt32();
				l.MoveSpeed.X = reader.ReadSingle();
				l.MoveSpeed.Y = reader.ReadSingle();

				Int32 dataLengthY = reader.ReadInt32();
				Int32 dataLengthX = reader.ReadInt32();

				l.Data = new UInt16[dataLengthX, dataLengthY];

				for (int yy = 0; yy < dataLengthY; yy++)
				{
					for (int xx = 0; xx < dataLengthX; xx++)
					{
						l.Data[yy, xx] = reader.ReadUInt16();
					}
				}

				result.Add(l);
			}

			return result;
		}

		private Dictionary<string, object> ReadSettings(ContentReader reader)
		{
			Int32 length = reader.ReadInt32();
			Dictionary<string, object> result = new Dictionary<string, object>();

			for (int ii = 0; ii < length; ii++)
			{
				string s = reader.ReadString();
				Int32 bytes = reader.ReadInt32();
				object o = reader.ReadBytes(bytes);

				result.Add(s, o);
			}

			return result;
		}
	}
}
