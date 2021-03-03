using MonoGame.Extended.Tiled;

namespace Anchored.Assets
{
	public class Maps
	{
		public static readonly TiledMap TEST;

		static Maps()
		{
			Maps.TEST = Game1.MapContent.Load<TiledMap>("maps/test_map");
		}
	}
}
