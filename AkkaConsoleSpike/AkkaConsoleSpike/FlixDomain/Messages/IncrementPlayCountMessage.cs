using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaConsoleSpike.FlixDomain.Messages
{
    public class IncrementPlayCountMessage
    {
        public string Movie { get; private set; }

        public IncrementPlayCountMessage(string movie)
        {
            Movie = movie;
        }
    }
}
