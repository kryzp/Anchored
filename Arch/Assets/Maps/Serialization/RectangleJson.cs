using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace Arch.Assets.Maps.Serialization
{
	public class RectangleJson
	{
		[JsonProperty("x")]
		public int X;

		[JsonProperty("y")]
		public int Y;

		[JsonProperty("width")]
		public int Width;

		[JsonProperty("height")]
		public int Height;

		public Rectangle ToRectangle() => new Rectangle(X, Y, Width, Height);
	}
}
