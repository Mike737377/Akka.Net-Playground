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
    public class PlaybackActor : ReceiveActor
    {
        private Logger _log = LogManager.GetCurrentClassLogger();
        private PlayMovieMessage _playingMovie;

        public PlaybackActor()
        {
            _log.Trace("{0} created", typeof(PlaybackActor).Name);

            Receive<PlayMovieMessage>(x => HandleMessage(x));
            Receive<StopMovieMessage>(x => HandleMessage(x));
        }

        private void HandleMessage(StopMovieMessage message)
        {
            _log.Trace("StopMovieMessage: user {1}", message.UserId);

            if (_playingMovie == null)
            {
                _log.Warn("User is currently not watching a movie");
                return;
            }

            _log.Trace("Stopping movie '{0}' for user {1}", _playingMovie.MovieName, message.UserId);
            _playingMovie = null;
        }

        private void HandleMessage(PlayMovieMessage message)
        {
            _log.Trace("PlayMovieMessage: '{0}' for user {1}", message.MovieName, message.UserId);
            _playingMovie = message;
        }

        protected override void PreStart()
        {
            _log.Debug("{0} starting", typeof(PlaybackActor).Name);
        }

        protected override void PostStop()
        {
            _log.Debug("{0} stopping", typeof(PlaybackActor).Name);
        }

        protected override void PreRestart(Exception reason, object message)
        {
            _log.Error(reason);

            base.PreRestart(reason, message);
        }

    }
}
