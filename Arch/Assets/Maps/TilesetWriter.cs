using Arch.Assets.Maps.Serialization;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arch.Assets.Maps
{
	[ContentTypeWriter]
	public class TilesetWriter : ContentTypeWriter<TilesetContentItem>
	{
		protected override void Write(ContentWriter writer, TilesetContentItem value)
		{
			writer.Write(value.Data.Texture);
			writer.Write(value.Data.TileSize);
			WriteTiles(writer, value.Data.Tiles);
		}

		public override string GetRuntimeReader(TargetPlatform targetPlatform)
		{
			return "Arch.Assets.Maps.TilesetReader, Arch";
		}

		private void WriteTiles(ContentWriter writer, List<TileJson> tiles)
		{
			writer.Write(tiles.Count);

			foreach (var tile in tiles)
			{
				writer.Write(tile.ID);
				writer.Write(tile.Type);
				writer.Write(tile.Bounds.X);
				writer.Write(tile.Bounds.Y);
				writer.Write(tile.Bounds.Width);
				writer.Write(tile.Bounds.Height);
			}
		}
	}
}
