using Akka.Actor;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaConsoleSpike.FlixDomain.Actors
{
    public abstract class LoggedReceiveActor : ReceiveActor
    {
        protected readonly Logger _log = LogManager.GetCurrentClassLogger();

        protected override void PreStart()
        {
            _log.Trace("{0} PreStart: {1}", this.GetType().Name, Context.Self.Path);            
        }

        protected override void PostStop()
        {
            _log.Trace("{0} PostStop: {1}", this.GetType().Name, Context.Self.Path);
        }

        protected override void PreRestart(Exception reason, object message)
        {
            _log.Error(reason);
            _log.Trace("{0} PreRestart: {1}", this.GetType().Name, Context.Self.Path);
        }
    }
}
