using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Arch.Assets.Maps.Serialization
{
	public class TilesetJson
	{
		[JsonProperty("texture_name")]
		public String Texture;

		[JsonProperty("tile_size")]
		public Int32 TileSize;

		[JsonProperty("tiles")]
		public List<TileJson> Tiles = new List<TileJson>();
	}
}
