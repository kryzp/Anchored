using Anchored.Graphics.Animating;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Anchored.World.Components
{
	public class Animator : Component, IUpdatable, IDrawable
	{
		public Animation Animation { get; set; }

		public float LayerDepth = 0f;
		public Color Colour = Color.White;
		public Vector2 Origin = Vector2.Zero;

		public Animator()
		{
		}

		public Animator(Animation animation)
		{
			this.Animation = animation;
		}

		public void Update()
		{
			Animation.Update();
		}

		public void Draw(SpriteBatch sb)
		{
			Animation.Draw(
				sb,
				Entity.Transform.Position,
				Origin,
				Entity.Transform.RotationDegrees,
				LayerDepth,
				Entity.Transform.Scale,
				Colour
			);
		}
	}
}
