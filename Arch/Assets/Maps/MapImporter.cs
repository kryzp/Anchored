using Arch.Assets.Maps.Serialization;
using Arch.Streams;
using Microsoft.Xna.Framework.Content.Pipeline;
using Newtonsoft.Json;

namespace Arch.Assets.Maps
{
	[ContentImporter(".amap", DefaultProcessor = "MapProcessor", DisplayName = "Map Importer - Arch")]
	public class MapImporter : ContentImporter<MapContentItem>
	{
		public override MapContentItem Import(string filename, ContentImporterContext context)
		{
			string json = new FileHandle(filename).ReadAll();
			var data = JsonConvert.DeserializeObject<MapJson>(json);
			return new MapContentItem(data);
		}
	}
}
