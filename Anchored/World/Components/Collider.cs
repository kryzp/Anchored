using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Linq;
using Anchored.Util;
using Microsoft.Xna.Framework.Graphics;
using Anchored.Physics;
using Anchored.Math;
using MonoGame.Extended.Shapes;
using Anchored.Debug;
using System.Collections.Generic;
using Anchored.Debug.Console;

// note to anyone reading:
// im super proud of myself for making this lol
// didn't know anything about matrices and physics systems (super annoying to research good algorithm implementations for them that work well
// with my setup, most guides just give the general theory behind it)
// when i started so im really happy with how it turned out!
// its based on SAT (Seperating Axis Theorem) collisions and not AABB (Axis Aligned Bounding Boxes) collision
// so it can handle any shape i need it to so long as i define a check function and a resulution function for it and it isn't concave!
// (concave shapes can be created by just combining different colliders together)

namespace Anchored.World.Components
{
	public class Collider : Component, IDrawable
	{
		public enum ColliderType
		{
			Rect,
			Circle,
			Line,
			Polygon
		}

		private UInt32 entityMovementID;
		private RectangleF worldBounds;
		private List<Vector2> axis;
		private List<Vector2> points;
		private int axisCount;
		private int pointCount;
		private ColliderType currentColliderType;
		private IColliderData data;

		public Transform Transform;

		public ColliderType CurrentColliderType => currentColliderType;

		public RectColliderData RectData => (RectColliderData)data;
		public CircleColliderData CircleData => (CircleColliderData)data;
		public LineColliderData LineData => (LineColliderData)data;
		public PolygonColliderData PolygonData => (PolygonColliderData)data;

		public Collider()
		{
			Transform = new Transform();
			Transform.OnTransformed += Transformed;

			worldBounds = new RectangleF();

			//axis = new Vector2[4];
			axis = new List<Vector2>();
			axisCount = 0;

			//points = new Vector2[4];
			points = new List<Vector2>();
			pointCount = 0;
		}

		public Collider(RectangleF rect)
			: this()
		{
			this.currentColliderType = ColliderType.Rect;
			this.data = new RectColliderData(rect);
			this.axis.Populate(4, Vector2.Zero);
			this.points.Populate(4, Vector2.Zero);
		}

		public Collider(CircleF circle)
			: this()
		{
			this.currentColliderType = ColliderType.Circle;
			this.data = new CircleColliderData(circle);
		}

		public Collider(LineF line)
			: this()
		{
			this.currentColliderType = ColliderType.Line;
			this.data = new LineColliderData(line);
			this.axis.Populate(1, Vector2.Zero);
			this.points.Populate(2, Vector2.Zero);
		}

		public Collider(Polygon polygon)
			: this()
		{
			this.currentColliderType = ColliderType.Polygon;
			this.data = new PolygonColliderData(polygon);
			this.axis.Populate(polygon.Vertices.Length, Vector2.Zero);
			this.points.Populate(polygon.Vertices.Length, Vector2.Zero);
		}

		public void Draw(SpriteBatch sb)
		{
			if (!DebugConsole.GetVariable<bool>("showcolliders"))
				return;

			sb.DrawRectangle(
				GetWorldBounds(),
				((Overlaps(Masks.Solid, out _)) ? Color.Green : Color.Red) * 0.75f,
				1f,
				0.95f
			);

			if (data is CircleColliderData)
			{
				sb.DrawCircle(
					CircleData.WorldCircle,
					50,
					Color.Red,
					1.5f,
					0.95f
				);
			}
			else if (data is LineColliderData)
			{
				sb.DrawLine(
					LineData.WorldLine.A,
					LineData.WorldLine.B,
					Color.Red,
					1.5f,
					0.95f
				);
			}
			else if (data is PolygonColliderData)
			{
				sb.DrawPolygon(
					Transform.Position + Entity.Transform.Position,
					PolygonData.Polygon,
					Color.Red,
					1.5f,
					0.95f
				);
			}
		}

