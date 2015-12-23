using Akka.Actor;
using AkkaConsoleSpike.FlixDomain.Actors;
using AkkaConsoleSpike.FlixDomain.Messages;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AkkaConsoleSpike.FlixDomain
{
    public class Example
    {
        private readonly Logger _log = LogManager.GetCurrentClassLogger();
        private ActorSystem _actorSystem;
        private readonly Dictionary<string, Action> _commands = new Dictionary<string, Action>();
        private bool _quit = false;

        public Example()
        {
            _commands.Add("play", Play);
            _commands.Add("stop", Stop);
            _commands.Add("help", PrintHelp);
            _commands.Add("exit", Exit);
        }

        private void Exit()
        {
            _quit = true;
        }

        public void Run()
        {
            _log.Trace("Starting actor system");
            using (_actorSystem = ActorSystem.Create("FlixSystem"))
            {
                _log.Trace("Creating playback actor");
                var playbackActorRef = _actorSystem.ActorOf(Props.Create<PlaybackActor>(), "Playback");

                Thread.Sleep(1000);

                PrintHelp();

                do
                {
                    Thread.Sleep(250);
                    Console.WriteLine();
                    Console.WriteLine();

                    Console.Write("Command: ");
                    var command = Console.ReadLine().Trim();

                    if (_commands.ContainsKey(command))
                    {
                        _commands[command]();
                    }
                    else
                    {
                        Console.WriteLine("Unknown command");
                    }                   

                } while(!_quit);

                _log.Trace("Shutting down actor system");
                _actorSystem.Shutdown();
                _actorSystem.AwaitTermination();
            }
        }

        private void PrintHelp()
        {
            Console.WriteLine("Commands are:");
            foreach (var commandItem in _commands)
            {
                Console.WriteLine("\t-" + commandItem.Key);
            }
        }

        private void Play()
        {
            Console.Write("UserId: ");
            var userId = Console.ReadLine();
                        
            Console.Write("Movie: ");
            var movie = Console.ReadLine();
            
            var message = new PlayMovieMessage(Convert.ToInt32(userId), movie);
            _actorSystem.ActorSelection("/user/Playback/UserCoordinator").Tell(message);
        }

        private void Stop()
        {
            Console.Write("UserId: ");
            var userId = Console.ReadLine();

            var message = new StopMovieMessage(Convert.ToInt32(userId));
            _actorSystem.ActorSelection("/user/Playback/UserCoordinator").Tell(message);
        }

    }
}
