using Anchored.Assets;
using Anchored.World.Components;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Tiled;
using Anchored.Streams;
using Anchored.Util;
using Anchored.Assets.Textures;

namespace Anchored.World.Types
{
	public class LevelType : EntityType
	{
		private TiledMap map;
		private bool loadColliders;
		private bool loadEntities;

		public LevelType(TiledMap map, bool loadColliders = true, bool loadEntities = true)
		{
			Serializable = true;
			
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

		public override void Save(FileWriter stream)
		{
			stream.WriteString(map.Name);
			stream.WriteBoolean(loadColliders);
			stream.WriteBoolean(loadEntities);
		}

		public override void Load(FileReader stream)
		{
			string mapName = stream.ReadString();
			loadColliders = stream.ReadBoolean();
			loadEntities = stream.ReadBoolean();
			map = TileMapManager.Get(mapName);
		}
		
		protected void LoadEntitiesFromTileMap(EntityWorld world, TiledMap map)
		{
			foreach (var obj in map.GetLayer<TiledMapObjectLayer>("Entities").Objects)
			{
				TiledMapRectangleObject entityObj = (TiledMapRectangleObject)obj;
				string entityName = entityObj.Name;
				Vector2 entityPosition = entityObj.Position;
				float entityRotation = entityObj.Rotation;

				var entity = world.AddEntity(entityName);

				if (entityObj.Type == "Tree")
				{
					string sheet = entityObj.Properties["Sheet"];
					string textureName = entityObj.Properties["Texture"];
					TextureRegion texture = TextureBoundManager.Get($"tilesheets\\{sheet}", textureName);
					new TreeType(texture).Create(entity);
				}

				entity.Transform.Position = entityPosition;
				entity.Transform.RotationDegrees = entityRotation;
			}
		}
	}
}
