using Akka.Actor;
using AkkaConsoleSpike.Messages;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaConsoleSpike.Actors
{
    public class StatefulPlaybackActor : ReceiveActor
    {
        
        private Logger _log = LogManager.GetCurrentClassLogger();
        private string _watchingMovieName;

        public StatefulPlaybackActor()
        {
            _log.Trace("{0} created", typeof(StatefulPlaybackActor).Name);
            Stopped();           
        }

        private void Playing()
        {
            _log.Trace("{0} is in playing state", typeof(StatefulPlaybackActor).Name);
            Receive<StopMovieMessage>(x => HandleMessage(x));
            Receive<PlayMovieMessage>(x => _log.Error("Cannot play movie whilst another one is playing", x.UserId));
        }

        private void Stopped()
        {
            _log.Trace("{0} is in stopped state", typeof(StatefulPlaybackActor).Name);
            Receive<PlayMovieMessage>(x => HandleMessage(x));
            Receive<StopMovieMessage>(x => _log.Error("Cannot stop movie before one is playing", x.UserId));
        }


        private void HandleMessage(PlayMovieMessage message)
        {
            _log.Trace("PlayMovieMessage: '{0}' for user {1}", message.MovieName, message.UserId);
            _watchingMovieName = message.MovieName;

            Become(Playing);
        }

        private void HandleMessage(StopMovieMessage message)
        {
            _log.Trace("StopMovieMessage: user {0}", message.UserId);
            _log.Trace("Stopping movie '{0}' for user {1}", _watchingMovieName, message.UserId);
            _watchingMovieName = null;

            Become(Stopped);
        }

    }
}
