using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Human_vs_Zombies.Rendering;
using Microsoft.Xna.Framework.Graphics;
using Human_vs_Zombies.GameElements;

namespace Human_vs_Zombies.HvZClasses.Walls
{
    public class Wall : Entity
    {
        private float m_Thickness;

        //tracks whether or not this wall casts a shadow
        private bool m_CastsShadow;

        public Wall(HvZWorld hvzWorld, Vector2 position, Vector2 rotation, float radius, float thickness, bool castsShadow)
            : base(hvzWorld, position, rotation, radius)
        {
            this.SetThickness(thickness);
            this.SetShadow(castsShadow);
        }

        public float GetThickness()
        {
            return m_Thickness;
        }

        public void SetThickness(float thickness)
        {
            m_Thickness = thickness;
        }

        public bool CastShadow()
        {
            return this.m_CastsShadow;
        }

        public void SetShadow(bool castsShadow)
        {
            this.m_CastsShadow = castsShadow;
        }

        public Vector2[] GetPoints()
        {
            Vector2 len = this.GetRotation() * this.GetRadius();
            Vector2 wid = new Vector2(this.GetRotation().Y, -this.GetRotation().X) * this.GetThickness();
            Vector2[] ret = { this.GetPosition() - len / 2 - wid / 2, this.GetPosition() - len / 2 + wid / 2, this.GetPosition() + len / 2 + wid / 2, this.GetPosition() + len / 2 - wid / 2 };

            return ret;
        }

        public override bool Collides(Entity other)
        {
            Vector2[] pts = this.GetPoints();

            Vector2 a = pts[0];
            Vector2 b = pts[1];
            Vector2 c = pts[2];
            Vector2 d = pts[3];

            Vector2 p = other.GetPosition();
            float r = other.GetRadius();

            //first check if any of the corners are inside the entity
            if ((p - a).LengthSquared() <= r * r || (p - b).LengthSquared() <= r * r || (p - c).LengthSquared() <= r * r || (p - d).LengthSquared() <= r * r)
            {
                return true;
            }

            //then check if the entity is simply inside this
            if (Vector2.Dot(p - a, b - a) >= 0 && Vector2.Dot(p - a, b - a) <= (b - a).LengthSquared() && Vector2.Dot(p - a, d - a) >= 0 && Vector2.Dot(p - a, d - a) <= (d - a).LengthSquared())
            {
                return true;
            }

            //then check line segment collisions
            if (Vector2.Dot(p - a, b - a) >= 0 && Vector2.Dot(p - a, b - a) <= (b - a).LengthSquared() && (p - a - ((Vector2.Dot(p - a, b - a) / (b - a).LengthSquared()) * (b - a))).LengthSquared() <= r * r)
            {
                return true;
            }
            if (Vector2.Dot(p - b, c - b) >= 0 && Vector2.Dot(p - b, c - b) <= (c - b).LengthSquared() && (p - b - ((Vector2.Dot(p - b, c - b) / (c - b).LengthSquared()) * (c - b))).LengthSquared() <= r * r)
            {
                return true;
            }
            if (Vector2.Dot(p - c, d - c) >= 0 && Vector2.Dot(p - c, d - c) <= (d - c).LengthSquared() && (p - c - ((Vector2.Dot(p - c, d - c) / (d - c).LengthSquared()) * (d - c))).LengthSquared() <= r * r)
            {
                return true;
            }
            if (Vector2.Dot(p - d, a - d) >= 0 && Vector2.Dot(p - d, a - d) <= (a - d).LengthSquared() && (p - d - ((Vector2.Dot(p - d, a - d) / (a - d).LengthSquared()) * (a - d))).LengthSquared() <= r * r)
            {
                return true;
            }

            return false;
        }

        public override void Update(float dTime) { }
        public override void Draw()
        {
                        Texture2D texture = TextureStatic.Get("Wall");
            Drawer.Draw(
                texture,
                new Rectangle((int)(this.GetPoints()[0].X), (int)(this.GetPoints()[0].Y), (int)(this.GetRadius()), (int)(this.GetThickness())),
                null,
                Color.White,
                (float)Math.Atan2(this.GetRotation().Y, this.GetRotation().X),
                new Vector2(0, texture.Height),
                SpriteEffects.None,
                Settings.wallLayer);
        }
    }
}
