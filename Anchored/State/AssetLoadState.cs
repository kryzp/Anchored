using System;
using System.Diagnostics;
using System.Threading;
using Anchored.Assets;
using Anchored.Assets.Prefabs;
using Anchored.Debug.Console;
using Anchored.Save;
using Anchored.Util;
using Anchored.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace Anchored.State
{
    public class AssetLoadState : GameState
    {
        private const bool SECTIONAL_LOAD_TIME_LOGGING = true;
        private bool ready;
        private int progress;
        private string currentlyLoadingLabel;
        private EntityWorld world;
        
        public override void Load(SpriteBatch sb)
        {
            var thread = new Thread(() =>
            {
                var sw = Stopwatch.StartNew();
                
                DebugConsole.Engine("Loading Assets...");
                
                LoadSection(() => SaveManager.Load(world, SaveType.Global), "Global Saves");
                LoadSection(() => AssetManager.Load(ref progress), "Assets");

                progress += 1;
                
                LoadSection(ShaderHolder.Load, "Shaders");
                LoadSection(TileSheetBounds.Load, "Tile Sheets");
                LoadSection(PrefabHolder.Load, "Prefabs");

                progress += 1;
                
                world = new EntityWorld();
                
                LoadSection(() => SaveManager.Load(world, SaveType.Game), "Game Saves");
                LoadSection(() => SaveManager.Load(world, SaveType.Level), "Level Saves");
                LoadSection(() => SaveManager.Load(world, SaveType.Player), "Player Saves");

                progress += 1;
                
                DebugConsole.Engine($"Finished Loading in {sw.ElapsedMilliseconds} ms!");
                ready = true;
            });

            thread.Start();
        }

        public override void Unload()
        {
        }

        public override void Update()
        {
            if (ready)
            {
                Game1.ChangeState(new PlayingState());
            }
        }

        public override void Draw(SpriteBatch sb)
        {
        }

        public override void DrawUI(SpriteBatch sb)
        {
            // TODO: draw loading icon and such
            
            sb.Begin(SpriteSortMode.Immediate, samplerState: SamplerState.PointClamp);
            var color = Utility.BlendColours(Color.Aqua, new Color(50, 150, 200), MathF.Sin(Time.TotalSeconds * 10f));
            Utility.DrawRectangle(new RectangleF(10, 10, 64, 64), color, 1f, sb);
            sb.End();
        }

        public override void DrawDebug()
        {
        }
        
        private void LoadSection(Action section, string name)
        {
            currentlyLoadingLabel = name;

            var sw = Stopwatch.StartNew();
			
            section();

            progress += 1;

            if (SECTIONAL_LOAD_TIME_LOGGING)
                DebugConsole.Engine($"Loaded section '{name}' in {sw.ElapsedMilliseconds} ms.");
        }
    }
}