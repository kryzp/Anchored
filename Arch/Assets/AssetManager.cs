using Arch.Assets.Maps;
using Arch.Assets.Textures;
using Microsoft.Xna.Framework.Content;
using System.IO;

namespace Arch.Assets
{
	public class AssetManager
	{
#if DEBUG
		public static bool ImGuiEnabled = true;
		public static bool LoadOriginalFiles = false;
		public static bool LoadMusic = false;
		public static bool LoadSfx = false;
		public const bool Reload = false;
		public static bool LoadMods = false;
#else
		public static bool ImGuiEnabled = false;
		public static bool LoadOriginalFiles = false;
		public static bool LoadMusic = true;
		public static bool LoadSfx = true;
		public static bool Reload = false;
		public static bool LoadMods = true;
#endif

		public static string Root
		{
			get
			{
				return (LoadOriginalFiles)
					? (Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\Content\\"))
					: (NearRoot);
			}
		}

		public static string NearRoot => $"{Directory.GetCurrentDirectory()}\\Content\\";

		public static ContentManager Content;

		public static void Load(ref int progress)
		{
			LoadAssets(ref progress);
		}

		private static void LoadAssets(ref int progress)
		{
			if (Locales.Map == null)
			{
				Locales.Load(Locales.PrefferedClientLanguage);
				progress += 1;
			}

			EffectManager.Load();
			progress += 1;
		
			TextureManager.Load();
			TilesetManager.Load();
			progress += 1;

			FontManager.Load();
			progress += 1;

			if (LoadSfx)
			{
				Audio.Load();
				progress += 1;
			}
		}

		public static void Destroy()
		{
			DestroyAssets();
		}

		private static void DestroyAssets()
		{
			EffectManager.Destroy();
			TextureManager.Destroy();
			FontManager.Destroy();
			Audio.Destroy();
		}
	}
}
