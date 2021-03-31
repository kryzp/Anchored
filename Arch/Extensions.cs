using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arch
{
    public static class Extensions
    {
        public static Texture2D Crop(
            this Texture2D tex,
            Rectangle rect,
            bool set = false
        )
		{
            Texture2D croppedTexture = new Texture2D(Engine.GraphicsDevice, rect.Width, rect.Height);
            Color[] data = new Color[rect.Width * rect.Height];
            tex.GetData(0, rect, data, 0, rect.Width * rect.Height);
            croppedTexture.SetData(data);
            if (set)
                tex = croppedTexture;
            return croppedTexture;
		}

        public static Vector2 Normalized(this Vector2 v)
		{
            Vector2 vv = v;
            vv.Normalize();
            return vv;
		}
    }
}