using Arch.Assets.Maps.Serialization;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using System;
using System.Collections.Generic;

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

		private void WriteLevels(ContentWriter writer, List<LevelJson> levels)
		{
			writer.Write(levels.Count);

			foreach (var level in levels)
			{
				writer.Write(level.Height);
			}
		}

		private void WriteEntities(ContentWriter writer, List<MapEntityJson> entities)
		{
			writer.Write(entities.Count);

			foreach (var entity in entities)
			{
				writer.Write(entity.Name);
				writer.Write(entity.Type);
				writer.Write(entity.Level);
				writer.Write(entity.X);
				writer.Write(entity.Y);
				writer.Write(entity.Z);
				WriteSettings(writer, entity.Settings);
			}
		}

		private void WriteLayers(ContentWriter writer, List<LayerJson> layers)
		{
			writer.Write(layers.Count);

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

		private void WriteSettings(ContentWriter writer, Dictionary<string, object> settings)
		{
			unsafe
			{
				writer.Write(settings.Count);

				foreach (var setting in settings)
				{
					var key = setting.Key;
					var val = setting.Value;

					var bytes = Utility.ToByteArray<object>(val);

					writer.Write(key);
					writer.Write(bytes.Length);
					writer.Write(bytes);
				}
			}
		}
	}
}
