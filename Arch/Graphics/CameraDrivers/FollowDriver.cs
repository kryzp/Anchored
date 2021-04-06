using Microsoft.Xna.Framework;

namespace Arch.Graphics.CameraDrivers
{
	public class FollowDriver : CameraDriver
	{
		public override void Init()
		{
		}

		public override void Destroy()
		{
		}

		public override void Update()
		{
			if (Camera.Follow != null && Camera.DoFollow)
			{
				Camera.Position = new Vector2(
					MathHelper.Lerp(Camera.Position.X, Camera.Follow.Transform.Position.X, Camera.FollowLerp),
					MathHelper.Lerp(Camera.Position.Y, Camera.Follow.Transform.Position.Y, Camera.FollowLerp)
				);
			}
		}
	}
}
