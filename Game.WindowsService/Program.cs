using System;
using Akka.Actor;
using Game.ActorModel.Actors;
using Topshelf;
using Topshelf.Runtime.Windows;

namespace Game.WindowsService
{
    public class GameStateService
    {
        private ActorSystem ActorSystemInstance;

        public void Start()
        {
            ActorSystemInstance = ActorSystem.Create("GameSystem");
            var gameController = ActorSystemInstance.ActorOf<GameControllerActor>("GameController");
        }
        public void Stop()
        {
            ActorSystemInstance.Terminate();
        }
    }

    class Program // To install the service on windows. Open cmd as admin and CD to the build folder and type the service name and install. eg. "Game.WindowsService install". Use uninstall to remove service from windows.
    {
        static void Main(string[] args)
        {
            HostFactory.Run(gameService => {
                gameService.Service<GameStateService>(s => {
                    s.ConstructUsing(game => new GameStateService());
                    s.WhenStarted(game => game.Start());
                    s.WhenStopped(game => game.Stop());
                });
                gameService.RunAsLocalSystem();
                gameService.StartAutomatically();
                gameService.SetDescription("PSDemo Game Topshelf Service");
                gameService.SetDisplayName("PSDemoGame");
                gameService.SetServiceName("PSDemoGame");
            });
        }
    }
}
