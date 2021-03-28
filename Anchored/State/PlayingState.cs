using Anchored.Areas;
using Anchored.Assets;
using Anchored.Debug;
using Anchored.Debug.Console;
using Anchored.UI;
using Anchored.World;
using Anchored.World.Components;
using Microsoft.Xna.Framework.Graphics;

namespace Anchored.State
{
	public class PlayingState : GameState
	{
		private GameArea currentArea;
		private EntityWorld world;
		private Camera camera;

		public override void Load(SpriteBatch sb)
		{
			world = new EntityWorld();
			DebugConsole.World = world;

			currentArea = new TutorialIslandStartBeach(world);
			currentArea.Load(sb);
			camera = currentArea.Camera;

			Editor editor = new Editor();
			editor.Camera = camera;
			editor.World = world;
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
			currentArea.Draw(sb);
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

		public override void DrawDebug()
		{
			if (!DebugConsole.Open)
				return;

			DebugManager.Draw(world);
		}
	}
}
