using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arch
{
	public interface IRenderable
	{
		void DrawBegin(SpriteBatch sb);
		void Draw(SpriteBatch sb);
		void DrawEnd(SpriteBatch sb);
	}
}
