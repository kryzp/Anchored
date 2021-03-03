using Microsoft.Xna.Framework;
using System;

namespace Anchored
{
	public class Transform
	{
		private bool dirty = false;
		private Vector2 position = Vector2.Zero;
		private float rotation = 0f;
		private Vector2 scale = Vector2.One;
		private Vector2 origin = Vector2.Zero;
		private Matrix matrix = Matrix.Identity;

		public Action OnTransformed;

		public Vector2 Position
		{
			get => position;
			set
			{
				if (value != position)
				{
					position = value;
					MakeDirty();
				}
			}
		}

		public float RotationDegrees
		{
			get => rotation;
			set
			{
				if (value != rotation)
				{
					rotation = value;
					MakeDirty();
				}
			}
		}

		public float RotationRadians
		{
			get => MathHelper.ToRadians(rotation);
			set
			{
				if (value != MathHelper.ToRadians(rotation))
				{
					rotation = MathHelper.ToDegrees(value);
					MakeDirty();
				}
			}
		}

		public Vector2 Scale
		{
			get => scale;
			set
			{
				if (value != scale)
				{
					scale = value;
					MakeDirty();
				}
			}
		}

		public Vector2 Origin
		{
			get => origin;
			set
			{
				if (value != origin)
				{
					origin = value;
					MakeDirty();
				}
			}
		}

		public Matrix GetMatrix()
		{
			if (dirty)
			{
				{
					Matrix result = Matrix.Identity;
					
					if (Origin.X != 0f || Origin.Y != 0f)
						result = Matrix.CreateTranslation(-Origin.X, -Origin.Y, 0);
					
					if (Scale.X != 0f || Scale.Y != 0f)
						result *= Matrix.CreateScale(Scale.X, Scale.Y, 1f);
					
					if (RotationRadians != 0f)
						result *= Matrix.CreateRotationZ(RotationRadians);
					
					if (Position.X != 0f || Position.Y != 0f)
						result *= Matrix.CreateTranslation(Position.X, Position.Y, 0f);
					
					matrix = result;
				}
				dirty = false;
			}
			return matrix;
		}

		public void MakeDirty()
		{
			if (!dirty)
			{
				if (OnTransformed != null)
					OnTransformed();

				dirty = true;
			}
		}

		public Vector2 GetPositionWithoutTrigger()
		{
			return position;
		}
	}
}
