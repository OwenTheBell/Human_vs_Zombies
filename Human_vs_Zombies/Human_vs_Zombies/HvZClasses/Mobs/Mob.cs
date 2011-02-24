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

        public Mob(HvZWorld hvzWorld, Vector2 position, float rotation, float radius, Vector2 velocity)
            : base(hvzWorld, position, rotation, radius)
        {
            this.SetVelocity(velocity);
        }

        public Vector2 GetVelocity()
        {
            return m_Velocity;
        }

        public void SetVelocity(Vector2 velocity)
        {
            this.m_Velocity = velocity;
        }

        public override void Update(float dTime)
        {
            this.SetPosition(this.GetPosition() + this.GetVelocity() * dTime);
        }
    }
}
