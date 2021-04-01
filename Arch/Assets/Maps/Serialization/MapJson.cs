using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Arch.Assets.Maps.Serialization
{
	public class MapJson
	{
		[JsonProperty("name")]
		public String Name = "";

		[JsonProperty("map_width")]
		public Int32 MapWidth = 0;

		[JsonProperty("map_height")]
		public Int32 MapHeight = 0;

		[JsonProperty("master_level")]
		public Int32 MasterLevel = 0;
		
		[JsonProperty("levels")]
		public List<LevelJson> Levels = new List<LevelJson>();
		
		[JsonProperty("entities")]
		public List<MapEntityJson> Entities = new List<MapEntityJson>();

		[JsonProperty("layers")]
		public List<LayerJson> Layers = new List<LayerJson>();
	}
}
