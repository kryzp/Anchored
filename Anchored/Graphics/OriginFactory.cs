using Anchored.Util;
using Microsoft.Xna.Framework;

namespace Anchored.Graphics
{
    public static class OriginFactory
    {
        public static Vector2 GetOrigin(OriginPosition position, TextureRegion texture)
        {
            Vector2 result = Vector2.Zero;
            
            switch (position)
            {
                case OriginPosition.TopLeft:
                    result.X = 0;
                    result.Y = 0;
                    break;

                case OriginPosition.TopCenter:
                    result.X = texture.Width / 2;
                    result.Y = 0;
                    break;

                case OriginPosition.TopRight:
                    result.X = texture.Width;
                    result.Y = 0;
                    break;

                case OriginPosition.CenterLeft:
                    result.X = 0;
                    result.Y = texture.Height / 2;
                    break;

                case OriginPosition.CenterCenter:
                    result.X = texture.Width / 2;
                    result.Y = texture.Height / 2;
                    break;

                case OriginPosition.CenterRight:
                    result.X = texture.Width;
                    result.Y = texture.Height / 2;
                    break;

                case OriginPosition.BottomLeft:
                    result.X = 0;
                    result.Y = texture.Height;
                    break;

                case OriginPosition.BottomCenter:
                    result.X = texture.Width / 2;
                    result.Y = texture.Height;
                    break;

                case OriginPosition.BottomRight:
                    result.X = texture.Width;
                    result.Y = texture.Height;
                    break;
            }

            return result;
        }
    }
}