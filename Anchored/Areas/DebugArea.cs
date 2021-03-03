using Anchored.Assets;
using Anchored.World;
using Anchored.World.Components;
using Microsoft.Xna.Framework.Graphics;

namespace Anchored.Areas
{
	public class DebugArea : GameArea
	{
		public DebugArea()
			: base()
		{
		}

		public override void Load(SpriteBatch sb)
		{
			base.Load(sb);

			SetupTileMap(Maps.TEST, true);

			var ids = EntityTypes.GetIDs();

			var playerEntity = world.AddEntity();
			EntityTypes.NewTypeOf(ids[0]).Create(playerEntity, 0);
			Game1.Player = playerEntity;

			var cameraEntity = world.AddEntity();
			EntityTypes.NewTypeOf(ids[1]).Create(cameraEntity, 0);
			Game1.Camera = cameraEntity.GetComponent<Camera>();
		}

		public override void Unload()
		{
			base.Unload();
		}

		public override void Update()
		{
			base.Update();
		}

		public override void Draw(SpriteBatch sb)
		{
			base.Draw(sb);
		}

		public override void DrawUI(SpriteBatch sb)
		{
			base.DrawUI(sb);
		}
	}
}
