using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Human_vs_Zombies.Rendering;
using Microsoft.Xna.Framework.Graphics;
using Human_vs_Zombies.HvZClasses.Mobs;
using Human_vs_Zombies.GameElements;

namespace Human_vs_Zombies.HvZClasses.Items
{
    public abstract class Item:Entity
    {
        private float m_Life;
        private float m_Lifespan;
        protected Texture2D m_Texture;

        public Item(HvZWorld hvzWorld, Vector2 position, Vector2 rotation, float radius, float lifespan)
            : base(hvzWorld, position, rotation, radius)
        {
            m_Lifespan = m_Life = lifespan;
        }
        public override void Update(float dTime)
        {
            m_Life -= dTime;

            this.SetRotation(new Vector2((float)Math.Cos(m_Life), (float)Math.Sin(m_Life)));

            if (m_Life < 0)
            {
                this.SetDead(true);
            }     
            List<Entity> cols = GetHvZWorld().Collisions(this);
            foreach (Entity butWho in cols)
            {
                if (butWho is Player) // ?????
                {
                    this.SetDead(true);
                    this.OnPickup( (Player)butWho);
                }
            }
        }

        public abstract void OnPickup(Player player);

        public override void Draw()
        {
            if (m_Life > Settings.itemWarningTime || m_Life % Settings.itemBlinkRate < Settings.itemBlinkRate / 2)
            {
                base.DrawCircular(m_Texture, 0.5f);
            }
        }
        public static Item NewRandomItem(HvZWorld hvzWorld, Vector2 position, Vector2 rotation, float radius, float lifespan)
        {
            // Eventually, put logic in here to determine which random item to spawn.
            return new AmmoItem(hvzWorld, position, rotation, radius, lifespan, new Random().Next(64, 128));
        }
    }
}
