using Newtonsoft.Json;
using System;

namespace Arch.Assets.Maps.Serialization
{
	public class LevelJson
	{
		[JsonProperty("height")]
		public Int32 Height;
	}
}
