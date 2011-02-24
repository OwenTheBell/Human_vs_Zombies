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

        public override void update(float dTime, Vector2 position)
        {
            Vector2 path = new Vector2(m_HvZWorld.GetPlayer().GetPosition().X - position.X, m_HvZWorld.GetPlayer().GetPosition().Y - position.Y);
            this.m_Walk = new Vector2((float)(path.X / Math.Sqrt(Math.Pow(path.X, 2) + Math.Pow(path.Y, 2))), (float)(path.Y / Math.Pow(path.X, 2) + Math.Pow(path.Y, 2)));
            this.m_Shoot = this.m_Walk;
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
