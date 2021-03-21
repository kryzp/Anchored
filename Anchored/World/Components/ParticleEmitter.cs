using Anchored.Assets;
using Anchored.Graphics.Particles;
using Anchored.Util.Math;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Text;

namespace Anchored.World.Components
{
	public class ParticleEmitter : Component, IUpdatable, IRenderable
	{
		private HashSet<Particle> ToRemove = new HashSet<Particle>();
		public HashSet<Particle> Particles = new HashSet<Particle>();

		public Particle ParticleType;
		public float ParticleSpawnInterval = 0.5f;

		public Shader Shader = null;
		public float LayerDepth = 0f;
		
		public int Order { get; set; } = 0;

		public ParticleEmitter()
		{
		}

		public ParticleEmitter(Particle particleType)
		{
			this.ParticleType = particleType;
		}

		public override void Init()
		{
		}

		public void Update()
		{
			if (Time.OnInterval(ParticleSpawnInterval, 0f))
			{
				var particle = new Particle(ParticleType);
				
				if (particle.VelocityOffset != null)
					particle.Velocity += particle.VelocityOffset(particle.Velocity);

				Particles.Add(particle);
			}

			foreach (var particle in Particles)
			{
				particle.CurrentLifeTime += Time.Delta;
				if (particle.CurrentLifeTime >= particle.LifeTime)
				{
					ToRemove.Add(particle);
					continue;
				}

				particle.Velocity.X = Calc.Approach(particle.Velocity.X, 0f, particle.Friction.X * Time.Delta);
				particle.Velocity.Y = Calc.Approach(particle.Velocity.Y, 0f, particle.Friction.Y * Time.Delta);
				
				particle.Position += particle.Velocity * Time.Delta;
				particle.Rotation += particle.RotationVelocity * Time.Delta;
				particle.Scale += particle.ScaleVelocity * Time.Delta;

				particle.Alpha += particle.AlphaVelocity * Time.Delta;
				particle.Alpha = MathHelper.Clamp(particle.Alpha, 0f, 1f);

				if (particle.DoColourLerp)
					particle.Colour = Color.Lerp(particle.Colour, particle.TargetColour, particle.ColourLerpSpeed * Time.Delta);
			}

			ResolveRemoving();
		}

		public void DrawBegin(SpriteBatch sb)
		{
		}

		public void Draw(SpriteBatch sb)
		{
			Shader?.Begin(sb);

			foreach (Particle particle in Particles)
			{
				sb.Draw(
					particle.Texture.Texture,
					particle.Position + Entity.Transform.Position,
					particle.Texture.Source,
					particle.Colour * particle.Alpha,
					particle.Rotation,
					particle.Origin,
					particle.Scale,
					particle.SpriteEffects,
					LayerDepth
				);
			}

			Shader?.End(sb);
		}

		public void DrawEnd(SpriteBatch sb)
		{
		}

		private void ResolveRemoving()
		{
			foreach (Particle particle in ToRemove)
			{
				Particles.Remove(particle);
			}

			ToRemove.Clear();
		}
	}
}
