using System;
using Akka.Persistence;
using GameConsole.Commands;
using GameConsole.Events;
namespace GameConsole.Actors
{
    class PlayerActorState
    {
        public string PlayerName { get; set; }
        public int Health { get; set; }

        public override string ToString()
        {
            return $"[PlayerActorState {PlayerName} {Health}]";
        }
    }
    public class PlayerActor : ReceivePersistentActor
    {
        private PlayerActorState _state;
        private int _eventCount;

        public override string PersistenceId => $"player-{_state.PlayerName}";

        public PlayerActor(string playerName, int startingHealth)
        {
            _state = new PlayerActorState()
            {
                PlayerName = playerName,
                Health = startingHealth
            };

            ColorConsole.WriteLine($"{_state.PlayerName} created", ConsoleColor.Magenta);
            Command<HitPlayer>(command => HitPlayer(command));
            Command<DisplayStatus>(command => DisplayStatusMessage());
            Command<SimulateError>(command => SimulateError());

            Recover<PlayerHit>(playerHitEvent =>
            {
                ColorConsole.WriteLine($"{_state.PlayerName} replaying PlayerHit event {playerHitEvent} from journal", ConsoleColor.Magenta);
                _state.Health -= playerHitEvent.DamageTaken;
            });

            Recover<SnapshotOffer>(offer =>
            {
                ColorConsole.WriteLine($"{_state.PlayerName} received SnapshotOffer from snapshot store, updating state", ConsoleColor.Magenta);
                _state = (PlayerActorState)offer.Snapshot;
                ColorConsole.WriteLine($"{_state.PlayerName} state {_state} set from snapshot", ConsoleColor.Magenta);
            });
        }

        private void HitPlayer(HitPlayer command)
        {
            ColorConsole.WriteLine($"{_state.PlayerName} received HitPlayer Command", ConsoleColor.Magenta);
            var @event = new PlayerHit(command.Damage);
            ColorConsole.WriteLine($"{_state.PlayerName} persisting PlayerHit event", ConsoleColor.Magenta);
            Persist(@event, playerHitEvent =>
            {
                ColorConsole.WriteLine($"{_state.PlayerName} persisted PlayerHit event ok, updating actor state", ConsoleColor.Magenta);
                _state.Health -= playerHitEvent.DamageTaken;
                _eventCount++;
                if (_eventCount == 5)
                {
                    ColorConsole.WriteLine($"{_state.PlayerName} saving snapshot", ConsoleColor.Magenta);
                    SaveSnapshot(_state);
                    ColorConsole.WriteLine($"{_state.PlayerName} resetting event count to 0", ConsoleColor.Magenta);
                    _eventCount = 0;
                }
            });
        }
        private void DisplayStatusMessage()
        {
            ColorConsole.WriteLine($"{_state.PlayerName} received DisplayStatus command", ConsoleColor.Magenta);
            ColorConsole.WriteLine($"{_state.PlayerName} has {_state.Health} health", ConsoleColor.Magenta);
        }
        private void SimulateError()
        {
            ColorConsole.WriteLine($"{_state.PlayerName} received SimulateError command", ConsoleColor.Magenta);
            throw new ApplicationException($"Simulated exception in player: {_state.PlayerName}");
        }
    }
}
