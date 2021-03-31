using Newtonsoft.Json;
using System;

namespace Arch.Assets.Maps.Serialization
{
	public class MapJson
	{
		[JsonProperty("name")]
		public String Name = "";

		[JsonProperty("width")]
		public Int32 MapWidth = 0;

		[JsonProperty("height")]
		public Int32 MapHeight = 0;

		[JsonProperty("master_level")]
		public Int32 MasterLevel = 0;

		[JsonProperty("levels")]
		public LevelJson[] Levels = null;
		
		[JsonProperty("entities")]
		public MapEntityJson[] Entities = null;

		[JsonProperty("layers")]
		public LayerJson[] Layers = null;
	}
}
