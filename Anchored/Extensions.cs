using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Anchored
{
    public static class Extensions
    {
        public static Texture2D Crop(
            this Texture2D tex,
            Rectangle rect,
            bool set = false
        )
		{
            Texture2D croppedTexture = new Texture2D(Game1.GraphicsDevice, rect.Width, rect.Height);
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