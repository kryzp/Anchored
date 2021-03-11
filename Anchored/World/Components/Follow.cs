using Anchored.Util.Physics;
using Microsoft.Xna.Framework;

namespace Anchored.World.Components
{
	public class Follow : Component, IUpdatable
	{
		private Transform target;
		public Transform Target => target;
		public float LerpAmount;

		public Follow()
		{
		}

		public Follow(Transform target)
		{
			this.target = target;
		}

		public void Update()
		{
			Entity.Transform.Position = new Vector2(
				MathHelper.Lerp(Entity.Transform.Position.X, target.Position.X, LerpAmount),
				MathHelper.Lerp(Entity.Transform.Position.Y, target.Position.Y, LerpAmount)
			);
		}
	}
}
