using System.Collections.Generic;
using Anchored.World.Components;
using Microsoft.Xna.Framework.Graphics;

namespace Anchored.Assets
{
    public abstract class Shader
    {
        public Effect Effect = null;
        
        public Shader(Effect fx)
        {
            this.Effect = fx;
        }

        public Shader(string fx)
        {
            this.Effect = Effects.Get(fx);
        }

        public abstract void Update();
        
        public virtual void Begin(SpriteBatch sb, Camera camera = null)
        {
            sb.End();
            sb.Begin(
                SpriteSortMode.Immediate,
                samplerState: SamplerState.PointClamp,
                transformMatrix: camera?.GetViewMatrix() ?? Camera.Main.GetViewMatrix()
            );
            Effect.CurrentTechnique.Passes[0].Apply();
        }

        public virtual void End(SpriteBatch sb, Camera camera = null)
        {
            sb.End();
            sb.Begin(
                SpriteSortMode.FrontToBack,
                samplerState: SamplerState.PointClamp,
                transformMatrix: camera?.GetViewMatrix() ?? Camera.Main.GetViewMatrix()
            );
        }
    }
}