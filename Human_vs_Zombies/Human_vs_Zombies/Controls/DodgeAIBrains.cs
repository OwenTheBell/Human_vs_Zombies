using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Human_vs_Zombies.HvZClasses;
using Human_vs_Zombies.HvZClasses.Mobs;

namespace Human_vs_Zombies.Controls
{
    class DodgeAIBrains : ClusterAIBrains
    {
        private Vector2 m_Shoot;
        private Vector2 m_Walk;
        private HvZWorld m_HvZWorld;

        public DodgeAIBrains(HvZWorld hvzWorld)
            : base(hvzWorld)
        {
            m_HvZWorld = hvzWorld;
        }

        public override void Update(float dTime, Vector2 position)
        {
            base.Update(dTime, position);
            Player player = m_HvZWorld.GetPlayer();
            Vector2 toPlayer = player.GetPosition() - position;

            float angle = HvZWorld.SignedAngle(player.GetRotation(), -toPlayer);
            float rotation = 0;

            if (angle < 0)
            {
                rotation = ((float)Math.PI + angle) / 2;
            }
            else
            {
                rotation = (-(float)Math.PI + angle) / 2;
            }

            float angleToPlayer = (float)Math.Atan2(toPlayer.Y, toPlayer.X);

            this.m_Walk = base.GetWalk() + 2 * new Vector2((float)Math.Cos(angleToPlayer + rotation), (float)Math.Sin(angleToPlayer + rotation));
            this.m_Walk.Normalize();
            this.m_Shoot = base.GetShoot();
        }

        public override Vector2 GetShoot()
        {
            return this.m_Shoot;
        }

        public override Vector2 GetWalk()
        {
            return this.m_Walk;
        }
    }
}
