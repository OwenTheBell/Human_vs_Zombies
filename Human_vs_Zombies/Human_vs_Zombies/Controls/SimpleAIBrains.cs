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

        public override void Update(float dTime, Vector2 position)
        {
            Vector2 path = m_HvZWorld.GetPlayer().GetPosition() - position;
            path.Normalize();
            this.m_Walk = path;
            this.m_Shoot = this.m_Walk;
        }

        public override Vector2 GetWalk()
        {
            return m_Walk;
        }

        public override Vector2 GetShoot()
        {
            return m_Shoot;
        }
    }
}
