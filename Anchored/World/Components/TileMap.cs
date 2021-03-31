﻿using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Arch.World.Components;
using Arch.Assets.Maps;
using Arch.World;

namespace Anchored.World.Components
{
	public class TileMapRenderer : GraphicsComponent
	{
		private Map map;
		private List<Collider> colliders;
		
		private Camera camera;

		public Map Map => map;

		public TileMapRenderer()
		{
			this.colliders = new List<Collider>();
			this.LayerDepth = 0.5f;
		}

		public TileMapRenderer(Map map, Camera camera)
			: this()
		{
			this.map = map;
			this.camera = camera;
		}

		public override void DrawBegin(SpriteBatch sb)
		{
		}

		public override void Draw(SpriteBatch sb)
		{
		}

		public override void DrawEnd(SpriteBatch sb)
		{
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
		
		private Matrix GetTileMapMatrix()
		{
			return camera.GetPerfectViewMatrix();
		}
	}
}