		public void MakeRect(RectangleF rect)
		{
			this.currentColliderType = ColliderType.Rect;
			this.data = new RectColliderData(rect);
			this.axis.Populate(4, Vector2.Zero);
			this.points.Populate(4, Vector2.Zero);
			UpdateWorldBounds();
		}

		public void MakeCircle(CircleF circle)
		{
			this.currentColliderType = ColliderType.Circle;
			this.data = new CircleColliderData(circle);
			UpdateWorldBounds();
		}

		public void MakeLine(LineF line)
		{
			this.currentColliderType = ColliderType.Line;
			this.data = new LineColliderData(line);
			this.axis.Populate(1, Vector2.Zero);
			this.points.Populate(2, Vector2.Zero);
			UpdateWorldBounds();
		}

		public void MakePolygon(Polygon polygon)
		{
			this.currentColliderType = ColliderType.Polygon;
			this.data = new PolygonColliderData(polygon);
			this.axis.Populate(polygon.Vertices.Length, Vector2.Zero);
			this.points.Populate(polygon.Vertices.Length, Vector2.Zero);
			UpdateWorldBounds();
		}

		public override void Init()
		{
			UpdateWorldBounds();
		}

		public RectangleF GetWorldBounds()
		{
			UpdateWorldBounds();
			return worldBounds;
		}

		public bool Overlaps(Collider collider, out Vector2 pushout)
		{
			Vector2 push = new Vector2();
			bool result = false;

			if (currentColliderType == ColliderType.Circle)
			{
				// Circle -> Circle
				if (collider.currentColliderType == ColliderType.Circle)
				{
					result = CircleToCircle(this, collider, ref push);
				}
		
				// Circle -> Convex
				else if (currentColliderType == ColliderType.Circle)
				{
					result = CircleToConvex(this, collider, ref push);
				}
			}

			// Convex -> Circle
			else if (collider.currentColliderType == ColliderType.Circle)
			{
				result = ConvexToCircle(this, collider, ref push);
			}

			// Convex -> Convex
			else
			{
				result = ConvexToConvex(this, collider, ref push);
			}

			pushout = push;

			return result;
		}

		public bool Overlaps(Masks mask, out Vector2 pushout)
		{
			bool hit = false;
			Vector2 push = new Vector2();

			World.ForeachComponentStoppable<Collider>(mask, (other) =>
			{
				if (Overlaps(other, out push))
				{
					hit = true;
					return false;
				}

				return true;
			});

			pushout = push;

			return hit;
		}

		public bool Overlaps(Masks mask, Vector2 offset, out Vector2 pushout)
		{
			Transform.Position += offset;
			bool result = Overlaps(mask, out pushout);
			Transform.Position -= offset;
			return result;
		}

		public int OverlapsAll(Masks mask, ref Hit[] populate, int capacity)
		{
			if (capacity <= 0)
				return 0;

			int index = 0;

			Hit[] populateTemp = new Hit[populate.Count()];
			for (int ii = 0; ii < populate.Count(); ii++)
				populateTemp[ii] = new Hit();

			World.ForeachComponentStoppable<Collider>(mask, (other) =>
			{
				Vector2 push = new Vector2();
				if (Overlaps(mask, out push))
				{
					populateTemp[index].Collider = other;
					populateTemp[index].Pushout = push;

					index++;

					if (index >= capacity)
						return false;
				}
				return true;
			});

			populate = populateTemp;

			return index;
		}

		public int OverlapsAll(Masks mask, Vector2 offset, ref Hit[] populate, int capacity)
		{
			Transform.Position += offset;
			int result = OverlapsAll(mask, ref populate, capacity);
			Transform.Position -= offset;
			return result;
		}

		private void Transformed()
		{
			entityMovementID = 0;
		}

