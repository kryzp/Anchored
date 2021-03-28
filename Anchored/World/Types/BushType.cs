﻿using Anchored.Math;
using Anchored.Util;
using Anchored.World.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.World.Types
{
	public class BushType : EntityType
	{
		private TextureRegion texture;

		public BushType(TextureRegion texture)
		{
			this.texture = texture;
		}

		public override void Create(Entity entity)
		{
			base.Create(entity);
			entity.Transform.Origin = new Vector2(8, 16);

			var sprite = entity.AddComponent(new Sprite(texture));

			var depth = entity.AddComponent(new DepthSorter(sprite));

			var collider = entity.AddComponent(new Collider());
			collider.MakeRect(new RectangleF(0, 12, 16, 6));
			collider.Mask = Masks.Solid;
		}
	}
}