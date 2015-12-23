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
    public static class StatefulDemo
    {
        private static ActorSystem _myActorSystem;
        private static Logger _log = LogManager.GetCurrentClassLogger();

        public static void Run()
        {

            using (_myActorSystem = ActorSystem.Create("MyActorSystem"))
            {
                _log.Trace("Actor system created");

                var playbackActorRef = _myActorSystem.ActorOf<StatefulPlaybackActor>();

                Tell(playbackActorRef, new PlayMovieMessage("Terminator 2", 324));
                Tell(playbackActorRef, new PlayMovieMessage("Jesus 3000AD", 324));
                Tell(playbackActorRef, new StopMovieMessage(324));
                Tell(playbackActorRef, new StopMovieMessage(324));
                Tell(playbackActorRef, new PlayMovieMessage("Twi-wtf", 324));
                Tell(playbackActorRef, new StopMovieMessage(324));
                Tell(playbackActorRef, PoisonPill.Instance);

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

        private static void Tell<T>(IActorRef actorRef, T message)
        {
            _log.Trace("Telling {0}: {1}", actorRef.Path, message.GetType().Name);

            actorRef.Tell(message);
        }

    }
}
