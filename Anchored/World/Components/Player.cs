using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using Anchored.Util.Timing;

namespace Anchored.World.Components
{
	public class Player : Actor
	{
		private Mover mover;

		public const float ACCELERATION = 75f;
		public const float MAX_SPEED = 150f;

		public int Order { get; set; } = 0;

		public Player()
		{
			StartCoroutine(DoTextPrint());
		}

		public Player(Mover mover)
			: this()
		{
			this.mover = mover;
		}

		public override void Update()
		{
			base.Update();
			
			int mx = 0;
			int my = 0;

			if (Input.IsDown(Keys.W))
				my -= 1;

			if (Input.IsDown(Keys.S))
				my += 1;

			if (Input.IsDown(Keys.A))
				mx -= 1;

			if (Input.IsDown(Keys.D))
				mx += 1;

			float angle = MathF.Atan2(my, mx);

			if (mx != 0)
				mover.Velocity.X = MathF.Cos(angle) * ACCELERATION;

			if (my != 0)
				mover.Velocity.Y = MathF.Sin(angle) * ACCELERATION;

			mover.Velocity.X = MathHelper.Clamp(mover.Velocity.X, -MAX_SPEED, MAX_SPEED);
			mover.Velocity.Y = MathHelper.Clamp(mover.Velocity.Y, -MAX_SPEED, MAX_SPEED);
		}

		private IEnumerator DoTextPrint()
		{
			while (true)
			{
				Console.WriteLine("E");
				yield return new WaitForSeconds(3f);
			}
		}
	}
}
