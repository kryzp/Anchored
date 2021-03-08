using Anchored.World.Components;
using Microsoft.Xna.Framework;
using System;

namespace Anchored.World.Types
{
	public class CameraType : EntityType
	{
		public override void Create(Entity entity, UInt32 instance)
		{
			var camera = entity.AddComponent(new Camera(320, 180));
			camera.Origin = new Vector2(160, 90);
		}
	}
}
