using Arch.Assets.Maps.Serialization;

namespace Arch.Assets.Maps
{
	public class TilesetContentItem : ContentItem<TilesetJson>
	{
		public TilesetContentItem(TilesetJson data)
			: base(data)
		{
		}
	}
}
