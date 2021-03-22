using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Anchored.World.Components
{
	public class Camera : Component, IUpdatable
	{
		public Vector2 Position = Vector2.Zero;
		public float Rotation = 0f;
		public float Zoom = 1f;
		public Vector2 Scale = Vector2.One * 4;
		public Vector2 Origin = Vector2.Zero;

		public static Camera Main;

		public int Order { get; set; } = 100;
		
		public Entity Follow = null;
		public float FollowLerp = 0.2f;
		
		public Camera()
		{
		}

		public Camera(int width, int height)
		{
			Scale.X = (int)(Game1.WINDOW_WIDTH / width);
			Scale.Y = (int)(Game1.WINDOW_HEIGHT / height);
		}

		public Viewport GetViewport()
		{
			return new Viewport(
				(int)(Position.X - Origin.X),
				(int)(Position.Y - Origin.Y),
				(int)(Game1.WINDOW_WIDTH / Scale.X),
				(int)(Game1.WINDOW_HEIGHT / Scale.Y)
			);
		}

		public Matrix GetViewMatrix()
		{
			return (
				Matrix.CreateTranslation(new Vector3(-Position.X - Entity.Transform.Position.X, -Position.Y - Entity.Transform.Position.Y, 0)) *
				Matrix.CreateRotationZ(Rotation) *
				Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
				Matrix.CreateTranslation(Origin.X, Origin.Y, 0) *
				Matrix.CreateScale(new Vector3(Scale.X, Scale.Y, 1))
			);
		}

		public Matrix GetIntPosViewMatrix()
		{
			Matrix mat = GetViewMatrix();

			mat.M41 = (int)mat.M41;
			mat.M42 = (int)mat.M42;
			mat.M43 = (int)mat.M43;

			return mat;
		}

		public bool Sees(Sprite sprite)
		{
			// todo: does not account for "advanced" matrix transformations

			Rectangle bounds = sprite.Texture.Texture.Bounds;
			
			bounds.X += (int)(sprite.Entity.Transform.Position.X);
			bounds.Y += (int)(sprite.Entity.Transform.Position.Y);

			if (GetViewport().Bounds.Contains(new Vector2(bounds.X, bounds.Y)) ||
				GetViewport().Bounds.Contains(new Vector2(bounds.X + bounds.Width, bounds.Y + bounds.Height)))
			{
				return true;
			}

			return false;
		}
		
		public void Update()
		{
			if (Follow != null)
			{
				Position = new Vector2(
					MathHelper.Lerp(Position.X, Follow.Transform.Position.X, FollowLerp),
					MathHelper.Lerp(Position.Y, Follow.Transform.Position.Y, FollowLerp)
				);
			}
		}
	}
}
