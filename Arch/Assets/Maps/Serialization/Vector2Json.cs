using Newtonsoft.Json;

namespace Arch.Assets.Maps.Serialization
{
	public class Vector2Json
	{
		[JsonProperty("x")]
		public float X;

		[JsonProperty("y")]
		public float Y;
	}
}
