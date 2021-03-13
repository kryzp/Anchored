using Anchored.World.Components;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.Assets
{
	public static class Shaders
	{
		public static Effect TestEffect;

		public static void Load()
		{
			TestEffect = Effects.Get("test_effect");
		}

		public static void Begin(SpriteBatch sb, Effect effect, Camera camera = null)
		{
			sb.End();
			effect.CurrentTechnique.Passes[0].Apply();
			sb.Begin(
				SpriteSortMode.Immediate,
				samplerState: SamplerState.PointClamp,
				effect: effect,
				transformMatrix: (camera != null) ? camera.GetViewMatrix() : Camera.Main.GetViewMatrix()
			);
		}

		public static void End(SpriteBatch sb, Camera camera = null)
		{
			sb.End();
			sb.Begin(
				SpriteSortMode.FrontToBack,
				samplerState: SamplerState.PointClamp,
				transformMatrix: (camera != null) ? camera.GetViewMatrix() : Camera.Main.GetViewMatrix()
			);
		}
	}
}
