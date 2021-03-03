using Anchored.Assets;
using Anchored.Graphics.Animating;
using Anchored.Util;
using Anchored.World.Components;
using Anchored.World.Components.Physics;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;

namespace Anchored.World.Types
{
	public class PlayerType : EntityType
	{
		public override void Create(Entity entity, UInt32 instance)
		{
			var collider = entity.AddComponent(new Collider(new RectangleF(0, 0, 16, 16)));
			collider.Transform.Origin = new Vector2(8, 8);

			var mover = entity.AddComponent(new Mover(collider));
			mover.Friction = 500f;
			mover.Solids = Masks.Solid;

			var player = entity.AddComponent(new Player(mover));

			{
				AnimationData testAnimationData = new AnimationData();
				testAnimationData.Layers.Add("Main", new List<AnimationFrame>()
				{
					new AnimationFrame()
					{
						Duration = 0.2f,
						Bounds = new Rectangle(0, 0, 16, 16),
						Texture = new TextureRegion(Textures.TEST_ANIM)
					},
					new AnimationFrame()
					{
						Duration = 0.2f,
						Bounds = new Rectangle(16, 0, 16, 16),
						Texture = new TextureRegion(Textures.TEST_ANIM)
					}
				});

				testAnimationData.Tags.Add("Main", new AnimationTag()
				{
					StartFrame = 0,
					EndFrame = 1,
					Direction = AnimationDirection.Forward
				});

				var animation = new Animation(testAnimationData);

				var animator = entity.AddComponent(new Animator(animation));
				animator.Origin = new Vector2(8, 8);
			}
		}
	}
}
