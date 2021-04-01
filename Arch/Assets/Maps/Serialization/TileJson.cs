using Newtonsoft.Json;
using System;

namespace Arch.Assets.Maps.Serialization
{
	public class TileJson
	{
		[JsonProperty("id")]
		public UInt16 ID;
		
		[JsonProperty("type")]
		public string Type;

		[JsonProperty("bounds")]
		public RectangleJson Bounds = new RectangleJson();
	}
}
