using Anchored.World.Components.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System;
using System.Collections.Generic;

namespace Anchored.World.Components
{
	public class TileMap : Component, IUpdatable, IDrawable
	{
		private TiledMap map;
		private TiledMapRenderer renderer;
		private TiledMapTileLayer collisionLayer;
		private List<Collider> colliders;

		public Effect Effect = null;
		public float LayerDepth = 0f;

		public TiledMap Map => map;
		public TiledMapRenderer Renderer => renderer;

		public TileMap()
		{
		}

		public TileMap(TiledMap map)
		{
			this.map = map;
			renderer = new TiledMapRenderer(Game1.GraphicsDevice, map);
			collisionLayer = map.GetLayer<TiledMapTileLayer>("Collisions");
			colliders = new List<Collider>();
		}

		public void Update()
		{
			renderer.Update(Time.GameTime);
		}

		public void Draw(SpriteBatch sb)
		{
			renderer.Draw(Game1.Camera.GetViewMatrix(), effect: Effect, depth: LayerDepth);
		}

		public void LoadColliders()
		{
			if (collisionLayer == null)
				return;

			int tileCountX = collisionLayer.Width;
			int tileCountY = collisionLayer.Height;

			for (UInt16 yy = 0; yy < tileCountY; yy++)
			{
				for (UInt16 xx = 0; xx < tileCountX; xx++)
				{
					if (collisionLayer.TryGetTile(xx, yy, out TiledMapTile? tt))
					{
						if (!tt.Value.IsBlank)
						{
							var collider = Entity.AddComponent(new Collider());
							collider.MakeRect(new RectangleF(0, 0, 16, 16));
							collider.Transform.Position = new Vector2(xx*16, yy*16);
							collider.Mask = Masks.Solid;
							colliders.Add(collider);
						}
					}
				}
			}
		}
	}
}
