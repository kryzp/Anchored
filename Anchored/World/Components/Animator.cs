using Anchored.Graphics.Animating;
using System.Collections.Generic;

namespace Anchored.World.Components
{
	public class Animator : Component, IUpdatable
	{
		private Sprite sprite = null;
		private Dictionary<string, Animation> animations = new Dictionary<string, Animation>();

		public Animation CurrentAnimation { get; set; }

		public int Order { get; set; } = 0;

		public bool Paused { get; private set; }

		public Animator()
		{
		}

		public Animator(Sprite sprite, Dictionary<string, Animation> animations)
		{
			this.sprite = sprite;
			this.animations = animations;
		}

		public void Update()
		{
			if (Paused || sprite == null)
				return;

			CurrentAnimation.Update();
			sprite.Texture = CurrentAnimation.GetCurrentTexture();
		}

		public void Add(string name, Animation animation)
		{
			animations.Add(name, animation);
		}

		public void Play(string name)
		{
			CurrentAnimation = animations[name];
			sprite.Texture = CurrentAnimation.GetCurrentTexture();
		}

		public void Pause()
		{
			Paused = true;
		}
	}
}
