using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaConsoleSpike.Messages
{
    public class PlayMovieMessage
    {

        public PlayMovieMessage(string movieName, int userId)
        {
            MovieName = movieName;
            UserId = userId;
        }

        public string MovieName { get; private set; }
        public int UserId { get; private set; }
    }
}
