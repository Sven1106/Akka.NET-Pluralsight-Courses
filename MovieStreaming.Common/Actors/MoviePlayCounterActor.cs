using Akka.Actor;
using MovieStreaming.Common.Exceptions;
using MovieStreaming.Common.Messages;
using System;
using System.Collections.Generic;

namespace MovieStreaming.Common.Actors
{
    public class MoviePlayCounterActor : ReceiveActor
    {
        private readonly Dictionary<string, int> _moviePlayCounts;
        public MoviePlayCounterActor()
        {
            _moviePlayCounts = new Dictionary<string, int>();
            Receive<IncrementPlayCountMessage>(message => HandleIncrementMessage(message));
        }

        private void HandleIncrementMessage(IncrementPlayCountMessage message)
        {
            if (!_moviePlayCounts.ContainsKey(message.MovieTitle))
            {
                _moviePlayCounts.Add(message.MovieTitle, 0);
            }
            _moviePlayCounts[message.MovieTitle]++;
            if (_moviePlayCounts[message.MovieTitle] > 3)
            {
                throw new SimulatedCorruptStateException();
            }
            if (message.MovieTitle == "Partial Recoil")
            {
                throw new SimulatedTerribleMovieException();
            }

            ColorConsole.WriteLine($"MoviePlayCountActor '{message.MovieTitle}' has been watched {_moviePlayCounts[message.MovieTitle]} times", ConsoleColor.Magenta);
        }
        #region Lifecycle Hooks
        protected override void PreStart()
        {
            ColorConsole.WriteLine("MoviePlayCounterActor PreStart", ConsoleColor.Magenta);
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLine("MoviePlayCounterActor PostStop", ConsoleColor.Magenta);
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLine("MoviePlayCounterActor PreRestart because: " + reason, ConsoleColor.Magenta);
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLine("MoviePlayCounterActor PostRestart because: " + reason, ConsoleColor.Magenta);
            base.PostRestart(reason);
        }
        #endregion
    }
}