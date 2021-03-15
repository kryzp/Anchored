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
using Anchored.Streams;

namespace Anchored.World.Types
{
	public class PlayerType : EntityType
	{
		public override void Create(Entity entity)
		{
			base.Create(entity);

			var collider = entity.AddComponent(new Collider(new RectangleF(0, 0, 16, 16)));
			collider.Transform.Origin = new Vector2(8, 8);

			var mover = entity.AddComponent(new Mover(collider));
			mover.Friction = 500f;
			mover.Solids = Masks.Solid;

			var player = entity.AddComponent(new Player(mover));

			{
				var sprite = Textures.Get("null");

				// Walk Animation
				AnimationData walkAnimData = new AnimationData();
				{
					walkAnimData.Layers.Add("Main", new List<AnimationFrame>()
					{
						new AnimationFrame()
						{
							Duration = 0.2f,
							Bounds = new Rectangle(0, 0, 16, 16),
							Texture = sprite
						}
					});

					walkAnimData.Tags.Add("Main", new AnimationTag()
					{
						StartFrame = 0,
						EndFrame = 0,
						Direction = AnimationDirection.Forward
					});
				}
				var walkAnim = new Animation(walkAnimData);

				var animator = entity.AddComponent(new Animator(new Dictionary<string, Animation>()
				{
					{
						"Walk",
						walkAnim
					}
				}));
				animator.Origin = new Vector2(8, 8);

				animator.Play("Walk");
			}
		}
	}
}
