using Akka.Actor;
using GameConsole.Actors;
using GameConsole.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var gameSystem = ActorSystem.Create("GameSystem"))
            {

                IActorRef playerCoordinatorRef = gameSystem.ActorOf<PlayerCoordinatorActor>(ActorPaths.PlayerCoordinatorActor.Name);
                ShowCommands();
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    var action = Console.ReadLine().ToLower();
                    string[] actions = action.Split(' ');
                    if (actions.Count() > 1)
                    {
                        var playerName = actions[1];
                        if (actions[0].Contains("create"))
                        {
                            playerCoordinatorRef.Tell(new CreatePlayer(playerName));
                        }
                        else if (actions[0].Contains("hit"))
                        {
                            var damage = int.Parse(actions[2]);
                            gameSystem.ActorSelection(ActorPaths.PlayerCoordinatorActor.Path + $"/{playerName}").Tell(new HitPlayer(damage));
                        }
                        else if (actions[0].Contains("display"))
                        {
                            gameSystem.ActorSelection(ActorPaths.PlayerCoordinatorActor.Path + $"/{playerName}").Tell(new DisplayStatus());
                        }
                        else if (actions[0].Contains("crash"))
                        {
                            gameSystem.ActorSelection(ActorPaths.PlayerCoordinatorActor.Path + $"/{playerName}").Tell(new SimulateError());
                        }
                        else
                        {
                            Console.WriteLine("Unknown command");
                        }
                    }
                }
            }
        }
        static void ShowCommands()
        {
            ColorConsole.WriteLine("Awailable commands:", ConsoleColor.White);
            ColorConsole.WriteLine("create <playerName>", ConsoleColor.White);
            ColorConsole.WriteLine("hit <playerName> <damage>", ConsoleColor.White);
            ColorConsole.WriteLine("display <playerName>", ConsoleColor.White);
            ColorConsole.WriteLine("error <playerName>", ConsoleColor.White);
            Console.WriteLine("");
        }
    }
}
