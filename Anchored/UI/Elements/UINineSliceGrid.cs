using Anchored.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.UI.Elements
{
	public class UINineSliceGrid : UIComponent
	{
		private TextureRegion texture;
		private int textureWidth;
		private int textureHeight;

		private int width;
		private int height;

		private Vector2 scale;

		private TextureRegion texture_topLeft;
		private TextureRegion texture_topCenter;
		private TextureRegion texture_topRight;
		private TextureRegion texture_centerLeft;
		private TextureRegion texture_centerCenter;
		private TextureRegion texture_centerRight;
		private TextureRegion texture_bottomLeft;
		private TextureRegion texture_bottomCenter;
		private TextureRegion texture_bottomRight;

		public float LayerDepth = 0.95f;

		public UINineSliceGrid(TextureRegion texture, int gridW, int gridH, int width, int height, float scale = 4)
		{
			this.texture = texture;
			this.textureWidth = gridW;
			this.textureHeight = gridH;

			this.width = width;
			this.height = height;

			this.scale.X = scale;
			this.scale.Y = scale;
		}

		public override void Init()
		{
			Width = (int)(width * textureWidth * scale.X);
			Height = (int)(height * textureHeight * scale.Y);
			InitTextures();
		}

		public override void Draw(SpriteBatch sb)
		{
			base.Draw(sb);

			// draw top
			{
				texture_topLeft.Draw(
					Position,
					Vector2.Zero,
					Color.White,
					0f,
					scale,
					sb,
					LayerDepth
				);

				for (int xx = 1; xx < width - 1; xx++)
				{
					float xPos = Position.X + (xx * textureWidth * scale.X);
					var pos = new Vector2(xPos, Position.Y);

					texture_topCenter.Draw(
						pos,
						Vector2.Zero,
						Color.White,
						0f,
						scale,
						sb,
						LayerDepth
					);
				}

				texture_topRight.Draw(
					Position + new Vector2((width - 1) * textureWidth * scale.X, 0),
					Vector2.Zero,
					Color.White,
					0f,
					scale,
					sb,
					LayerDepth
				);
			}

			// draw center
			{
				for (int yy = 1; yy < height - 1; yy++)
				{
					float yPos = Position.Y + (yy * textureHeight * scale.Y);

					texture_centerLeft.Draw(
						new Vector2(Position.X, yPos),
						Vector2.Zero,
						Color.White,
						0f,
						scale,
						sb,
						LayerDepth
					);

					for (int xx = 1; xx < width - 1; xx++)
					{
						float xPos = Position.X + (xx * textureWidth * scale.X);
						var pos = new Vector2(xPos, yPos);

						texture_centerCenter.Draw(
							pos,
							Vector2.Zero,
							Color.White,
							0f,
							scale,
							sb,
							LayerDepth
						);
					}

					texture_centerRight.Draw(
						new Vector2(Position.X + ((width - 1) * textureWidth * scale.X), yPos),
						Vector2.Zero,
						Color.White,
						0f,
						scale,
						sb,
						LayerDepth
					);
				}
			}

			// draw bottom
			{
				float yPos = Position.Y + ((height - 1) * textureHeight * scale.Y);

				texture_bottomLeft.Draw(
					new Vector2(Position.X, yPos),
					Vector2.Zero,
					Color.White,
					0f,
					scale,
					sb,
					LayerDepth
				);

				for (int xx = 1; xx < width - 1; xx++)
				{
					float xPos = Position.X + (xx * textureWidth * scale.X);
					var pos = new Vector2(xPos, yPos);

					texture_bottomCenter.Draw(
						pos,
						Vector2.Zero,
						Color.White,
						0f,
						scale,
						sb,
						LayerDepth
					);
				}

				texture_bottomRight.Draw(
					new Vector2(Position.X + ((width - 1) * textureWidth * scale.X), yPos),
					Vector2.Zero,
					Color.White,
					0f,
					scale,
					sb,
					LayerDepth
				);
			}
		}

		private void InitTextures()
		{
			texture_topLeft      = new TextureRegion(texture.Texture, new Rectangle(textureWidth * 0, textureHeight * 0, textureWidth, textureHeight));
			texture_topCenter    = new TextureRegion(texture.Texture, new Rectangle(textureWidth * 1, textureHeight * 0, textureWidth, textureHeight));
			texture_topRight     = new TextureRegion(texture.Texture, new Rectangle(textureWidth * 2, textureHeight * 0, textureWidth, textureHeight));
			texture_centerLeft   = new TextureRegion(texture.Texture, new Rectangle(textureWidth * 0, textureHeight * 1, textureWidth, textureHeight));
			texture_centerCenter = new TextureRegion(texture.Texture, new Rectangle(textureWidth * 1, textureHeight * 1, textureWidth, textureHeight));
			texture_centerRight  = new TextureRegion(texture.Texture, new Rectangle(textureWidth * 2, textureHeight * 1, textureWidth, textureHeight));
			texture_bottomLeft   = new TextureRegion(texture.Texture, new Rectangle(textureWidth * 0, textureHeight * 2, textureWidth, textureHeight));
			texture_bottomCenter = new TextureRegion(texture.Texture, new Rectangle(textureWidth * 1, textureHeight * 2, textureWidth, textureHeight));
			texture_bottomRight  = new TextureRegion(texture.Texture, new Rectangle(textureWidth * 2, textureHeight * 2, textureWidth, textureHeight));
		}
	}
}
