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
    public class UserCoordinatorActor : LoggedReceiveActor
    {
        private readonly Dictionary<int, IActorRef> _users = new Dictionary<int, IActorRef>();

        public UserCoordinatorActor()
        {
            Receive<PlayMovieMessage>(message =>
            {
                CreateChildUserIfNotExists(message.UserId);
                IActorRef childActorRef = _users[message.UserId];
                childActorRef.Tell(message);
            });

            Receive<StopMovieMessage>(message =>
            {
                CreateChildUserIfNotExists(message.UserId);
                IActorRef childActorRef = _users[message.UserId];
                childActorRef.Tell(message);
            });
        }

        private void CreateChildUserIfNotExists(int userId)
        {
            if (!_users.ContainsKey(userId))
            {
                var actorRef = Context.ActorOf(Props.Create(() => new UserActor(userId)), "User" + userId);
                _users.Add(userId, actorRef);
                _log.Debug("{0} has created new User {1} (Total users: {2})", this.GetType().Name, userId, _users.Count);
            }
        }
    }
}
