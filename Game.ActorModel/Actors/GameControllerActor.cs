using Akka.Actor;
using Game.ActorModel.Messages;
using System;
using System.Collections.Generic;

namespace Game.ActorModel.Actors
{
    public class GameControllerActor : ReceiveActor
    {
        private readonly Dictionary<string, IActorRef> _players;
        public GameControllerActor()
        {
            _players = new Dictionary<string, IActorRef>();

            Receive<JoinGameMessage>(message => JoinGame(message));
            Receive<AttackPlayerMessage>(message => {
                _players[message.PlayerName].Forward(message); //The Forward()-method will preserve the original sender(SignalRBridgeActor) of the AttackPlayerMessage, and not the GameControllerActor.
            });
        }

        private void JoinGame(JoinGameMessage message)
        {
            var playerNeedCreating = !_players.ContainsKey(message.PlayerName);

            if (playerNeedCreating)
            {
                IActorRef newPlayerActor = Context.ActorOf(Props.Create(() => new PlayerActor(message.PlayerName)), message.PlayerName);
                _players.Add(message.PlayerName, newPlayerActor);
            }

            foreach (var player in _players.Values)
            {
                player.Tell(new RefreshPlayerStatusMessage(), Sender); // By default the sender of this RefreshPlayerStatusMessage would be GameControllerActor. We change it to the sender of the JoinGameMessage AKA SignalRBridgeActor.
            }
        }
    }
}
