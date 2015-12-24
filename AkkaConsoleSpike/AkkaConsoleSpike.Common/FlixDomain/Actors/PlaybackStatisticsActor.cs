using Akka.Actor;
using AkkaConsoleSpike.FlixDomain.Exceptions;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaConsoleSpike.FlixDomain.Actors
{
    public class PlaybackStatisticsActor : LoggedReceiveActor
    {
        public PlaybackStatisticsActor()
        {
            Context.ActorOf(Props.Create<MoviePlayCounterActor>(), "MoviePlayCounter");
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(ex =>
            {
                if (ex is MovieLicenseExpiredException)
                {
                    return Directive.Resume;
                }
                
                if (ex is MovieStoreCorruptedException)
                {
                    return Directive.Restart;
                }

                return Directive.Restart;
            });
        }
    }
}
