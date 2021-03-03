using Anchored.Util;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;

namespace Anchored.World.Components.Physics
{
	public class Mover : Component, IUpdatable
	{
		public Action<Mover, Hit> OnCollide;
		public Vector2 Velocity;
		public bool ResolveCollisions;
		public Collider Collider;
		public float Friction;
		public Masks Solids;

		public Mover()
		{
			Collider = null;
			ResolveCollisions = true;
			Friction = 0f;
			OnCollide = null;
			Solids = Masks.Solid;
		}

		public Mover(Collider collider)
		{
			Collider = collider;
			ResolveCollisions = true;
			Friction = 0f;
			OnCollide = null;
			Solids = Masks.Solid;
		}

		public void Update()
		{
			Velocity.X = Calc.Approach(Velocity.X, 0f, Friction * Time.Delta);
			Velocity.Y = Calc.Approach(Velocity.Y, 0f, Friction * Time.Delta);

			Move(Velocity * Time.Delta);
			CheckForPushout();
		}

		public void ResolveCollision(Hit hit, bool adjustSpeed = true, bool doPushout = true)
		{
			if (adjustSpeed)
			{
				var pushoutNormal = hit.Pushout.NormalizedCopy();
				var velocityNormal = Velocity.NormalizedCopy();
				var dot = MathF.Min(0f, Vector2.Dot(velocityNormal, pushoutNormal));
				var amount = pushoutNormal * Velocity.Length() * dot;
				Velocity -= amount;
			}

			if (doPushout)
			{
				Entity.Transform.Position += hit.Pushout;
			}
		}

		public bool CheckForPushout()
		{
			if (Collider != null)
			{
				if (Collider.IsValid())
				{
					Hit result = new Hit();
					Hit[] hits = new Hit[16];
					int count = Collider.OverlapsAll(Solids, ref hits, 16);

					float lengthSqr = 0f;
					for (int ii = 0; ii < count; ii++)
					{
						float nextLengthSqr = hits[ii].Pushout.LengthSquared();
						if (ii == 0 || nextLengthSqr > lengthSqr)
						{
							lengthSqr = nextLengthSqr;
							result = hits[ii];
						}
					}

					if (result.Collider != null)
					{
						if (result.Collider.IsValid())
						{
							if (ResolveCollisions)
								ResolveCollision(result, true, true);

							if (OnCollide != null)
								OnCollide(this, result);

							return true;
						}
					}
				}
			}

			return false;
		}

		public void Move(Vector2 amount)
		{
			if (Collider != null)
			{
				if (Collider.IsValid())
				{
					var maxStep = MathF.Min(
						Collider.GetWorldBounds().Width,
						Collider.GetWorldBounds().Height
					) / 4f;

					var distance = amount.Length();
					var normal = amount.NormalizedCopy();

					while (distance > 0)
					{
						var stepDist = MathF.Min(distance, maxStep);
						var step = normal * stepDist;
						distance -= stepDist;

						Entity.Transform.Position += step;
						CheckForPushout();
					}
				}
			}
			else
			{
				Entity.Transform.Position += amount;
			}
		}

		public void MoveTo(Vector2 position)
		{
			Move(position - Entity.Transform.Position);
		}
	}
}
