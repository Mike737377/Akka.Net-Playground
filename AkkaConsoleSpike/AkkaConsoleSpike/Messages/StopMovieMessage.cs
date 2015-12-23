using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaConsoleSpike.Messages
{
    public class StopMovieMessage
    {
        public StopMovieMessage(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; private set; }
    }
}
