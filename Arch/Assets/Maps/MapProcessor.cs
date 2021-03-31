using Microsoft.Xna.Framework.Content.Pipeline;

namespace Arch.Assets.Maps
{
	[ContentProcessor(DisplayName = "Map Processor - Arch")]
	public class MapProcessor : ContentProcessor<MapContentItem, MapContentItem>
	{
		public override MapContentItem Process(MapContentItem input, ContentProcessorContext context)
		{
			return input;
		}
	}
}
