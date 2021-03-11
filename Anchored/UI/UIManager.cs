﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.UI
{
	public static class UIManager
	{
		public static UIComponent Container { get; private set; }

		static UIManager()
		{
			Container = new UIComponent();
		}

		public static void Update()
		{
			Container.Update();
		}

		public static void Draw(SpriteBatch sb)
		{
			sb.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);
			Container.Draw(sb);
			sb.End();
		}
	}
}