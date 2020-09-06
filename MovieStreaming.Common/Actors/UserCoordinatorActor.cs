using Akka.Actor;
using MovieStreaming.Common.Messages;
using System;
using System.Collections.Generic;

namespace MovieStreaming.Common.Actors
{
    public class UserCoordinatorActor : ReceiveActor
    {
        private readonly Dictionary<int, IActorRef> _users;
        public UserCoordinatorActor()
        {
            _users = new Dictionary<int, IActorRef>();
            Receive<StartMovieMessage>(message =>
            {
                CreateChildUserActorIfNotExists(message.UserId);
                IActorRef childActorRef = _users[message.UserId];
                childActorRef.Tell(message);
            });

            Receive<StopMovieMessage>(message =>
            {
                CreateChildUserActorIfNotExists(message.UserId);
                IActorRef childActorRef = _users[message.UserId];
                childActorRef.Tell(message);
            });
        }

        private void CreateChildUserActorIfNotExists(int userId)
        {
            if (!_users.ContainsKey(userId))
            {
                var newChildActorRef = Context.ActorOf(Props.Create<UserActor>(), $"User{userId}");
                _users.Add(userId, newChildActorRef);
                ColorConsole.WriteLine($"UserCoordinatorActor created new child UserActor for {userId} (Total Users: {_users.Count})", ConsoleColor.Cyan);
            }
        }

        #region Lifecycle Hooks
        protected override void PreStart()
        {
            ColorConsole.WriteLine("UserCoordinatorActor PreStart", ConsoleColor.Cyan);
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLine("UserCoordinatorActor PostStop", ConsoleColor.Cyan);
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLine("UserCoordinatorActor PreRestart because: " + reason, ConsoleColor.Cyan);
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLine("UserCoordinatorActor PostRestart because: " + reason, ConsoleColor.Cyan);
            base.PostRestart(reason);
        }
        #endregion
    }
}
