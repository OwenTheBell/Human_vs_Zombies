using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Human_vs_Zombies.HvZClasses.Mobs;
using Microsoft.Xna.Framework;
using System.Collections;

namespace Human_vs_Zombies.HvZClasses
{
    public class HvZWorld
    {
        private Player m_Player;
        private ArrayList m_Entities;

        public HvZWorld()
        {
            m_Player = new Player(this, Vector2.Zero, 0f, 0f, Vector2.Zero);
        }

        public Player GetPlayer()
        {
            return m_Player;
        }

        public void Update(float dTime)
        {

        }

        public void Draw()
        {
            this.m_Player.Draw();
        }
    }
}
