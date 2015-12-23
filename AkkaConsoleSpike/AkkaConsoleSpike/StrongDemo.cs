using Akka.Actor;
using AkkaConsoleSpike.Actors;
using AkkaConsoleSpike.Messages;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaConsoleSpike
{
    public static class StrongDemo
    {

        private static ActorSystem _myActorSystem;
        private static Logger _log = LogManager.GetCurrentClassLogger();

        public static void Run()
        {

            using (_myActorSystem = ActorSystem.Create("MyActorSystem"))
            {
                _log.Trace("Actor system created");

                var playbackActorRef = _myActorSystem.ActorOf<PlaybackActor>();

                playbackActorRef.Tell(new PlayMovieMessage("Terminator 2", 324));
                AwaitUser();
                playbackActorRef.Tell(new PlayMovieMessage("Small World", 324));
                AwaitUser();
                playbackActorRef.Tell(new StopMovieMessage(324));
                playbackActorRef.Tell(new PlayMovieMessage("Twi-wtf", 324));
                playbackActorRef.Tell(new StopMovieMessage(324));
                AwaitUser();
                playbackActorRef.Tell(PoisonPill.Instance);

                AwaitUser();

                _log.Trace("Actor system shutting down");

                _myActorSystem.Shutdown();
                _myActorSystem.AwaitTermination();
            }

            AwaitUser();

        }

        public static void AwaitUser()
        {
            _log.Info("<Press enter to continue>");
            Console.ReadLine();
        }
    }
}
