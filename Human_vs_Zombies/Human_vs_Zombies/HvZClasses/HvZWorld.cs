using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Human_vs_Zombies.HvZClasses.Mobs;
using Microsoft.Xna.Framework;

namespace Human_vs_Zombies.HvZClasses
{
    public class HvZWorld
    {
        private Player m_Player;

        public HvZWorld()
        {
            m_Player = new Player(this, Vector2.Zero, 0f, 0f, Vector2.Zero);
        }

        public Player getPlayer()
        {
            return m_Player;
        }

        public void Draw()
        {
            this.m_Player.Draw();
        }
    }
}
