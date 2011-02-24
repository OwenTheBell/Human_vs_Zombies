using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Human_vs_Zombies.Controls;

namespace Human_vs_Zombies.HvZClasses.Mobs
{
    public class Player : Mob
    {
        private Vector2 m_Target;


        private Brains m_Brains;

        public Player(HvZWorld hvzWorld, Vector2 position, float rotation, float radius, Vector2 velocity, float maxVelocity, Brains brains)
            : base(hvzWorld, position, rotation, radius, velocity, maxVelocity)
        {
            this.SetBrains(new HumanBrains(hvzWorld, this));
            
        }

        public Brains GetBrains()
        {
            return m_Brains;
        }

        public void SetBrains(Brains brains)
        {
            m_Brains = brains;
        }

        public Vector2 GetTarget()
        {
            return m_Target;
        }

        public void SetTarget(Vector2 target)
        {
            this.m_Target = target;
        }

        public override void Update(float dTime)
        {
            //no collision handling at this time

            this.SetVelocity(m_Brains.GetWalk());
            this.SetRotation((float)Math.Atan2(m_Brains.getShoot().Y, m_Brains.getShoot().X));

            base.Update(dTime);
        }

        public override void Draw() {}
    }
}
