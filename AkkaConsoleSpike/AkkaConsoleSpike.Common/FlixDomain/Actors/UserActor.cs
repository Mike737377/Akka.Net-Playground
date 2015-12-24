using Akka.Actor;
using AkkaConsoleSpike.FlixDomain.Messages;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaConsoleSpike.FlixDomain.Actors
{
    public class UserActor : LoggedReceiveActor
    {
        public int UserId { get; private set; }
        private string _watchingMovieName;

        public UserActor(int userId)
        {
            UserId = userId;
            Stopped();
        }

        private void Playing()
        {
            _log.Info("User {0} is watching {1}", UserId, _watchingMovieName);
            Receive<StopMovieMessage>(x => HandleMessage(x));
            Receive<PlayMovieMessage>(x => _log.Error("Cannot play movie whilst another one is playing", x.UserId));
        }

        private void Stopped()
        {
            _log.Info("User {0} is in stopped state", UserId);
            Receive<PlayMovieMessage>(x => HandleMessage(x));
            Receive<StopMovieMessage>(x => _log.Error("Cannot stop movie before one is playing", UserId));
        }


        private void HandleMessage(PlayMovieMessage message)
        {
            _log.Trace("PlayMovieMessage: '{0}' for user {1}", message.Movie, message.UserId);
            _watchingMovieName = message.Movie;

            Context.ActorSelection("/user/Playback/PlaybackStatistics/MoviePlayCounter").Tell(new IncrementPlayCountMessage(message.Movie));

            Become(Playing);
        }

        private void HandleMessage(StopMovieMessage message)
        {
            _log.Trace("StopMovieMessage: user {0}", UserId);
            _log.Trace("Stopping movie '{0}' for user {1}", _watchingMovieName, UserId);
            _watchingMovieName = null;

            Become(Stopped);
        }

    }
}
