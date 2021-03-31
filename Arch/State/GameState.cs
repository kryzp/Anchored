using Microsoft.Xna.Framework.Graphics;

namespace Arch.State
{
	public abstract class GameState
	{
		public abstract void Load(SpriteBatch sb);
		public abstract void Unload();
		public abstract void Update();
		public abstract void Draw(SpriteBatch sb);
		public abstract void DrawUI(SpriteBatch sb);
		public abstract void DrawDebug();
	}
}
