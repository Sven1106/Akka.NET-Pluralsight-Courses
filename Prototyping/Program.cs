using System;
using Akka.Actor;
using Prototyping.Actors;


namespace Prototyping
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var gameSystem = ActorSystem.Create("GameSystem"))
            {

                IActorRef playerCoordinatorRef = gameSystem.ActorOf<PlayerCoordinatorActor>(ActorPaths.PlayerCoordinatorActor.Name);
                while (true)
                {

                    Console.ReadKey();
                }
            }
        }
    }
}
