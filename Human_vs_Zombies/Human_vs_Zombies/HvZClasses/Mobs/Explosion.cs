using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Human_vs_Zombies.Rendering;
using Microsoft.Xna.Framework;
using Human_vs_Zombies.GameElements;

namespace Human_vs_Zombies.HvZClasses.Mobs
{
    class Explosion : Mob
    {
        private float m_BlastSpeed;
        private float m_MaxRadius;
        public Explosion(HvZWorld hvzWorld, Vector2 position, Vector2 rotation, float radius, Vector2 velocity, float maxVelocity, float blastSpeed, float maxRadius)
            : base(hvzWorld, position, rotation, radius, velocity, maxVelocity)
        {
            m_BlastSpeed = blastSpeed;
            m_MaxRadius = maxRadius;
        }

        public override void Update(float dTime)
        {
            float newRadius = this.GetRadius() + m_BlastSpeed * dTime;
            if (newRadius >= m_MaxRadius)
            {
                newRadius = m_MaxRadius;
                this.SetDead(true);
            }
            this.SetRadius(newRadius);

            base.Update(dTime);
        }

        public override void Draw()
        {
            base.DrawCircular(TextureStatic.Get("Explosion"), Settings.explosionLayer);
        }
    }
}
