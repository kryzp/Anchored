using Arch.Graphics;
using Microsoft.Xna.Framework.Graphics;

namespace Arch.Assets
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
            this.Effect = EffectManager.Get(fx);
        }

        public abstract void Update();
        
        public virtual void Begin(SpriteBatch sb, Camera camera = null)
        {
            sb.End();
            sb.Begin(
                SpriteSortMode.Immediate,
                samplerState: SamplerState.PointClamp,
                transformMatrix: camera?.GetPerfectViewMatrix() ?? Camera.Main.GetPerfectViewMatrix()
            );
            Effect.CurrentTechnique.Passes[0].Apply();
        }

        public virtual void End(SpriteBatch sb, Camera camera = null)
        {
            sb.End();
            sb.Begin(
                SpriteSortMode.FrontToBack,
                samplerState: SamplerState.PointClamp,
                transformMatrix: camera?.GetPerfectViewMatrix() ?? Camera.Main.GetPerfectViewMatrix()
            );
        }
    }
}