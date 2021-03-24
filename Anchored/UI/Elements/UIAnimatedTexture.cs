using Anchored.Graphics.Animating;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.UI.Elements
{
	public class UIAnimatedTexture : UIComponent
	{
		public Animation Animation { get; set; }

		public bool Paused { get; private set; }
		public float LayerDepth = 0.95f;

		public UIAnimatedTexture()
		{
		}

		public UIAnimatedTexture(Animation animation)
		{
			this.Animation = animation;
		}

		public override void Update()
		{
			base.Update();

			if (Paused)
				return;

			Animation.Update();
		}

		public override void Draw(SpriteBatch sb)
		{
			base.Draw(sb);

			Animation.Draw(
				sb,
				Position,
				Vector2.Zero,
				0f,
				LayerDepth,
				Size / new Vector2(Animation.CurrentFrame.Bounds.Width, Animation.CurrentFrame.Bounds.Height),
				Color.White,
				SpriteEffects.None
			);
		}

		public void Pause()
		{
			Paused = true;
		}

		public void Resume()
		{
			Paused = false;
		}
	}
}
