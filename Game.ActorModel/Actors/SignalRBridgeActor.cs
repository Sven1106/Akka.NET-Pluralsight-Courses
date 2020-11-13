using Akka.Actor;
using Game.ActorModel.ExternalSystems;
using Game.ActorModel.Messages;

namespace Game.ActorModel.Actors
{
    public class SignalRBridgeActor : ReceiveActor // This is used to connect SignalR and the actor system.
    {
        private readonly IActorRef _gameController; //
        private readonly IGameEventsPusher _gameEventPusher;
        public SignalRBridgeActor(IGameEventsPusher gameEventPusher, IActorRef gameController)
        {
            _gameController = gameController;
            _gameEventPusher = gameEventPusher;

            Receive<JoinGameMessage>(message =>
            {
                _gameController.Tell(message);
            });
            Receive<AttackPlayerMessage>(message =>
            {
                _gameController.Tell(message);
            });
            Receive<PlayerStatusMessage>(message =>
            {
                _gameEventPusher.PlayerJoined(message.PlayerName, message.Health);
            });

            Receive<PlayerHealthChangedMessage>(message =>
            {
                _gameEventPusher.UpdatePlayerHealth(message.PlayerName, message.Health);
            });
        }
    }
}
