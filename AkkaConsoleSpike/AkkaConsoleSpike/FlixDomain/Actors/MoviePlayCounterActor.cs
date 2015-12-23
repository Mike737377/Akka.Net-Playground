using Akka.Actor;
using AkkaConsoleSpike.FlixDomain.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaConsoleSpike.FlixDomain.Actors
{
    public class MoviePlayCounterActor : LoggedReceiveActor
    {
        private readonly Dictionary<string, int> _moviePlayCounts = new Dictionary<string, int>();

        public MoviePlayCounterActor()
        {
            Receive<IncrementPlayCountMessage>(x => HandleMessage(x));
        }

        private void HandleMessage(IncrementPlayCountMessage message)
        {

            if (!_moviePlayCounts.ContainsKey(message.Movie))
            {
                _moviePlayCounts.Add(message.Movie, 0);
            }

            _moviePlayCounts[message.Movie] += 1;

            _log.Info("{0} has been watched {1} time(s)", message.Movie, _moviePlayCounts[message.Movie]);
        }
    }
}
