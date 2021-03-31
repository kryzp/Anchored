using Arch.Assets.Maps.Serialization;

namespace Arch.Assets.Maps
{
	public class MapContentItem : ContentItem<MapJson>
	{
		public MapContentItem(MapJson data)
			: base(data)
		{
		}
	}
}
