using Arch.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Anchored.Graphics.Particles
{
	public class Particle
	{
		public TextureRegion Texture;

		public Vector2 Position = Vector2.Zero;
		public Vector2 Velocity = Vector2.Zero;
		public Vector2 Friction = Vector2.Zero;
		public Func<Vector2, Vector2> VelocityOffset = null;

		public float Rotation;
		public float RotationVelocity;

		public Vector2 Scale = Vector2.One;
		public Vector2 ScaleVelocity = Vector2.Zero;

		public Vector2 Origin = Vector2.Zero;

		public float Alpha = 1f;
		public float AlphaVelocity = 0f;

		public bool DoColourLerp = false;
		public Color Colour = Color.White;
		public Color TargetColour = Color.Black;
		public float ColourLerpSpeed = 20f;

		public float LifeTime = 1f;
		public float CurrentLifeTime = 0f;

		public SpriteEffects SpriteEffects = SpriteEffects.None;

		public Particle()
		{
		}

		public Particle(Particle other)
		{
			this.Texture = other.Texture;

			this.Position = other.Position;
			this.Velocity = other.Velocity;
			this.Friction = other.Friction;
			this.VelocityOffset = other.VelocityOffset;

			this.Rotation = other.Rotation;
			this.RotationVelocity = other.RotationVelocity;

			this.Scale = other.Scale;
			this.ScaleVelocity = other.ScaleVelocity;

			this.Origin = other.Origin;

			this.Alpha = other.Alpha;
			this.AlphaVelocity = other.AlphaVelocity;

			this.DoColourLerp = other.DoColourLerp;
			this.Colour = other.Colour;
			this.TargetColour = other.TargetColour;
			this.ColourLerpSpeed = other.ColourLerpSpeed;

			this.LifeTime = other.LifeTime;
			this.CurrentLifeTime = other.CurrentLifeTime;

			this.SpriteEffects = other.SpriteEffects;
		}
	}
}
