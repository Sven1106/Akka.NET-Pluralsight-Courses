using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameConsole
{
    /// <summary>
    /// Static helper class used to define paths to fixed-name actors
    /// (helps eliminate errors when using <see cref="ActorSelection"/>)
    /// </summary>
    public static class ActorPaths
    {
        public static ActorMetaData PlayerCoordinatorActor = new ActorMetaData("PlayerCoordinator");
        public static ActorMetaData PlayerActor = new ActorMetaData("Player", PlayerCoordinatorActor);
    }
}
