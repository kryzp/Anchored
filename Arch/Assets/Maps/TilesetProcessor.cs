using Microsoft.Xna.Framework.Content.Pipeline;

namespace Arch.Assets.Maps
{
	[ContentProcessor(DisplayName = "Tileset Processor - Arch")]
	public class TilesetProcessor : ContentProcessor<TilesetContentItem, TilesetContentItem>
	{
		public override TilesetContentItem Process(TilesetContentItem input, ContentProcessorContext context)
		{
			return input;
		}
	}
}
