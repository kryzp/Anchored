using Anchored.World;
using Anchored.World.Components;
using Arch.World;
using Microsoft.Xna.Framework;

namespace Anchored.Util.Physics
{
	public class Hit
	{
		public Entity Other => Collider.Entity;
		public Collider Collider;
		public Vector2 Pushout;
	}
}
