using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Anchored
{
	public interface IRenderable
	{
		void DrawBegin(SpriteBatch sb);
		void Draw(SpriteBatch sb);
		void DrawEnd(SpriteBatch sb);
	}
}
