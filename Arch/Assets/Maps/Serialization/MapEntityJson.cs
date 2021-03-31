using Newtonsoft.Json;
using System;

namespace Arch.Assets.Maps.Serialization
{
	public class MapEntityJson
	{
		[JsonProperty("name")]
		public String Name;

		[JsonProperty("type")]
		public String Type;

		[JsonProperty("level")]
		public Int32 Level;

		[JsonProperty("x")]
		public Int32 X;

		[JsonProperty("y")]
		public Int32 Y;

		[JsonProperty("z")]
		public Int32 Z;
	}
}
