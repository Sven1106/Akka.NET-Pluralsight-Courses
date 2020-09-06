using System;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Configuration;
using MovieStreaming.Common.Actors;
using MovieStreaming.Common.Messages;

namespace MovieStreaming
{
    class Program
    {

        static void Main(string[] args)
        {
            ColorConsole.WriteLine("Creating MovieStreamingActorSystem", ConsoleColor.Gray);
            using (ActorSystem movieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem"))
            {
                ColorConsole.WriteLine("Creating actor supervisory hierarchy", ConsoleColor.Gray);
                movieStreamingActorSystem.ActorOf(Props.Create(() => new PlaybackActor()), "Playback");
                do
                {
                    Task.Delay(100).Wait();
                    ColorConsole.WriteLine("enter a command and hit enter", ConsoleColor.Gray);
                    var command = Console.ReadLine();

                    if (command.StartsWith("play"))
                    {
                        int userId = int.Parse(command.Split(',')[1]);
                        string movieTitle = command.Split(',')[2];
                        StartMovieMessage message = new StartMovieMessage(movieTitle, userId);
                        movieStreamingActorSystem.ActorSelection("/user/Playback/UserCoordinator").Tell(message);
                    }

                    if (command.StartsWith("stop"))
                    {
                        int userId = int.Parse(command.Split(',')[1]);
                        StopMovieMessage message = new StopMovieMessage(userId);
                        movieStreamingActorSystem.ActorSelection("/user/Playback/UserCoordinator").Tell(message);
                    }

                    if (command == "exit")
                    {
                        break;
                    }
                } while (true);
            }
            Console.ReadKey();
        }
    }
}
