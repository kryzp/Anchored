using Anchored.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Anchored.Graphics.Animating
{
	public delegate void AnimationCallback();

	public class Animation
	{
		private AnimationFrame frame;

		private uint currentFrame;
		public uint StartFrame { get; private set; }
		public uint EndFrame { get; private set; }

		public float SpeedModifier = 1f;
		public bool Paused;
		public bool AutoStop;
		public bool Reverse;

		public uint TagSize => (EndFrame - StartFrame + 1);

		public uint Frame
		{
			get => currentFrame;
			set
			{
				var v = value % TagSize;

				if (v != currentFrame)
				{
					currentFrame = v;
					UpdateFrame();
				}
			}
		}

		private string layer;
		private string tag;

		public string Layer
		{
			get => layer;

			set
			{
				if (layer == value)
					return;

				layer = value;
				UpdateFrame();
			}
		}

		public string Tag
		{
			get => tag;

			set
			{
				currentFrame = 0;
				tag = value;
				Paused = false;

				UpdateFrame();
			}
		}

		private float timer;

		public float Timer
		{
			get => timer;

			set
			{
				timer = value;

				if (timer >= frame.Duration)
				{
					timer = 0;

					if (!AutoStop || currentFrame < EndFrame - StartFrame)
					{
						Frame += 1;

						if (SkipNextFrame)
						{
							SkipNextFrame = false;
							Frame += 1;
						}

						UpdateFrame();
					}
					else
					{
						Paused = true;
						OnEnd?.Invoke();
					}
				}
			}
		}

		public AnimationFrame CurrentFrame => frame;

		public bool PingGoingForward;
		public bool SkipNextFrame;

		public AnimationData Data;
		public AnimationCallback OnEnd;

		public Animation(AnimationData data, string layer = null)
		{
			Data = data;

			if (layer != null)
				Layer = layer;
			else
				UpdateFrame(true);
		}

		public void Update()
		{
			if (!Paused)
			{
				var frame = currentFrame;
				Timer += Time.Delta * SpeedModifier;
				var newFrame = currentFrame;

				if ((frame != newFrame) && (newFrame == 0))
					OnEnd?.Invoke();
			}
		}

		public void Draw(SpriteBatch sb, Vector2 position, Vector2 origin, float rotation, float layer, Vector2 scale, Color colour, SpriteEffects flip = SpriteEffects.None)
		{
			sb.Draw(
				frame.Texture.Texture,
				position,
				frame.Bounds,
				colour,
				rotation,
				origin,
				scale,
				flip,
				layer
			);
		}

		public TextureRegion GetCurrentTexture()
		{
			return frame.Texture;
		}

		public TextureRegion GetFirstCurrent()
		{
			return (
				(tag == null)
					? (Data.GetFrame(layer, 0)?.Texture)
					: (GetFrame(tag, 0))
			);
		}

		public void Reset()
		{
			Frame = 0;
		}

		public bool HasTag(string tag)
		{
			return Data.Tags.ContainsKey(tag);
		}

		private void UpdateFrame(bool rand = false)
		{
			var nullableTag = Data.GetTag(tag);

			if (nullableTag == null)
				return;

			var currentTag = nullableTag.Value;

			StartFrame = currentTag.StartFrame;
			EndFrame = currentTag.EndFrame;

			if (rand)
				currentFrame = (uint)Rng.Int(0, (int)(currentTag.EndFrame - currentTag.StartFrame));

			var frame = Data.GetFrame(layer, currentTag.Direction.GetFrameId(this, Reverse));

			if (frame != null)
				this.frame = (AnimationFrame)frame;
		}

		public TextureRegion GetFrame(string tag, int frame)
		{
			if (tag == null || !Data.Tags.TryGetValue(tag, out var t))
				return null;

			return Data.GetFrame(layer, (uint)(t.StartFrame + frame))?.Texture;
		}

		public void Randomize()
		{
			Frame = (uint)Rng.Int((int)TagSize);
		}
	}
}
