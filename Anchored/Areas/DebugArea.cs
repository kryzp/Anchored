using Anchored.Assets;
using Anchored.World;
using Anchored.World.Components;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Anchored.Graphics.Particles;
using System;
using Anchored.Save;
using Anchored.Streams;
using Anchored.World.Types;

namespace Anchored.Areas
{
	public class DebugArea : GameArea
	{
		public DebugArea(EntityWorld world)
			: base(world)
		{
		}

		public override void Load(SpriteBatch sb)
		{
			base.Load(sb);

			SetupTileMap(TileMaps.Get("test_map"), true);

			var playerEntity = world.AddEntity("Player");
			{
				new PlayerType().Create(playerEntity);
				Game1.Player = playerEntity;
			}

			var dummyEntity = world.AddEntity("Dummy");
			{
				dummyEntity.Transform.Position = new Vector2(64, 64);
				dummyEntity.Transform.Origin = new Vector2(8, 16);
				
				var sprite = dummyEntity.AddComponent(new Sprite(Textures.Get("null")));
				var depth = dummyEntity.AddComponent(new DepthSorter(sprite));
			}

			var follow = cameraEntity.AddComponent(new Follow(world.GetComponent<Player>().Entity.Transform));
			{
				follow.LerpAmount = 0.2f;
			}

			/*
			var particleEmitterEntity = world.AddEntity("Particle Emitter");
			{
				particleEmitterEntity.Transform.Position = new Vector2();

				var particleType = new Particle()
				{
					Texture = Textures.Get("null"),
					Velocity = new Vector2(0f, -50f),
					VelocityOffset = (velocity) =>
					{
						float angle = MathF.Atan2(velocity.Y, velocity.X);
						return new Vector2(
							MathF.Cos(angle + Rng.Float(-15, 15)),
							MathF.Sin(angle + Rng.Float(-15, 15))
						) * 25f;
					},
					RotationVelocity = 1f,
					ScaleVelocity = Vector2.One * -1f,
					AlphaVelocity = -1f,
					Origin = new Vector2(8, 8),
					LifeTime = 1f
				};

				var emitter = particleEmitterEntity.AddComponent(new ParticleEmitter(particleType));
				emitter.ParticleSpawnInterval = 0.0001f;
				emitter.Shader = Shaders.TestEffect;
			}
			*/
		}

		public override void Unload()
		{
			base.Unload();
		}

		public override void Update()
		{
			base.Update();
		}

		public override void Draw(SpriteBatch sb)
		{
			base.Draw(sb);
		}

		public override void DrawUI(SpriteBatch sb)
		{
			base.DrawUI(sb);
		}
	}
}
