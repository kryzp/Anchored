using System;
using Anchored.World.Components;
using Arch.Math;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arch.World.Components
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

		private Entity follow = null;

		public Entity Follow
		{
			get => follow;
			set
			{
				DoFollow = true;
				follow = value;
			}
		}
		
		public bool DoFollow = false;
		public float FollowLerp = 0.2f;
		
		public Camera()
		{
		}

		public Camera(int width, int height)
		{
			Scale.X = (int)(Engine.WindowWidth / width);
			Scale.Y = (int)(Engine.WindowHeight / height);
		}

		public Viewport GetViewport()
		{
			return new Viewport(
				(int)(Position.X - Origin.X),
				(int)(Position.Y - Origin.Y),
				(int)(Engine.WindowWidth / Scale.X),
				(int)(Engine.WindowHeight / Scale.Y)
			);
		}

		public Matrix GetViewMatrix()
		{
			return _GetViewMatrix();
		}

		public Matrix GetPerfectViewMatrix()
		{
			Matrix mat = _GetViewMatrix();

			mat.M41 = (int)mat.M41;
			mat.M42 = (int)mat.M42;
			mat.M43 = (int)mat.M43;

			return mat;
		}

		public bool Sees(GraphicsComponent sprite)
		{
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
		
		public bool SeesAdvanced(GraphicsComponent sprite)
		{
			Rectangle bounds = sprite.Texture.Texture.Bounds;
			var mat = sprite.Entity.Transform.GetMatrix();
			
			Quad worldRect = new Quad();
			RectangleF worldBounds = new RectangleF();

			var tl = new Vector2(bounds.Top, bounds.Left);
			var tr = new Vector2(bounds.Top, bounds.Right);
			var bl = new Vector2(bounds.Bottom, bounds.Left);
			var br = new Vector2(bounds.Bottom, bounds.Right);
			
			worldRect.A = Vector2.Transform(tl, mat);
			worldRect.B = Vector2.Transform(tr, mat);
			worldRect.C = Vector2.Transform(br, mat);
			worldRect.D = Vector2.Transform(bl, mat);

			worldBounds.X = MathF.Min(worldRect.A.X, MathF.Min(worldRect.B.X, MathF.Min(worldRect.C.X, worldRect.D.X)));
			worldBounds.Y = MathF.Min(worldRect.A.Y, MathF.Min(worldRect.B.Y, MathF.Min(worldRect.C.Y, worldRect.D.Y)));
			worldBounds.Width = MathF.Max(worldRect.A.X, MathF.Max(worldRect.B.X, MathF.Max(worldRect.C.X, worldRect.D.X))) - worldBounds.X;
			worldBounds.Height = MathF.Max(worldRect.A.Y, MathF.Max(worldRect.B.Y, MathF.Max(worldRect.C.Y, worldRect.D.Y))) - worldBounds.Y;
			
			if (GetViewport().Bounds.Contains(new Vector2(worldBounds.X, worldBounds.Y)) ||
				GetViewport().Bounds.Contains(new Vector2(worldBounds.X + worldBounds.Width, worldBounds.Y + worldBounds.Height)))
			{
				return true;
			}

			return false;
		}
		
		public void Update()
		{
			if (Follow != null && DoFollow)
			{
				Position = new Vector2(
					MathHelper.Lerp(Position.X, Follow.Transform.Position.X, FollowLerp),
					MathHelper.Lerp(Position.Y, Follow.Transform.Position.Y, FollowLerp)
				);
			}
		}
		
		private Matrix _GetViewMatrix()
		{
			return (
				Matrix.CreateTranslation(new Vector3(-Position.X - Entity.Transform.Position.X, -Position.Y - Entity.Transform.Position.Y, 0)) *
				Matrix.CreateRotationZ(Rotation) *
				Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
				Matrix.CreateTranslation(Origin.X, Origin.Y, 0) *
				Matrix.CreateScale(new Vector3(Scale.X, Scale.Y, 1))
			);
		}
	}
}
