using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Human_vs_Zombies.HvZClasses.Walls;

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

        public void SetVelocityUnchecked(Vector2 velocity)
        {
            this.m_Velocity = velocity;
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
            List<Entity> cols = GetHvZWorld().Collisions(this);

            foreach (Entity col in cols)
            {
                if (col is Wall)
                {
                    Wall w = (Wall)col;

                    Vector2[] pts = w.GetPoints();

                    Vector2 a = pts[0];
                    Vector2 b = pts[1];
                    Vector2 c = pts[2];
                    Vector2 d = pts[3];

                    Vector2 p = this.GetPosition();
                    float r = this.GetRadius();
                    Vector2 v = this.GetVelocity();

                    //first check if this is simply inside the wall
                    if (Vector2.Dot(p - a, b - a) >= 0 && Vector2.Dot(p - a, b - a) <= (b - a).LengthSquared() && Vector2.Dot(p - a, d - a) >= 0 && Vector2.Dot(p - a, d - a) <= (d - a).LengthSquared())
                    {
                        //v = -v;
                    } //then check if any of the corners are inside this
                    else if ((p - a).LengthSquared() <= r * r)
                    {
                        Vector2 tangent = new Vector2(-(p - a).Y, (p - a).X);
                        Vector2 normal = p - a;
                        if (Vector2.Dot(v, normal) < 0) v = tangent * Vector2.Dot(v, tangent) / tangent.LengthSquared();
                        normal.Normalize();
                        p += (r - (p - a).Length()) * normal;
                    }
                    else if ((p - b).LengthSquared() <= r * r)
                    {
                        Vector2 tangent = new Vector2(-(p - b).Y, (p - b).X);
                        Vector2 normal = p - b;
                        if (Vector2.Dot(v, normal) < 0) v = tangent * Vector2.Dot(v, tangent) / tangent.LengthSquared();
                        normal.Normalize();
                        p += (r - (p - b).Length()) * normal;
                    }
                    else if ((p - c).LengthSquared() <= r * r)
                    {
                        Vector2 tangent = new Vector2(-(p - c).Y, (p - c).X);
                        Vector2 normal = p - c;
                        if (Vector2.Dot(v, normal) < 0) v = tangent * Vector2.Dot(v, tangent) / tangent.LengthSquared();
                        normal.Normalize();
                        p += (r - (p - c).Length()) * normal;
                    }
                    else if ((p - d).LengthSquared() <= r * r)
                    {
                        Vector2 tangent = new Vector2(-(p - d).Y, (p - d).X);
                        Vector2 normal = p - d;
                        if (Vector2.Dot(v, normal) < 0) v = tangent * Vector2.Dot(v, tangent) / tangent.LengthSquared();
                        normal.Normalize();
                        p += (r - (p - d).Length()) * normal;
                    } //then check line segment collisions
                    else if (Vector2.Dot(p - a, b - a) >= 0 && Vector2.Dot(p - a, b - a) <= (b - a).LengthSquared() && (p - a - ((Vector2.Dot(p - a, b - a) / (b - a).LengthSquared()) * (b - a))).LengthSquared() <= r * r)
                    {
                        Vector2 tangent = b - a;
                        Vector2 normal = new Vector2(tangent.Y, -tangent.X);
                        normal.Normalize();
                        if(Vector2.Dot(v,normal) < 0) v = tangent * Vector2.Dot(v, tangent) / tangent.LengthSquared();
                        p += (r - (p - a - ((Vector2.Dot(p - a, b - a) / (b - a).LengthSquared()) * (b - a))).Length()) * normal;
                    }
                    else if (Vector2.Dot(p - b, c - b) >= 0 && Vector2.Dot(p - b, c - b) <= (c - b).LengthSquared() && (p - b - ((Vector2.Dot(p - b, c - b) / (c - b).LengthSquared()) * (c - b))).LengthSquared() <= r * r)
                    {
                        Vector2 tangent = c - b;
                        Vector2 normal = new Vector2(tangent.Y, -tangent.X);
                        normal.Normalize();
                        if (Vector2.Dot(v, normal) < 0) v = tangent * Vector2.Dot(v, tangent) / tangent.LengthSquared();
                        p += (r - (p - b - ((Vector2.Dot(p - b, c - b) / (c - b).LengthSquared()) * (c - b))).Length()) * normal;
                    }
                    else if (Vector2.Dot(p - c, d - c) >= 0 && Vector2.Dot(p - c, d - c) <= (d - c).LengthSquared() && (p - c - ((Vector2.Dot(p - c, d - c) / (d - c).LengthSquared()) * (d - c))).LengthSquared() <= r * r)
                    {
                        Vector2 tangent = d - c;
                        Vector2 normal = new Vector2(tangent.Y, -tangent.X);
                        normal.Normalize();
                        if (Vector2.Dot(v, normal) < 0) v = tangent * Vector2.Dot(v, tangent) / tangent.LengthSquared();
                        p += (r - (p - c - ((Vector2.Dot(p - c, d - c) / (d - c).LengthSquared()) * (d - c))).Length()) * normal;
                    }
                    else if (Vector2.Dot(p - d, a - d) >= 0 && Vector2.Dot(p - d, a - d) <= (a - d).LengthSquared() && (p - d - ((Vector2.Dot(p - d, a - d) / (a - d).LengthSquared()) * (a - d))).LengthSquared() <= r * r)
                    {
                        Vector2 tangent = a - d;
                        Vector2 normal = new Vector2(tangent.Y, -tangent.X);
                        normal.Normalize();
                        if (Vector2.Dot(v, normal) < 0) v = tangent * Vector2.Dot(v, tangent) / tangent.LengthSquared();
                        p += (r - (p - d - ((Vector2.Dot(p - d, a - d) / (a - d).LengthSquared()) * (a - d))).Length()) * normal;
                    }

                    this.SetVelocity(v);
                    this.SetPosition(p);
                }
            }

            this.SetPosition(this.GetPosition() + this.GetVelocity() * dTime);
        }
    }
}
