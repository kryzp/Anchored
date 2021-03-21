using Anchored.Assets;
using Anchored.Util;
using Anchored.World.Components;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.World.Types
{
	public class TileMapType : EntityType
	{
		private TiledMap map;
		private bool loadColliders;
		private bool loadEntities;

		public TileMapType(TiledMap map, bool loadColliders = true, bool loadEntities = true)
		{
			this.map = map;
			this.loadColliders = loadColliders;
			this.loadEntities = loadEntities;
		}

		public override void Create(Entity entity)
		{
			base.Create(entity);

			var tileMap = entity.AddComponent(new TileMap(map, Camera.Main));

			if (loadColliders)
				tileMap.LoadColliders();

			if (loadEntities)
				LoadEntitiesFromTileMap(entity.World, map);
		}

		protected void LoadEntitiesFromTileMap(EntityWorld world, TiledMap map)
		{
			foreach (var obj in map.GetLayer<TiledMapObjectLayer>("Entities").Objects)
			{
				// todo: move into external class that inherits from like "TiledMapEntity" or something like that which has a Create method n' stuff!

				TiledMapRectangleObject entityObj = (TiledMapRectangleObject)obj;
				string entityName = entityObj.Name;
				Vector2 entityPosition = entityObj.Position;
				float entityRotation = entityObj.Rotation;

				// todo: only an example this should be replaced with like a 'TreeType' entity type!!!!

				/*
				if (entityObj.Type == "Tree")
				{
					string sheet = entityObj.Properties["Sheet"];
					string textureName = entityObj.Properties["Texture"];
					TextureRegion texture = TileSheetBounds.Get(sheet, textureName);

					Entity entity = world.AddEntity(entityName);
					entity.Transform.Position = entityPosition;
					entity.Transform.RotationDegrees = entityRotation;

					var sprite = entity.AddComponent(new Sprite(texture));

					sprite.Origin.X = texture.Width / 2;
					sprite.Origin.Y = texture.Height;

					var collider = entity.AddComponent(new Collider());
					collider.Mask = Masks.Solid;
					collider.MakeRect(new RectangleF(-8, -16, 16, 16));
				}
				*/
			}
		}
	}
}
