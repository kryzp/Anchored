using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Anchored.Assets;
using Anchored.Assets.Prefabs;
using Anchored.Save;
using Arch.UI;
using Arch.Util;
using Anchored.World;
using Anchored.World.Components;
using Arch.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Arch.Assets.Textures;
using Arch;
using Arch.World;
using Arch.Assets;
using Arch.Graphics.Animating;
using Anchored.UI.Constraints;
using Anchored.UI.Elements;
using Anchored.Assets.Maps;
using Anchored.Assets.Textures;

namespace Anchored.State
{
    public class AssetLoadState : GameState
    {
        private const bool SECTIONAL_LOAD_TIME_LOGGING = true;
        private bool ready;
        private int progress;
        private string currentlyLoadingLabel;
        private EntityWorld world;
        
        public AssetLoadState()
		{
        }

        public override void Load(SpriteBatch sb)
        {
            //SetupUI();

            var thread = new Thread(() =>
            {
                var sw = Stopwatch.StartNew();
                
                Log.Engine("Loading Assets...");
                
                LoadSection(() => SaveManager.Load(world, SaveType.Global), "Global Saves");
                LoadSection(() => AssetManager.Load(ref progress), "Assets");

                progress += 1;
                
                LoadSection(ShaderManager.Load, "Shaders");
                LoadSection(TextureBoundManager.Load, "Tile Sheets");
                LoadSection(PrefabManager.Load, "Prefabs");
                LoadSection(MapManager.Load, "Maps");

                progress += 1;
                
                world = new EntityWorld();
                
                LoadSection(() => SaveManager.Load(world, SaveType.Game), "Game Saves");
                LoadSection(() => SaveManager.Load(world, SaveType.Level), "Level Saves");
                LoadSection(() => SaveManager.Load(world, SaveType.Player), "Player Saves");

                progress += 1;

                Log.Engine($"Finished Loading in {sw.ElapsedMilliseconds} ms!");
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
                Game1.ChangeState(new EditorState());
			}
        }

        public override void Draw(SpriteBatch sb)
        {
        }

        public override void DrawUI(SpriteBatch sb)
        {
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
                Log.Engine($"Loaded section '{name}' in {sw.ElapsedMilliseconds} ms.");
        }

        private void SetupUI()
		{
            float delay = 0.1f;
            var loadTex = new TextureRegion(AssetManager.Content.Load<Texture2D>("txrs\\ui\\loading_icon"));

            UIComponent display = UIManager.Container;

            AnimationData loadAnimData = new AnimationData();
            {
                loadAnimData.Layers.Add("Main", new List<AnimationFrame>()
                {
                    new AnimationFrame()
                    {
                        Duration = delay,
                        Bounds = new Rectangle(0, 0, 9, 9),
                        Texture = loadTex
                    },
                    new AnimationFrame()
                    {
                        Duration = delay,
                        Bounds = new Rectangle(9, 0, 9, 9),
                        Texture = loadTex
                    },
                    new AnimationFrame()
                    {
                        Duration = delay,
                        Bounds = new Rectangle(18, 0, 9, 9),
                        Texture = loadTex
                    },
                    new AnimationFrame()
                    {
                        Duration = delay,
                        Bounds = new Rectangle(27, 0, 9, 9),
                        Texture = loadTex
                    },
                    new AnimationFrame()
                    {
                        Duration = delay,
                        Bounds = new Rectangle(36, 0, 9, 9),
                        Texture = loadTex
                    },
                    new AnimationFrame()
                    {
                        Duration = delay,
                        Bounds = new Rectangle(45, 0, 9, 9),
                        Texture = loadTex
                    },
                    new AnimationFrame()
                    {
                        Duration = delay,
                        Bounds = new Rectangle(54, 0, 9, 9),
                        Texture = loadTex
                    }
                });

                loadAnimData.Tags.Add("Main", new AnimationTag()
                {
                    StartFrame = 0,
                    EndFrame = 6,
                    Direction = AnimationDirection.Forward
                });
            }

            UIAnimatedTexture uiLoader = new UIAnimatedTexture(loadAnimData.CreateAnimation());

            UIConstraints constraints = new UIConstraints();

            constraints.X = new PixelConstraint(Game1.WindowWidth-32-8);
            constraints.Y = new PixelConstraint(Game1.WindowHeight-32-8);
            constraints.Width = new PixelConstraint(36);
            constraints.Height = new PixelConstraint(36);

            display.Add(uiLoader, constraints);
        }
    }
}
