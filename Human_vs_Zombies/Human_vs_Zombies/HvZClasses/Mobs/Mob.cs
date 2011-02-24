using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Human_vs_Zombies.HvZClasses.Mobs
{
    public abstract class Mob : Entity
    {
        private Vector2 m_Velocity;

        private float m_maxVelocity;

        public Mob(HvZWorld hvzWorld, Vector2 position, Vector2 rotation, float radius, Vector2 velocity, float maxVelocity)
            : base(hvzWorld, position, rotation, radius)
        {
            this.SetMax(maxVelocity);
            this.SetVelocity(velocity);
        }

        public Vector2 GetVelocity()
        {
            return m_Velocity;
        }

        public void SetVelocity(Vector2 velocity)
        {
            if (velocity.LengthSquared() <= (m_maxVelocity * m_maxVelocity))
            {
                this.m_Velocity = velocity;
            }
            else
            {
                Vector2 unitVelocity = velocity;
                unitVelocity.Normalize();
                this.m_Velocity = m_maxVelocity * unitVelocity;
            }
        }

        public void SetMax(float maxVelocity)
        {
            this.m_maxVelocity = maxVelocity;
        }

        public float GetMaxVel()
        {
            return this.m_maxVelocity;
        }

        public override void Update(float dTime)
        {
            this.SetPosition(this.GetPosition() + this.GetVelocity() * dTime);
        }
    }
}
