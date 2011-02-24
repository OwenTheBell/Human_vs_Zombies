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

        public Mob(HvZWorld hvzWorld, Vector2 position, float rotation, float radius, Vector2 velocity, float maxVelocity)
            : base(hvzWorld, position, rotation, radius)
        {
            this.SetVelocity(velocity);
            this.SetMax(maxVelocity);
        }

        public Vector2 GetVelocity()
        {
            return m_Velocity;
        }

        public void SetVelocity(Vector2 velocity)
        {
            this.m_Velocity = velocity;
        }

        public void SetMax(float maxVelocity)
        {
            this.m_maxVelocity = maxVelocity;
        }

        public float GetMax()
        {
            return this.m_maxVelocity;
        }

        public override void Update(float dTime)
        {
            this.SetPosition(this.GetPosition() + this.GetVelocity() * dTime);
        }
    }
}
