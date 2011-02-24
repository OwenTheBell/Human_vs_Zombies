using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Human_vs_Zombies.HvZClasses.Mobs
{
    public class Projectile : Mob
    {
        public const float LIFE = 0.5f;

        private float m_Life;

        public Projectile(HvZWorld hvzWorld, Vector2 position, Vector2 rotation, float radius, Vector2 velocity)
            : base(hvzWorld, position, rotation, radius, velocity, 10f)
        {
            m_Life = LIFE;
        }

        public override void Update(float dTime)
        {
            if (m_Life < 0 || this.GetWorld().Collisions(this).Count > 0)
            {
                this.SetDead(true);
            }

            m_Life -= dTime;
            base.Update(dTime);
        }

        public override void Draw() { }
    }
}
