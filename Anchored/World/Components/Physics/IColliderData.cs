
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace Anchored.World.Components.Physics
{
	public interface IColliderData
	{
		public void Project(Vector2 axis, ref float min, ref float max);
		public void UpdateWorldBounds(Matrix mat, ref RectangleF worldBounds, ref Vector2[] axis, ref Vector2[] points, ref int axisCount, ref int pointCount);
	}
}
