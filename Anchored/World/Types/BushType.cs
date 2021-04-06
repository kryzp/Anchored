using Anchored.Assets.Textures;
using Anchored.Util;
using Anchored.World.Components;
using Arch;
using Arch.Math;
using Arch.Util;
using Arch.World;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.World.Types
{
	public class BushType : EntityType
	{
		[EntityTypeSetting("sheet")]
		public string Sheet;

		[EntityTypeSetting("texture")]
		public string Texture;

		public BushType()
		{
		}

		public override void Create(Entity entity)
		{
			base.Create(entity);
			entity.Transform.Origin = new Vector2(8, 16);

			var sprite = entity.AddComponent(new Sprite(TextureBoundManager.Get(Sheet, Texture)));

			var depth = entity.AddComponent(new DepthSorter(sprite));

			var collider = entity.AddComponent(new Collider());
			collider.MakeRect(new RectangleF(0, 12, 16, 6));
			collider.Mask = Masks.Solid;
		}
	}
}
