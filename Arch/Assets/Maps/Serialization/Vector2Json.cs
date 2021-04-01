using Newtonsoft.Json;

namespace Arch.Assets.Maps.Serialization
{
	public class Vector2Json
	{
		[JsonProperty("x")]
		public float X;

		[JsonProperty("y")]
		public float Y;

		public Vector2Json()
		{
			X = 0;
			Y = 0;
		}

		public Vector2Json(float x, float y)
		{
			X = x;
			Y = y;
		}
	}
}
