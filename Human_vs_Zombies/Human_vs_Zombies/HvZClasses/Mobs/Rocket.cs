using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Human_vs_Zombies.Rendering;
using Human_vs_Zombies.GameElements;
using Microsoft.Xna.Framework;

namespace Human_vs_Zombies.HvZClasses.Mobs
{
    class Rocket : Projectile
    {
        private Vector2 m_Accel;
        private float m_BlastRadius;
        private float m_BlastSpeed;

        public Rocket(HvZWorld hvzWorld, Vector2 position, Vector2 rotation, float radius, Vector2 accel, float blastRadius, float blastSpeed)
            : base(hvzWorld, position, rotation, radius, Vector2.Zero)
        {
            m_Accel = accel;
            this.SetMaxVel(.5f * accel.Length() * Settings.rocketLife * Settings.rocketLife);
            m_BlastRadius = blastRadius;
            m_BlastSpeed = blastSpeed;
            this.SetLife(Settings.rocketLife);
        }
        public override void Update(float dTime)
        {
            base.Update(dTime);

            if (this.IsDead())
            {
                this.GetHvZWorld().AddEntity(new Explosion(this.GetHvZWorld(), this.GetPosition(), Vector2.UnitX, 0f, m_BlastSpeed, m_BlastRadius));
            }
            else
            {
                this.SetVelocity(this.GetVelocity() + m_Accel * dTime);
            }
        }

        public override void Draw()
        {
            base.DrawCircular(TextureStatic.Get("Rocket"), Settings.dartLayer);
        }
    }
}
