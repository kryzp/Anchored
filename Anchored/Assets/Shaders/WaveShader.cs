using Arch;
using Arch.Assets;
using Microsoft.Xna.Framework.Graphics;

namespace Anchored.Assets.Shaders
{
    public class WaveShader : Shader
    {
        public float _Time;
        
        public WaveShader(Effect fx)
            : base(fx)
        {
        }

        public WaveShader(string fx)
            : base(fx)
        {
        }

        public override void Update()
        {
            _Time = Time.TotalSeconds;
            Effect.Parameters["_time"].SetValue(_Time);
        }
    }
}
