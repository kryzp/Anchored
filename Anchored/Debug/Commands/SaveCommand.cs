using System.Threading;
using Anchored.Save;

namespace Anchored.Debug.Commands
{
    public class SaveCommand : ConsoleCommand
    {
        public SaveCommand()
            : base("save", "sv", "Saves the game", "")
        {
        }

        public override void Run(string[] args)
        {
            if (args.Length == 0)
            {
                DebugConsole.Log("Not enough arguments given.");
                return;
            }
            
            if (args.Length >= 3)
            {
                DebugConsole.Log("Too many arguments given.");
                return;
            }

            var saveType = (args.Length == 1) ? "all" : args[1];
            var world = DebugConsole.World;
			
            var thread = new Thread(() =>
            {
                switch (saveType)
                {
                    case "all":
                    {
                        SaveManager.Save(world, SaveType.Level);
                        SaveManager.Save(world, SaveType.Player);
                        SaveManager.Save(world, SaveType.Game);
                        SaveManager.Save(world, SaveType.Global);
                        break;
                    }

                    case "level":
                    {
                        SaveManager.Save(world, SaveType.Level);
                        break;
                    }

                    case "player":
                    {
                        SaveManager.Save(world, SaveType.Player);
                        break;
                    }

                    case "game":
                    {
                        SaveManager.Save(world, SaveType.Game);
                        break;
                    }

                    case "global":
                    {
                        SaveManager.Save(world, SaveType.Global);
                        break;
                    }

                    case "run":
                    {
                        SaveManager.Save(world, SaveType.Level);
                        SaveManager.Save(world, SaveType.Player);
                        SaveManager.Save(world, SaveType.Game);
                        break;
                    }

                    default:
                    {
                        DebugConsole.Log($"Unknown save type \'{saveType}\'. Should be one of all, level, player, game, global, run, prefab");
                        break;
                    }
                }
					
                DebugConsole.Log($"Finished saving via: \'{saveType}\'");
            });
			
            thread.Start();
        }
    }
}