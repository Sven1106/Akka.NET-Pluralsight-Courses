using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameConsole.Commands
{
    internal class HitPlayer
    {
        public int Damage { get; private set; }
        public HitPlayer(int damage)
        {
            Damage = damage;
        }
    }
}