		protected bool AxisOverlaps(Collider a, Collider b, Vector2 axis, ref float amount)
		{
			float min0 = 0f, max0 = 0f;
			float min1 = 0f, max1 = 0f;

			a.Project(axis, ref min0, ref max0);
			b.Project(axis, ref min1, ref max1);

			if (MathF.Abs(min1 - max0) < MathF.Abs(max1 - min0))
				amount = min1 - max0;
			else
				amount = max1 - min0;

			return (
				min0 < max1 &&
				max0 > min1
			);
		}

		protected void Project(Vector2 axis, ref float min, ref float max)
		{
			data.Project(axis, ref min, ref max);
		}

		protected void UpdateWorldBounds()
		{
			if (entityMovementID == 0 || Entity.TransformStamp != entityMovementID)
			{
				Matrix mat = Transform.GetMatrix() * Entity.Transform.GetMatrix();
				data.UpdateWorldBounds(mat, ref worldBounds, ref axis, ref points, ref axisCount, ref pointCount);
				entityMovementID = Entity.TransformStamp;
			}
		}

		protected bool CircleToCircle(Collider a, Collider b, ref Vector2 pushout)
		{
			a.UpdateWorldBounds();
			b.UpdateWorldBounds();

			if (!a.worldBounds.Intersects(b.worldBounds))
				return false;

			CircleF circle_a = ((CircleColliderData)a.data).WorldCircle;
			CircleF circle_b = ((CircleColliderData)b.data).WorldCircle;

			Vector2 center_a = new Vector2(circle_a.Center.X, circle_a.Center.Y);
			Vector2 center_b = new Vector2(circle_b.Center.X, circle_b.Center.Y);

			float length = (center_a - center_b).Length();
			float dist = (circle_a.Radius * circle_b.Radius);

			if (length <= dist)
			{
				pushout = ((center_a - center_b) / length) * (circle_a.Radius + circle_b.Radius - length);
				return true;
			}

			return false;
		}

		protected bool CircleToConvex(Collider a, Collider b, ref Vector2 pushout)
		{
			bool result = ConvexToCircle(b, a, ref pushout);
			pushout = -pushout;
			return result;
		}

		protected bool ConvexToCircle(Collider a, Collider b, ref Vector2 pushout)
		{
			a.UpdateWorldBounds();
			b.UpdateWorldBounds();

			if (!a.worldBounds.Intersects(b.worldBounds))
				return false;

			CircleF circle_b = ((CircleColliderData)b.data).WorldCircle;

			float distance = 0.0f;

			for (int ii = 0; ii < a.axisCount; ii++)
			{
				var axis = a.axis[ii];
				float amount = 0.0f;

				if (!AxisOverlaps(a, b, axis, ref amount))
					return false;

				if (ii == 0 || MathF.Abs(amount) < distance)
				{
					pushout = axis * amount;
					distance = MathF.Abs(amount);
				}
			}

			for (int ii = 0; ii < a.pointCount; ii++)
			{
				Vector2 axis = (a.points[ii] - new Vector2(circle_b.Center.X, circle_b.Center.Y)).NormalizedCopy();
				float amount = 0.0f;

				if (!AxisOverlaps(a, b, axis, ref amount))
					return false;

				if (ii == 0 || MathF.Abs(amount) < distance)
				{
					pushout = axis * amount;
					distance = MathF.Abs(amount);
				}
			}

			return true;
		}

