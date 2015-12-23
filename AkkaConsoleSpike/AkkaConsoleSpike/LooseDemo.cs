using Akka.Actor;
using AkkaConsoleSpike.Actors;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaConsoleSpike
{
    public static class LooseDemo
    {

        private static ActorSystem _myActorSystem;
        private static Logger _log = LogManager.GetCurrentClassLogger();

        public static void Run()
        {

            using (_myActorSystem = ActorSystem.Create("MyActorSystem"))
            {
                _log.Trace("Actor system created");

                var playbackActorProps = Props.Create<LoosePlaybackActor>();

                var loosePlaybackActorRef = _myActorSystem.ActorOf<LoosePlaybackActor>(); //playbackActorProps);
                var playbackActorRef = _myActorSystem.ActorOf<PlaybackActor>();

                loosePlaybackActorRef.Tell("play: Terminator2");
                loosePlaybackActorRef.Tell(42);
                loosePlaybackActorRef.Tell('c');
                
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
