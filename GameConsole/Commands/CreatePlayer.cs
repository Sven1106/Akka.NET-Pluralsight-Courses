using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameConsole.Commands
{
    internal class CreatePlayer
    {
        public string PlayerName { get; private set; }
        public CreatePlayer(string playerName)
        {
            PlayerName = playerName;
        }
    }
}