		protected bool ConvexToConvex(Collider a, Collider b, ref Vector2 pushout)
		{
			a.UpdateWorldBounds();
			b.UpdateWorldBounds();

			if (!a.worldBounds.Intersects(b.worldBounds))
				return false;

			float distance = 0;

			for (int ii = 0; ii < a.axisCount; ii++)
			{
				var axis = a.axis[ii];
				float amount = 0.0f;

				if (!AxisOverlaps(a, b, axis, ref amount))
					return false;

				if (ii == 0 || MathF.Abs(amount) < distance)
				{
					pushout = axis * amount;
					distance = MathF.Abs(amount);
				}
			}

			for (int ii = 0; ii < b.axisCount; ii++)
			{
				var axis = b.axis[ii];
				float amount = 0.0f;

				if (!AxisOverlaps(a, b, axis, ref amount))
					return false;

				if (ii == 0 || MathF.Abs(amount) < distance)
				{
					pushout = axis * amount;
					distance = MathF.Abs(amount);
				}
			}

			return true;
		}

		public class RectColliderData : IColliderData
		{
			public RectangleF Rect;
			public QuadF WorldRect;

			public RectColliderData(RectangleF rect)
			{
				this.Rect = rect;
				this.WorldRect = new QuadF();
			}

			public void Project(Vector2 axis, ref float min, ref float max)
			{
				WorldRect.Project(axis, ref min, ref max);
			}

			public void UpdateWorldBounds(Matrix mat, ref RectangleF worldBounds, ref List<Vector2> axis, ref List<Vector2> points, ref int axisCount, ref int pointCount)
			{
				WorldRect.A = Vector2.Transform(Rect.TopLeft, mat);
				WorldRect.B = Vector2.Transform(Rect.TopRight, mat);
				WorldRect.C = Vector2.Transform(Rect.BottomRight, mat);
				WorldRect.D = Vector2.Transform(Rect.BottomLeft, mat);

				axis[0] = (WorldRect.B - WorldRect.A).NormalizedCopy();
				axis[0] = new Vector2(-axis[0].Y, axis[0].X);
				axis[1] = (WorldRect.C - WorldRect.B).NormalizedCopy();
				axis[1] = new Vector2(-axis[1].Y, axis[1].X);
				axis[2] = (WorldRect.D - WorldRect.C).NormalizedCopy();
				axis[2] = new Vector2(-axis[2].Y, axis[2].X);
				axis[3] = (WorldRect.A - WorldRect.D).NormalizedCopy();
				axis[3] = new Vector2(-axis[3].Y, axis[3].X);
				axisCount = 4;

				points[0] = WorldRect.A;
				points[1] = WorldRect.B;
				points[2] = WorldRect.C;
				points[3] = WorldRect.D;
				pointCount = 4;

				worldBounds.X = MathF.Min(WorldRect.A.X, MathF.Min(WorldRect.B.X, MathF.Min(WorldRect.C.X, WorldRect.D.X)));
				worldBounds.Y = MathF.Min(WorldRect.A.Y, MathF.Min(WorldRect.B.Y, MathF.Min(WorldRect.C.Y, WorldRect.D.Y)));
				worldBounds.Width = MathF.Max(WorldRect.A.X, MathF.Max(WorldRect.B.X, MathF.Max(WorldRect.C.X, WorldRect.D.X))) - worldBounds.X;
				worldBounds.Height = MathF.Max(WorldRect.A.Y, MathF.Max(WorldRect.B.Y, MathF.Max(WorldRect.C.Y, WorldRect.D.Y))) - worldBounds.Y;
			}
		}

		public class CircleColliderData : IColliderData
		{
			public CircleF Circle;
			public CircleF WorldCircle;

			public CircleColliderData(CircleF circle)
			{
				this.Circle = circle;
			}

			public void Project(Vector2 axis, ref float min, ref float max)
			{
				WorldCircle.Project(axis, ref min, ref max);
			}

			public void UpdateWorldBounds(Matrix mat, ref RectangleF worldBounds, ref List<Vector2> axis, ref List<Vector2> points, ref int axisCount, ref int pointCount)
			{
				WorldCircle.Center = Vector2.Transform(Circle.Center, mat);
				WorldCircle.Radius = Circle.Radius;

				worldBounds.X = WorldCircle.Center.X - WorldCircle.Radius;
				worldBounds.Y = WorldCircle.Center.Y - WorldCircle.Radius;
				worldBounds.Width = WorldCircle.Diameter;
				worldBounds.Height = WorldCircle.Diameter;

				axisCount = 0;
				pointCount = 0;
			}
		}

