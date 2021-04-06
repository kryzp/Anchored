using Anchored.Areas;
using Anchored.Assets;
using Anchored.Debug;
using Anchored.World;
using Anchored.World.Components;
using Arch.Graphics;
using Arch.State;
using Arch.World;
using Arch.World.Components;
using Microsoft.Xna.Framework;
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
			Camera.Main = new Camera(Game1.WindowWidth / 4, Game1.WindowHeight / 4);
			Camera.Main.Origin = new Vector2(Game1.WindowWidth / 4 / 2, Game1.WindowHeight / 4 / 2);

			world = new EntityWorld();
			DebugConsole.World = world;

			currentArea = new DebugArea(world);
			currentArea.Load(sb);
			camera = Camera.Main;
		}

		public override void Unload()
		{
			currentArea.Unload();
		}

		public override void Update()
		{
			currentArea.Update();
			DebugConsole.Update();
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

			DebugConsole.Draw();
			DebugManager.Draw(world);
		}
	}
}
