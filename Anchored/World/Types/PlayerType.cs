﻿using Anchored.Assets;
using Anchored.World.Components;
using Arch;
using Arch.Assets.Textures;
using Arch.Graphics.Animating;
using Arch.World;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Anchored.World.Types
{
	public class PlayerType : EntityType
	{
		public PlayerType()
		{
			Serializable = true;
		}

		public override void Create(Entity entity)
		{
			base.Create(entity);
			entity.Transform.Origin = new Vector2(8, 16);
			
			var collider = entity.AddComponent(new Collider());
			collider.MakeRect(0, 13, 16, 5);

			var mover = entity.AddComponent(new Mover(collider));
			mover.Friction = 500f;
			mover.Solids = Masks.Solid;
			
			var player = entity.AddComponent(new Player(mover));

			#region Sprite

			var sprite = entity.AddComponent(new Sprite());
			
			Animator animator = null;
			{
				var texture = TextureManager.Get("null");

				// Walk Animation
				AnimationData walkAnimData = new AnimationData();
				{
					walkAnimData.Layers.Add("Main", new List<AnimationFrame>()
					{
						new AnimationFrame()
						{
							Duration = 0.2f,
							Bounds = new Rectangle(0, 0, 16, 16),
							Texture = texture
						}
					});

					walkAnimData.Tags.Add("Main", new AnimationTag()
					{
						StartFrame = 0,
						EndFrame = 0,
						Direction = AnimationDirection.Forward
					});
				}
				var walkAnim = walkAnimData.CreateAnimation();

				animator = entity.AddComponent(new Animator(sprite, new Dictionary<string, Animation>()
				{
					{
						"Walk",
						walkAnim
					}
				}));

				animator.Play("Walk");
			}
			#endregion Animator

			var depth = entity.AddComponent(new DepthSorter(sprite));
		}
	}
}
