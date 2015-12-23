using Akka.Actor;
using AkkaConsoleSpike.Actors;
using AkkaConsoleSpike.FlixDomain;
using AkkaConsoleSpike.Messages;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaConsoleSpike
{
    public class Program
    {

        public static void Main(string [] args)
        {

            //StatefulDemo.Run();

            new Example().Run();

        }

    }
}
