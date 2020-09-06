using Akka.Actor;
using Akka.Configuration;
using System;
namespace MovieStreaming.Remote
{
    class Program
    {
        static void Main(string[] args)
        {
            ColorConsole.WriteLine("Creating MovieStreamingActorSystem in remote process", ConsoleColor.Gray);
            using (ActorSystem movieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystemRemote"))
            {
                Console.ReadKey();
            }
        }
    }
}
