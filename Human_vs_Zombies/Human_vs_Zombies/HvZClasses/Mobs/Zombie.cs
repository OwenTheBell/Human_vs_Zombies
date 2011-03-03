using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Human_vs_Zombies.Controls;
using Human_vs_Zombies.Rendering;
using Microsoft.Xna.Framework.Graphics;

namespace Human_vs_Zombies.HvZClasses.Mobs
{


    public class Zombie : Mob
    {
        private Vector2 m_Target;

        private Brains m_Brains;

        public Zombie(HvZWorld hvzWorld, Vector2 position, Vector2 rotation, float radius, Vector2 velocity, float maxVelocity, Brains brains)
            : base(hvzWorld, position, rotation, radius, velocity, maxVelocity)
        {
            this.SetBrains(brains);
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
            this.m_Brains.Update(dTime, this.GetPosition());

            this.SetVelocity(m_Brains.GetWalk() * this.GetMaxVel());

            if (m_Brains.GetShoot().LengthSquared() > 0f)
            {
                this.SetRotation(m_Brains.GetShoot());
            }
            else if (this.GetVelocity().LengthSquared() > 0f)
            {
                this.SetRotation(this.GetVelocity() / this.GetVelocity().Length());
            }

            List<Entity> cols = GetHvZWorld().Collisions(this);

            foreach (Entity c in cols)
            {
                if (c is Projectile || c is Explosion)
                {
                    this.SetDead(true);
                }
                else if (c is Zombie)
                {
                    Zombie z = (Zombie)c;

                    Vector2 p = this.GetPosition();
                    Vector2 q = z.GetPosition();
                    Vector2 w = z.GetVelocity();
                    Vector2 v = this.GetVelocity() - w;
                    float r = this.GetRadius() + z.GetRadius();

                    Vector2 normal = p - q;
                    float d = normal.Length();
                    normal.Normalize();
                    Vector2 tangent = new Vector2(normal.Y, -normal.X);
                    if (Vector2.Dot(v, normal) < 0) v = tangent * Vector2.Dot(v, tangent) / tangent.LengthSquared();

                    this.SetVelocity(v + w);
                }
            }

            base.Update(dTime);
        }

        public override void Draw() 
        {
            base.DrawCircular(TextureStatic.Get("Zombie"), 0.5f);
        }
    }
}
