using Anchored.Debug.Console;
using Anchored.Util.Math;
using Anchored.Streams;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Anchored.Util.Math.Tween;

namespace Anchored.Assets
{
	public class Audio
	{
		private static Dictionary<string, SoundEffect> sounds = new Dictionary<string, SoundEffect>();

		public static float MasterVolume = 1f;
		public static float SfxVolume = 1f;
		public static float SfxVolumeBuffer = 1f;
		public static float SfxVolumeBufferResetTimer = 0f;
		public static bool Repeat = true;

		public static void Update(float dt)
		{
			if (SfxVolumeBufferResetTimer > 0)
			{
				SfxVolumeBufferResetTimer -= dt;

				if (SfxVolumeBufferResetTimer <= 0)
				{
					Tween.To(1, SfxVolumeBuffer, x => SfxVolumeBuffer = x, 0.3f);
				}
			}
		}

		public static void Destroy()
		{
			foreach (var sound in sounds.Values)
			{
				sound.Dispose();
				GC.SuppressFinalize(sound);
			}
		}

		public static void Load()
		{
			LoadSfx(FileHandle.FromNearRoot("sfx\\"), "", true);
		}

		public static void PlaySfx(string id, float volume = 1, float pitch = 0, float pan = 0)
		{
			if (!AssetManager.LoadSfx)
				return;

			PlaySfx(GetSfx(id), volume * SfxVolumeBuffer, pitch, pan);
		}

		public static SoundEffect GetSfx(string id)
		{
			SoundEffect effect;

			if (sounds.TryGetValue(id, out effect))
				return effect;

			DebugConsole.Error($"Sound effect \'{id}\' was not found!");
			return null;
		}

		public static void PlaySfx(SoundEffect sfx, float volume = 1, float pitch = 0, float pan = 0)
		{
			if (!AssetManager.LoadSfx)
				return;

			sfx?.Play(MathHelper.Clamp(0f, 1f, volume * SfxVolume * MasterVolume), pitch, pan);
		}

		private static void LoadSfx(FileHandle file, string path, bool root = false)
		{
			if (file.Exists())
			{
				path = $"{path}{file.Name}{(root ? "" : "\\")}";

				foreach (var sfx in file.ListFileHandles())
				{
					if (sfx.Extension == ".xnb")
					{
						LoadSfx(sfx, path);
					}
				}

				foreach (var dir in file.ListDirectoryHandles())
				{
					LoadSfx(dir, path);
				}
			}
			else
			{
				DebugConsole.Error($"File \'{file.Name}\' is missing.");
			}
		}

		private static void LoadSfx(FileHandle sfx, string path)
		{
			string folder = sfx.FullPath;
			folder = folder.Remove(sfx.FullPath.Length - sfx.Name.Length, sfx.Name.Length);
			folder = folder.Remove(0, (AssetManager.Root + "\\sfx").Length);
			string id = folder + sfx.NameWithoutExtension;
			sounds[$"{id}"] = AssetManager.Content.Load<SoundEffect>($"sfx\\{id}");
		}
	}
}
