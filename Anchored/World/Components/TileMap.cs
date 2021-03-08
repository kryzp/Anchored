using Anchored.Math;
using Anchored.Physics;
using Anchored.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;

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

			var colliderList = collisionLayer.Objects;

			foreach (TiledMapPolygonObject collider in colliderList.Where(x => x is TiledMapPolygonObject))
			{
				Point2[] points = collider.Points;

				for (int ii = 0; ii < points.Length; ii++)
				{
					Point2 point = points[ii];
					Point2 nextPoint = points[0];

					if (ii < points.Length-1)
						nextPoint = points[ii+1];

					Collider lineCollider = Entity.AddComponent(new Collider());

					lineCollider.MakeLine(
						new LineF(
							new Vector2(
								point.X,
								point.Y
							),
							new Vector2(
								nextPoint.X,
								nextPoint.Y
							)
						)
					);

					lineCollider.Transform.Position = collider.Position;
					lineCollider.Mask = Masks.Solid;

					colliders.Add(lineCollider);
				}
			}
		}
	}
}
