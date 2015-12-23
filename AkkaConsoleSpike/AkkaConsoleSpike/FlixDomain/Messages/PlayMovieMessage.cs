using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaConsoleSpike.FlixDomain.Messages
{
    public class PlayMovieMessage
    {
        public PlayMovieMessage(int userId, string movie)
        {
            UserId = userId;
            Movie = movie;
        }

        public int UserId { get; private set; }
        public string Movie { get; private set; }

    }
}
