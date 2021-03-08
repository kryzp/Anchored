using Anchored.Assets;
using Anchored.World;
using Anchored.World.Components;
using Anchored.World.Types;
using Microsoft.Xna.Framework.Graphics;

namespace Anchored.Areas
{
	public class DebugArea : GameArea
	{
		public DebugArea(EntityWorld world)
			: base(world)
		{
		}

		public override void Load(SpriteBatch sb)
		{
			base.Load(sb);

			SetupTileMap(TileMaps.Get("test_map"), true);

			var playerEntity = world.AddEntity("Player");
			new PlayerType().Create(playerEntity, 0);
			Game1.Player = playerEntity;

			var follow = cameraEntity.AddComponent(new Follow(playerEntity.Transform));
			follow.LerpAmount = 0.2f;
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
