namespace Anchored.Graphics.Animating
{
	public enum AnimationDirection
	{
		Forward,
		Backwards,
		PingPong,
	}

	static class AnimationDirectionMethods
	{
		public static uint GetFrameId(this AnimationDirection direction, Animation animation, bool reverse)
		{
			switch (direction)
			{
				case AnimationDirection.Forward:
					return (reverse)
						? (animation.EndFrame - animation.Frame)
						: (animation.StartFrame + animation.Frame);

				case AnimationDirection.Backwards:
					return (reverse)
						? (animation.StartFrame + animation.Frame)
						: (animation.EndFrame - animation.Frame);

				default:
					uint frame;
					if (animation.PingGoingForward)
					{
						frame = animation.StartFrame + animation.Frame;

						if (frame == animation.EndFrame)
						{
							animation.PingGoingForward = false;
							animation.SkipNextFrame = true;
						}
					}
					else
					{
						frame = animation.EndFrame - animation.Frame;

						if (frame == animation.StartFrame)
						{
							animation.PingGoingForward = true;
							animation.SkipNextFrame = true;
						}
					}
					return frame;
			}
		}
	}
}
