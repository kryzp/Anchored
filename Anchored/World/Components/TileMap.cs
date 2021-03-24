using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Shapes;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System.Collections.Generic;
using System.Linq;
using Anchored.Util.Math;

namespace Anchored.World.Components
{
	public class TileMap : GraphicsComponent
	{
		private TiledMap map;
		private TiledMapRenderer renderer;
		private TiledMapObjectLayer collisionLayer;
		private List<Collider> colliders;
		private int currentDrawingLayer;
		
		private Camera camera;

		public TiledMap Map => map;
		public TiledMapRenderer Renderer => renderer;

		public TileMap()
		{
			this.colliders = new List<Collider>();
			this.LayerDepth = 0.5f;
		}

		public TileMap(TiledMap map, Camera camera)
			: this()
		{
			this.map = map;
			this.camera = camera;
			this.renderer = new TiledMapRenderer(Game1.GraphicsDevice, map);
			this.collisionLayer = map.GetLayer<TiledMapObjectLayer>("Collisions");
		}

		public override void Update()
		{
			base.Update();
			renderer.Update(Time.GameTime);
		}

		public override void DrawBegin(SpriteBatch sb)
		{
			Game1.GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;

			for (int ii = 0; ii < map.Layers.Count; ii++)
			{
				var layer = map.Layers[ii];
				
				if (layer.Name == "Entities" || layer.Name == "Depth")
				{
					currentDrawingLayer = ii;
					break;
				}
				
				renderer.Draw(
					layer,
					viewMatrix: GetTileMapMatrix(),
					effect: Shader?.Effect,
					depth: 0f
				);
			}
		}

		public override void Draw(SpriteBatch sb)
		{
			Game1.GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;

			for (int ii = currentDrawingLayer; ii < map.Layers.Count; ii++)
			{
				var layer = map.Layers[ii];

				if (layer.Name == "Front" || layer.Name == "FrontObj")
				{
					currentDrawingLayer = ii;
					break;
				}

				renderer.Draw(
					layer,
					viewMatrix: GetTileMapMatrix(),
					effect: Shader?.Effect,
					depth: 0.1f
				);
			}
		}

		public override void DrawEnd(SpriteBatch sb)
		{
			Game1.GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;

			for (int ii = currentDrawingLayer; ii < map.Layers.Count; ii++)
			{
				var layer = map.Layers[ii];

				renderer.Draw(
					layer,
					viewMatrix: GetTileMapMatrix(),
					effect: Shader?.Effect,
					depth: 1f
				);
			}
		}

		public void LoadColliders()
		{
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
