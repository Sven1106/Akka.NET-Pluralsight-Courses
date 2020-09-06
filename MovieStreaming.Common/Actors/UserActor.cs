using Akka.Actor;
using MovieStreaming.Common.Messages;
using System;

namespace MovieStreaming.Common.Actors
{
    public class UserActor : ReceiveActor
    {
        private string _currentlyWatching = null;
        public UserActor()
        {
            ColorConsole.WriteLine("UserActor is being created", ConsoleColor.Cyan);
            Stopped();
        }

        private void Playing() // Behavior
        {
            Receive<StartMovieMessage>(message => ColorConsole.WriteLine($"ERROR: UserActor - {message.UserId} is already watching {message.MovieTitle}!", ConsoleColor.Red));
            Receive<StopMovieMessage>(message => StopPlayingCurrentMovie());
            ColorConsole.WriteLine("UserActor behavior: Playing", ConsoleColor.Cyan);
        }
        private void Stopped() // Behavior
        {
            Receive<StartMovieMessage>(message => StartPlayingMovie(message.MovieTitle));
            Receive<StopMovieMessage>(message => ColorConsole.WriteLine($"ERROR: UserActor - {message.UserId} is not watching any movies!", ConsoleColor.Red));
            ColorConsole.WriteLine("UserActor behavior: Stopped", ConsoleColor.Cyan);
        }

        private void StartPlayingMovie(string movieTitle)
        {
            _currentlyWatching = movieTitle;
            ColorConsole.WriteLine($"UserActor is currently watching {_currentlyWatching}", ConsoleColor.Cyan);
            Context.ActorSelection("/user/Playback/PlaybackStatistics/MoviePlayCounter").Tell(new IncrementPlayCountMessage(movieTitle));
            Become(Playing);
        }

        private void StopPlayingCurrentMovie()
        {
            ColorConsole.WriteLine($"UserActor has stopped watching {_currentlyWatching}", ConsoleColor.Cyan);
            _currentlyWatching = null;
            Become(Stopped);
        }

        #region Lifecycle Hooks
        protected override void PreStart()
        {
            ColorConsole.WriteLine("UserActor PreStart", ConsoleColor.Cyan);
        }
        protected override void PostStop()
        {
            ColorConsole.WriteLine("UserActor PostStop", ConsoleColor.Cyan);
        }
        #endregion
    }
}
