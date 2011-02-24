using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Human_vs_Zombies.HvZClasses.Mobs;
using Human_vs_Zombies.HvZClasses;
using Human_vs_Zombies.Controls;
using Microsoft.Xna.Framework;

namespace Human_vs_Zombies.Controls
{

    public class SimpleAIBrains : Brains
    {
        private HvZWorld m_HvZWorld;

        private Vector2 m_Shoot;

        private Vector2 m_Walk;

        public SimpleAIBrains(HvZWorld hvzWorld)
        {
            this.m_HvZWorld = hvzWorld;
            this.m_Shoot = new Vector2();
            this.m_Walk = new Vector2();
        }

        public override void update(float dTime)
        {
            //Vector2 path = new Vector2(m_Player.GetPosition().X - m_Zombie.GetPosition().X, m_Player.GetPosition().Y - m_Zombie.GetPosition().Y);
            //this.m_Walk = new Vector2((float)(path.X / Math.Sqrt(Math.Pow(path.X, 2) + Math.Pow(path.Y, 2))), (float)(path.Y / Math.Pow(path.X, 2) + Math.Pow(path.Y, 2)));
            //this.m_Shoot = new Vector2(0, 0);
        }

        public override Vector2 GetWalk()
        {
            return m_Walk;
        }

        public override Vector2 getShoot()
        {
            return m_Shoot;
        }
    }
}
