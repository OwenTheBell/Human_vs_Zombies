using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Human_vs_Zombies.HvZClasses.Mobs;

namespace Human_vs_Zombies.HvZClasses
{
    public class HvZWorld
    {
        private Player m_Player;

        public HvZWorld(Player player)
        {
            m_Player = player;
        }

        public Player getPlayer()
        {
            return m_Player;
        }
    }
}
