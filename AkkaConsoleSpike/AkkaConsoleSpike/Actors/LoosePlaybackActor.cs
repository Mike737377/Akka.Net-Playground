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
    public class LoosePlaybackActor : UntypedActor
    {

        private Logger _log = LogManager.GetCurrentClassLogger();

        public LoosePlaybackActor()
        {
            _log.Trace("{0} created", typeof(LoosePlaybackActor).Name);
        }

        protected override void OnReceive(object message)
        {
            if (message is PlayMovieMessage)
            {
                var m = message as PlayMovieMessage;
                _log.Trace("Play: '{0}' for user {1}", m.MovieName, m.UserId);
            }
            else if (message is string)
            {
                _log.Trace("Received: {0}",message);
            }
            else if (message is int)
            {
                _log.Trace("Received: UserID {0}", message);
            }
            else
            {
                Unhandled(message);
            }
        }
    }
}
