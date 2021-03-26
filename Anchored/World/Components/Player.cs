using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using Anchored.Debug.Info;

namespace Anchored.World.Components
{
	public class Player : Actor
	{
		private Mover mover;

		public const float ACCELERATION = 75f;
		public const float MAX_SPEED = 150f;

		public override int Order { get; set; } = 0;
		
		public Player()
		{
		}

		public Player(Mover mover)
			: this()
		{
			this.mover = mover;
		}

		public override void Update()
		{
			base.Update();

			if (DebugView.DebugCameraMove)
				return;

			int mx = 0;
			int my = 0;

			if (Input.IsDown("player_move_up"))
				my -= 1;

			if (Input.IsDown("player_move_down"))
				my += 1;

			if (Input.IsDown("player_move_left"))
				mx -= 1;

			if (Input.IsDown("player_move_right"))
				mx += 1;

			float angle = MathF.Atan2(my, mx);

			if (mx != 0)
				mover.Velocity.X = MathF.Cos(angle) * ACCELERATION;

			if (my != 0)
				mover.Velocity.Y = MathF.Sin(angle) * ACCELERATION;

			mover.Velocity.X = MathHelper.Clamp(mover.Velocity.X, -MAX_SPEED, MAX_SPEED);
			mover.Velocity.Y = MathHelper.Clamp(mover.Velocity.Y, -MAX_SPEED, MAX_SPEED);
		}
	}
}
