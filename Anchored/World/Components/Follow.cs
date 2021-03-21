using Anchored.Util.Physics;
using Microsoft.Xna.Framework;

namespace Anchored.World.Components
{
	public class Follow : Component, IUpdatable
	{
		private Transform target;
		public Transform Target => target;
		public float LerpAmount;

		public int Order { get; set; } = 0;

		public Follow()
		{
		}

		public Follow(Transform target)
		{
			this.target = target;
		}

		public void Update()
		{
			// todo: maybe replace with a mover component so that if follows with physics?
			
			Entity.Transform.Position = new Vector2(
				MathHelper.Lerp(Entity.Transform.Position.X, target.Position.X, LerpAmount),
				MathHelper.Lerp(Entity.Transform.Position.Y, target.Position.Y, LerpAmount)
			);
		}
	}
}
