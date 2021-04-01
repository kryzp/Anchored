using Arch.Assets.Maps.Serialization;
using Arch.Streams;
using Microsoft.Xna.Framework.Content.Pipeline;
using Newtonsoft.Json;

namespace Arch.Assets.Maps
{
	[ContentImporter(".tst", DefaultProcessor = "TilesetProcessor", DisplayName = "Tileset Importer - Arch")]
	public class TilesetImporter : ContentImporter<TilesetContentItem>
	{
		public override TilesetContentItem Import(string filename, ContentImporterContext context)
		{
			string json = new FileHandle(filename).ReadAll();
			TilesetJson data = JsonConvert.DeserializeObject<TilesetJson>(json);
			return new TilesetContentItem(data);
		}
	}
}
