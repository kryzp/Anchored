using Anchored.Assets;
using Anchored.Graphics.Animating;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Anchored.World.Components
{
	public class Animator : Component, IUpdatable, IRenderable
	{
		private Dictionary<string, Animation> animations = new Dictionary<string, Animation>();

		public Animation CurrentAnimation { get; set; }

		public float LayerDepth = 0f;
		public Color Colour = Color.White;
		public Vector2 Origin = Vector2.Zero;

		public bool Paused { get; private set; }

		public Animator()
		{
		}

		public Animator(Dictionary<string, Animation> animations)
		{
			this.animations = animations;
		}

		public void Update()
		{
			if (Paused)
				return;

			CurrentAnimation.Update();
		}

		public void Draw(SpriteBatch sb)
		{
			CurrentAnimation.Draw(
				sb,
				Entity.Transform.Position,
				Origin,
				Entity.Transform.RotationDegrees,
				LayerDepth,
				Entity.Transform.Scale,
				Colour
			);
		}

		public void Play(string name)
		{
			CurrentAnimation = animations[name];
		}

		public void Pause()
		{
			Paused = true;
		}
	}
}
