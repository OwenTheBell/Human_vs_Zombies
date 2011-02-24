using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Human_vs_Zombies.Rendering;
using Microsoft.Xna.Framework.Graphics;

namespace Human_vs_Zombies.HvZClasses.Mobs
{
    public class Projectile : Mob
    {
        public const float LIFE = .5f;

        private float m_Life;

        public Projectile(HvZWorld hvzWorld, Vector2 position, Vector2 rotation, float radius, Vector2 velocity)
            : base(hvzWorld, position, rotation, radius, velocity, velocity.Length())
        {
            m_Life = LIFE;
        }

        public override void Update(float dTime)
        {
            if (m_Life <= 0)
            {
                this.SetDead(true);
            }

            List<Entity> cols = GetWorld().Collisions(this);

            foreach (Entity c in cols)
            {
                if (c is Zombie)
                {
                    this.SetDead(true);
                }
            }

            m_Life -= dTime;
            base.Update(dTime);
        }

        public override void Draw()
        {
            Drawer.Draw(
                TextureStatic.Get("Dart"),
                this.GetPosition(),
                null,
                Color.White,
                (float)Math.Atan2(this.GetRotation().Y, this.GetRotation().X),
                new Vector2(9f),
                1f,
                SpriteEffects.None,
                0.9f);
        }
    }
}