		public class LineColliderData : IColliderData
		{
			public LineF Line;
			public LineF WorldLine;

			public LineColliderData(LineF line)
			{
				this.Line = line;
			}

			public void Project(Vector2 axis, ref float min, ref float max)
			{
				WorldLine.Project(axis, ref min, ref max);
			}

			public void UpdateWorldBounds(Matrix mat, ref RectangleF worldBounds, ref List<Vector2> axis, ref List<Vector2> points, ref int axisCount, ref int pointCount)
			{
				WorldLine.A = Vector2.Transform(Line.A, mat);
				WorldLine.B = Vector2.Transform(Line.B, mat);

				axis[0] = (WorldLine.B - WorldLine.A).NormalizedCopy();
				axis[0] = new Vector2(-axis[0].Y, axis[0].X);
				axisCount = 1;

				points[0] = WorldLine.A;
				points[1] = WorldLine.B;
				pointCount = 2;

				worldBounds.X = MathF.Min(WorldLine.A.X, WorldLine.B.X);
				worldBounds.Y = MathF.Min(WorldLine.A.Y, WorldLine.B.Y);
				worldBounds.Width = MathF.Max(WorldLine.A.X, WorldLine.B.X) - worldBounds.X;
				worldBounds.Height = MathF.Max(WorldLine.A.Y, WorldLine.B.Y) - worldBounds.Y;
			}
		}

		public class PolygonColliderData : IColliderData
		{
			public Polygon Polygon;
			public Polygon WorldPolygon;

			public PolygonColliderData(Polygon polygon)
			{
				this.Polygon = polygon;
				this.WorldPolygon = new Polygon(polygon.Vertices);
			}

			public void Project(Vector2 axis, ref float min, ref float max)
			{
				min = axis.Dot(Polygon.Vertices[0]);
				max = min;

				for (int ii = 0; ii < Polygon.Vertices.Length; ii++)
				{
					float p = axis.Dot(Polygon.Vertices[ii]);
					if (p < min)
						min = p;
					else if (p > max)
						max = p;
				}
			}

			public void UpdateWorldBounds(Matrix mat, ref RectangleF worldBounds, ref List<Vector2> axis, ref List<Vector2> points, ref int axisCount, ref int pointCount)
			{
				int vertexCount = Polygon.Vertices.Length;

				// Update Axis and Points
				{
					axisCount = vertexCount;
					pointCount = vertexCount;

					for (int ii = 0; ii < vertexCount; ii++)
					{
						WorldPolygon.Vertices[ii] = Vector2.Transform(Polygon.Vertices[ii], mat);

						int next = ii + 1;

						axis[ii] = Polygon.Vertices[ii]-Polygon.Vertices[(next>=vertexCount) ? 0 : next];
						axis[ii] = new Vector2(-axis[0].Y, axis[0].X);
						points[ii] = Polygon.Vertices[ii];
					}
				}

				// Update World Bounds
				{
					float minx = Single.MaxValue;
					float maxx = -Single.MaxValue;
					float miny = Single.MaxValue;
					float maxy = -Single.MaxValue;

					for (int ii = 0; ii < vertexCount; ii++)
					{
						float xx = WorldPolygon.Vertices[ii].X;
						float yy = WorldPolygon.Vertices[ii].Y;
						minx = MathF.Min(minx, xx);
						maxx = MathF.Max(maxx, xx);
						miny = MathF.Min(miny, yy);
						maxy = MathF.Max(maxy, yy);
					}

					worldBounds.X = minx;
					worldBounds.Y = miny;
					worldBounds.Width = maxx - minx;
					worldBounds.Height = maxy - miny;
				}
			}
		}
	}
}
