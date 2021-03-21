using Microsoft.Xna.Framework.Graphics;

namespace Anchored.Assets.Shaders
{
    public class RainbowShader : Shader
    {
        public float _Time;
        
        public RainbowShader(Effect fx)
            : base(fx)
        {
        }

        public RainbowShader(string fx)
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