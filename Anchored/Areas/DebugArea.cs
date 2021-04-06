using System;
using System.Collections.Generic;
using Anchored.Assets;
using Anchored.Assets.Maps;
using Anchored.Assets.Textures;
using Anchored.World;
using Anchored.World.Components;
using Anchored.World.Types;
using Arch.Assets.Maps;
using Arch.Graphics;
using Arch.Graphics.CameraDrivers;
using Arch.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Anchored.Areas
{
    public class DebugArea : GameArea
    {
        public DebugArea(EntityWorld w)
            : base(w)
        {
            SetupLevel(MapManager.Get("test"), true, true);

            Camera.Main.Driver = new FollowDriver();
            Camera.Main.Follow = world.GetComponent<Player>().Entity;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
        }
    }
}
