using Akka.Actor;
using MovieStreaming.Common.Exceptions;
using System;

namespace MovieStreaming.Common.Actors
{
    public class PlaybackStatisticsActor : ReceiveActor
    {
        public PlaybackStatisticsActor()
        {
            Context.ActorOf(Props.Create<MoviePlayCounterActor>(), "MoviePlayCounter");
        }



        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(
                    exception =>
                    {
                        if (exception is SimulatedCorruptStateException)
                        {
                            return Directive.Restart;
                        }
                        if (exception is SimulatedTerribleMovieException)
                        {
                            return Directive.Resume;
                        }
                        return Directive.Restart;
                    }
                );

        }


        #region Lifecycle Hooks
        protected override void PreStart()
        {
            ColorConsole.WriteLine("PlaybackStatisticsActor PreStart", ConsoleColor.White);
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLine("PlaybackStatisticsActor PostStop", ConsoleColor.White);
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLine("PlaybackStatisticsActor PreRestart because: " + reason, ConsoleColor.White);
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLine("PlaybackStatisticsActor PostRestart because: " + reason, ConsoleColor.White);
            base.PostRestart(reason);
        }
        #endregion
    }
}
