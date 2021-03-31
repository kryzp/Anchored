﻿using Arch.Assets.Maps.Serialization;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using System;

namespace Arch.Assets.Maps
{
	[ContentTypeWriter]
	public class MapWriter : ContentTypeWriter<MapContentItem>
	{
		protected override void Write(ContentWriter writer, MapContentItem value)
		{
			writer.Write(value.Data.Name);
			writer.Write(value.Data.MapWidth);
			writer.Write(value.Data.MapHeight);
			writer.Write(value.Data.MasterLevel);
			WriteLevels(writer, value.Data.Levels);
			WriteEntities(writer, value.Data.Entities);
			WriteLayers(writer, value.Data.Layers);
		}

		public override string GetRuntimeReader(TargetPlatform targetPlatform)
		{
			return "Arch.Assets.Maps.MapReader, Arch";
		}

		private void WriteLevels(ContentWriter writer, LevelJson[] levels)
		{
			writer.Write(levels.Length);

			foreach (var level in levels)
			{
				writer.Write(level.Height);
			}
		}

		private void WriteEntities(ContentWriter writer, MapEntityJson[] entities)
		{
			writer.Write(entities.Length);

			foreach (var entity in entities)
			{
				writer.Write(entity.Name);
				writer.Write(entity.Type);
				writer.Write(entity.Level);
				writer.Write(entity.X);
				writer.Write(entity.Y);
				writer.Write(entity.Z);
			}
		}

		private void WriteLayers(ContentWriter writer, LayerJson[] layers)
		{
			writer.Write(layers.Length);

			foreach (var layer in layers)
			{
				writer.Write(layer.Name);
				writer.Write(layer.Type);
				writer.Write(layer.ID);
				writer.Write(layer.Level);
				writer.Write(layer.Width);
				writer.Write(layer.Height);
				writer.Write(layer.TilesetName);
				writer.Write(layer.Opacity);
				writer.Write(layer.Repeat);
				writer.Write(layer.Distance);
				writer.Write(layer.YDistance);
				writer.Write(layer.TileSize);
				writer.Write(layer.MoveSpeed.X);
				writer.Write(layer.MoveSpeed.Y);

				writer.Write(layer.Data.GetLength(0));
				writer.Write(layer.Data.GetLength(1));

				foreach (UInt16 id in layer.Data)
				{
					writer.Write(id);
				}
			}
		}
	}
}
