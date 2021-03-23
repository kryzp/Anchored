using Anchored.Assets;
using Anchored.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Anchored.World.Components
{
    public abstract class GraphicsComponent : Component, IRenderable, IUpdatable
    {
        public float LayerDepth = 0f;
        public Vector2 Origin = Vector2.Zero;
        public Color Colour = Color.White;
        public Shader Shader = null;
        public TextureRegion Texture = null;
        
        public int Width => Texture.Texture.Width;
        public int Height => Texture.Texture.Height;
        
        public int Order { get; set; }

        public virtual void Update()
        {
            Shader?.Update();
        }
        
        public abstract void DrawBegin(SpriteBatch sb);
        public abstract void Draw(SpriteBatch sb);
        public abstract void DrawEnd(SpriteBatch sb);
    }
}