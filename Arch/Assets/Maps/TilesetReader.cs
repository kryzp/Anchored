using Arch.Assets.Maps.Serialization;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;

namespace Arch.Assets.Maps
{
	public class TilesetReader : ContentTypeReader<Tileset>
	{
		protected override Tileset Read(ContentReader reader, Tileset existingInstance)
		{
			TilesetJson data = new TilesetJson();
			data.Texture = reader.ReadString();
			data.TileSize = reader.ReadInt32();
			data.Tiles = ReadTiles(reader);

			return new Tileset(data);
		}

		private List<TileJson> ReadTiles(ContentReader reader)
		{
			List<TileJson> result = new List<TileJson>();

			Int32 count = reader.ReadInt32();

			for (int ii = 0; ii < count; ii++)
			{
				TileJson tile = new TileJson();
				tile.ID = reader.ReadUInt16();
				tile.Type = reader.ReadString();
				tile.Bounds.X = reader.ReadInt32();
				tile.Bounds.Y = reader.ReadInt32();
				tile.Bounds.Width = reader.ReadInt32();
				tile.Bounds.Height = reader.ReadInt32();

				result.Add(tile);
			}

			return result;
		}
	}
}
