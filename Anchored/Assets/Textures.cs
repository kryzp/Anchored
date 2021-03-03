using Microsoft.Xna.Framework.Graphics;

namespace Anchored.Assets
{
	public static class Textures
	{
		public static readonly Texture2D NULL;
		public static readonly Texture2D TEST_ANIM;

		static Textures()
		{
			Textures.NULL = Game1.Content.Load<Texture2D>("loosesprites/null");
			Textures.TEST_ANIM = Game1.Content.Load<Texture2D>("loosesprites/test_anim");
		}
	}
}
