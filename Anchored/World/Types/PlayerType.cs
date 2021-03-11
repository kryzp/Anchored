using Anchored.Assets;
using Anchored.Graphics.Animating;
using Anchored.Util;
using Anchored.World.Components;
using Anchored.Util.Physics;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using MonoGame.Extended.Shapes;

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
				var sprite = Textures.Get("test_anim");

				AnimationData walkAnimData = new AnimationData();
				walkAnimData.Layers.Add("Main", new List<AnimationFrame>()
				{
					new AnimationFrame()
					{
						Duration = 0.2f,
						Bounds = new Rectangle(0, 0, 16, 16),
						Texture = sprite
					},
					new AnimationFrame()
					{
						Duration = 0.2f,
						Bounds = new Rectangle(16, 0, 16, 16),
						Texture = sprite
					}
				});

				walkAnimData.Tags.Add("Main", new AnimationTag()
				{
					StartFrame = 0,
					EndFrame = 1,
					Direction = AnimationDirection.Forward
				});

				var walkAnim = new Animation(walkAnimData);

				var animator = entity.AddComponent(new Animator(walkAnim));
				animator.Origin = new Vector2(8, 8);
			}
		}
	}
}
