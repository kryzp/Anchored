using Anchored.World.Components;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Anchored.Assets.Shaders;

namespace Anchored.Assets
{
	public static class ShaderHolder
	{
		public static WaveShader WaveShader;
		public static RainbowShader RainbowShader;

		public static void Load()
		{
			WaveShader = new WaveShader("wave_fx");
			RainbowShader = new RainbowShader("rainbow_fx");
		}
	}
}
