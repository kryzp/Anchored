using Anchored.Areas;
using Microsoft.Xna.Framework.Graphics;

namespace Anchored.GameStates
{
	public class PlayingState : GameState
	{
		protected GameArea currentArea;

		public override void Load(SpriteBatch sb)
		{
			currentArea = new DebugArea();
			currentArea.Load(sb);
		}

		public override void Unload()
		{
			currentArea.Unload();
		}

		public override void Update()
		{
			currentArea.Update();
		}

		public override void Draw(SpriteBatch sb)
		{
			sb.Begin(
				SpriteSortMode.FrontToBack,
				samplerState: SamplerState.PointClamp,
				transformMatrix: Game1.Camera.GetViewMatrix()
			);

			currentArea.Draw(sb);

			sb.End();
		}

		public override void DrawUI(SpriteBatch sb)
		{
			sb.Begin(
				SpriteSortMode.FrontToBack,
				samplerState: SamplerState.PointClamp
			);

			currentArea.DrawUI(sb);

			sb.End();
		}
	}
}
