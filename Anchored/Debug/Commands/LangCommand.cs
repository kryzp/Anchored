using System;
using Anchored.Assets;
using Anchored.Debug;
using Arch.Assets;

namespace Anchored.Debug.Commands
{
    public class LangCommand : ConsoleCommand
    {
        public LangCommand()
            : base("language", "lang", "Sets the current game language", "<lang>")
        {
        }

        public override void Run(string[] args)
        {
            if (args.Length == 0)
            {
                DebugConsole.Error("Not enough arguments given.");
                return;
            }

            string lang = args[0].ToLower();
            SetLang(lang);
        }

        private void SetLang(string l)
        {
            if (Enum.TryParse(l, true, out Locales.ClientLanguage result))
            {
                Locales.Load(result);
                DebugConsole.Log($"Loaded up language: \'{result.ToString()}\'");
            }
            else
            {
                DebugConsole.Error("Such a language does not exist / is not implemented!");
            }
        }
    }
}
