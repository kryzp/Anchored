using Anchored.Areas;
using Anchored.Assets;
using Anchored.Debug;
using Anchored.Debug.Console;
using Anchored.Debug.Editors;
using Anchored.Util;
using Anchored.World;
using Anchored.World.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Diagnostics;

namespace Anchored.State
{
	public class PlayingState : GameState
	{
		private GameArea currentArea;
		private EntityWorld world;
		private Camera camera;

		private EditorWindow editorWindow;

		public override void Load(SpriteBatch sb)
		{
			world = new EntityWorld();
			DebugConsole.World = world;

			currentArea = new DebugArea(world);
			currentArea.Load(sb);
			camera = currentArea.Camera;

			Editor editor = new Editor();
			editor.Camera = camera;
			editor.World = world;

			editorWindow = new EditorWindow(editor);
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
				transformMatrix: camera.GetViewMatrix()
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

		public override void DrawDebug()
		{
			if (!DebugConsole.Open)
				return;

			editorWindow?.Draw();
			DebugManager.Draw(world);
		}
	}
}
