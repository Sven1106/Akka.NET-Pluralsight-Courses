using System;
using Akka.Actor;


namespace MovieStreaming.Common.Actors
{
    public class PlaybackActor : ReceiveActor
    {
        public PlaybackActor()
        {
            ColorConsole.WriteLine("Creating PlaybackActor", ConsoleColor.Green);
            Context.ActorOf(Props.Create<UserCoordinatorActor>(), "UserCoordinator");
            Context.ActorOf(Props.Create<PlaybackStatisticsActor>(), "PlaybackStatistics");
        }

        #region Lifecycle Hooks
        protected override void PreStart()
        {
            ColorConsole.WriteLine("PlaybackActor PreStart", ConsoleColor.Green);
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLine("PlaybackActor PostStop", ConsoleColor.Green);
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLine("PlaybackActor PreRestart because: " + reason, ConsoleColor.Green);
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLine("PlaybackActor PostRestart because: " + reason, ConsoleColor.Green);
            base.PostRestart(reason);
        }
        #endregion
    }
}
