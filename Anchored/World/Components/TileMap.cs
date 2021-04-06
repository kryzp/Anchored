using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Arch.World.Components;
using Arch.Assets.Maps;
using Arch.World;
using Anchored.Assets.Maps;
using Arch;
using System.Reflection;
using System.Linq;
using Arch.Graphics;

namespace Anchored.World.Components
{
	public class TileMap : GraphicsComponent
	{
		private AnchoredMap map;
		private List<Collider> colliders;
		
		private Camera camera;

		public AnchoredMap Map => map;

		public TileMap()
		{
			this.colliders = new List<Collider>();
			this.LayerDepth = 0.5f;
		}

		public TileMap(AnchoredMap map, Camera camera)
			: this()
		{
			this.map = map;
			this.camera = camera;
		}

		public override void Update()
		{
			base.Update();

			Map.Update();
		}

		public override void DrawBegin(SpriteBatch sb)
		{
		}

		public override void Draw(SpriteBatch sb)
		{
			Map.Draw(sb);
		}

		public override void DrawEnd(SpriteBatch sb)
		{
		}

		public void LoadEntities()
		{
			EntityWorld world = Entity.World;

			foreach (var e in Map.Entities)
			{
				var name = e.Name;
				var type = e.Type;
				var level = e.Level;
				var position = e.Position;
				var z = e.Z;
				var settings = e.Settings;

				foreach (var field in type.GetType().GetFields())
				{
					object[] attribs = field.GetCustomAttributes(true);

					foreach (var attrib in attribs)
					{
						if (attrib.GetType() == typeof(Arch.World.EntityTypeSetting))
						{
							var entityAttrib = (EntityTypeSetting)attrib;

							byte[] data = (byte[])settings[entityAttrib.Name];

							var variable = Utility.FromByteArray(field.FieldType, data);
							field.SetValue(type, variable);
						}
					}
				}

				Entity entity = world.AddEntity(name);

				entity.Transform.Position = position;
				entity.Transform.Z = z;
				entity.Level = level;

				type.Create(entity);
			}
		}

		public void LoadColliders()
		{
			/*
			if (collisionLayer == null)
				return;

			ClearColliders();

			var colliderList = collisionLayer.Objects;

			foreach (TiledMapRectangleObject rect in colliderList.Where(x => x is TiledMapRectangleObject))
			{
				Collider collider = Entity.AddComponent(new Collider());
				collider.MakeRect(new RectangleF(0, 0, rect.Size.Width, rect.Size.Height));
				collider.Transform.Position = rect.Position;
				collider.Mask = Masks.Solid;
				colliders.Add(collider);
			}

			foreach (TiledMapPolylineObject line in colliderList.Where(x => x is TiledMapPolylineObject))
			{
				Collider collider = Entity.AddComponent(new Collider());
				collider.MakeLine(new LineF(line.Points[0], line.Points[1]));
				collider.Transform.Position = line.Position;
				collider.Mask = Masks.Solid;
				colliders.Add(collider);
			}

			// note: currently placing down circles only supports circles that have equal radius, if the one placed down doesn't have equal radius then it'll just use the X radius!
			foreach (TiledMapEllipseObject circle in colliderList.Where(x => x is TiledMapEllipseObject))
			{
				Collider collider = Entity.AddComponent(new Collider());
				collider.MakeCircle(new CircleF(circle.Radius, circle.Radius.X));
				collider.Transform.Position = circle.Position;
				collider.Mask = Masks.Solid;
				colliders.Add(collider);
			}
			
			foreach (TiledMapPolygonObject polygon in colliderList.Where(x => x is TiledMapPolygonObject))
			{
				var vectorPoints = polygon.Points.Select(x => new Vector2(x.X, x.Y)).ToList();
				Collider collider = Entity.AddComponent(new Collider());
				collider.MakePolygon(new Polygon(vectorPoints));
				collider.Transform.Position = polygon.Position;
				collider.Mask = Masks.Solid;
				colliders.Add(collider);
			}
			*/
		}

		public void ClearColliders()
		{
			for (int ii = 0; ii < colliders.Count; ii += 1)
			{
				Entity entity = colliders[ii].Entity;
				entity.RemoveComponent(colliders[ii]);
			}

			colliders.Clear();
		}
	}
}
