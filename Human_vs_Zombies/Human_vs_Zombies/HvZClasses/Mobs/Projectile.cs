using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Human_vs_Zombies.Rendering;
using Microsoft.Xna.Framework.Graphics;
using Human_vs_Zombies.HvZClasses.Items;
using Human_vs_Zombies.HvZClasses.Walls;
using Human_vs_Zombies.GameElements;

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

            List<Entity> cols = GetHvZWorld().Collisions(this);

            foreach (Entity c in cols)
            {
                if (c is Zombie || c is Wall)
                {
                    this.SetDead(true);
                }
            }

            m_Life -= dTime;
            base.Update(dTime);
        }

        public override void Draw()
        {
            base.DrawCircular(TextureStatic.Get("Dart"), Settings.dartLayer);
        }
    }
}
