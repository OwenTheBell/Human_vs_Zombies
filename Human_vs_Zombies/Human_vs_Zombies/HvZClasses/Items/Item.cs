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
        }

        public abstract void OnPickup(Player player);

        public override void Draw()
        {
            if (m_Life > Settings.itemWarningTime || m_Life % Settings.itemBlinkRate < Settings.itemBlinkRate / 3)
            {
                base.DrawCircular(m_Texture, Settings.itemLayer);
            }
        }
        public static Item NewRandomItem(HvZWorld hvzWorld, Vector2 position, Vector2 rotation, float radius, float lifespan)
        {
            double item = (new Random()).Next(100);
            // Spawn more ammo 90% of the time, a Nuke 1%, and a RocketLauncher 4%
            if (item < 75)
                return new AmmoItem(hvzWorld, position, rotation, radius, lifespan, Settings.itemAmmo);
            if (item < 90)
                return new RocketItem(hvzWorld, position, rotation, radius, lifespan, Settings.rocketAmmo);
            if (item < 95)
                return new ExplosionItem(hvzWorld, position, rotation, radius, lifespan);
                return new SpeedItem(hvzWorld, position, rotation, radius, lifespan);
        }
    }
}
