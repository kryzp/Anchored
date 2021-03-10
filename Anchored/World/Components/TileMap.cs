using Anchored.Math;
using Anchored.Physics;
using Anchored.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Shapes;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Anchored.World.Components
{
	public class TileMap : Component, IUpdatable, IDrawable
	{
		private TiledMap map;
		private TiledMapRenderer renderer;
		private TiledMapObjectLayer collisionLayer;
		private List<Collider> colliders;

		private Camera camera;

		public Effect Effect = null;
		public float LayerDepth = 0f;

		public TiledMap Map => map;
		public TiledMapRenderer Renderer => renderer;

		public TileMap()
		{
		}

		public TileMap(TiledMap map, Camera camera)
		{
			this.map = map;
			this.camera = camera;
			this.renderer = new TiledMapRenderer(Game1.GraphicsDevice, map);
			this.collisionLayer = map.GetLayer<TiledMapObjectLayer>("CollisionsObj");
			this.colliders = new List<Collider>();
		}

		public void Update()
		{
			renderer.Update(Time.GameTime);
		}

		public void Draw(SpriteBatch sb)
		{
			Game1.GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;

			renderer.Draw(
				camera.GetViewMatrix(),
				effect: Effect,
				depth: LayerDepth
			);
		}

		public void LoadColliders()
		{
			if (collisionLayer == null)
				return;

			for (int ii = 0; ii < colliders.Count; ii += 1)
			{
				Entity entity = colliders[ii].Entity;
				entity.RemoveComponent(colliders[ii]);
			}

			colliders.Clear();

			var colliderList = collisionLayer.Objects;

			foreach (TiledMapObject rect in colliderList.Where(x => x is TiledMapRectangleObject))
			{
				Collider collider = Entity.AddComponent(new Collider());
				collider.MakeRect(new RectangleF(0, 0, rect.Size.Width, rect.Size.Height));
				collider.Transform.Position = rect.Position;
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
	}
}
