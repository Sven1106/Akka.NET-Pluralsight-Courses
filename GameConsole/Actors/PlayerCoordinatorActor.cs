using System;
using Akka.Actor;
using Akka.Persistence;
using GameConsole.Commands;
using GameConsole.Events;

namespace GameConsole.Actors
{
    public class PlayerCoordinatorActor : ReceivePersistentActor
    {
        public override string PersistenceId => "player-coordinator";
        private readonly int DefaultStartingHealth = 100;
        public PlayerCoordinatorActor()
        {
            Command<CreatePlayer>(command =>
            {
                ColorConsole.WriteLine($"{ActorPaths.PlayerCoordinatorActor.Name} received CreatePlayer command for {command.PlayerName}", ConsoleColor.Cyan);
                var @event = new PlayerCreated(command.PlayerName);
                Persist(@event, playerCreatedEvent =>
                {
                    ColorConsole.WriteLine($"{ActorPaths.PlayerCoordinatorActor.Name} persisted a PlayerCreated event for {playerCreatedEvent.PlayerName}", ConsoleColor.Cyan);
                    Context.ActorOf(Props.Create(() => new PlayerActor(playerCreatedEvent.PlayerName, DefaultStartingHealth)), playerCreatedEvent.PlayerName);
                });
            });

            Recover<PlayerCreated>(playerCreatedEvent =>
            {
                ColorConsole.WriteLine($"{ActorPaths.PlayerCoordinatorActor.Name} replaying a PlayerCreated event for {playerCreatedEvent.PlayerName} from journal", ConsoleColor.Cyan);
                Context.ActorOf(Props.Create(() => new PlayerActor(playerCreatedEvent.PlayerName, DefaultStartingHealth)), playerCreatedEvent.PlayerName);
            });
        }


    }
}
