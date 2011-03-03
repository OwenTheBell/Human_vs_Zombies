using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Human_vs_Zombies.Rendering;
using Microsoft.Xna.Framework;
using Human_vs_Zombies.HvZClasses.Mobs;

namespace Human_vs_Zombies.HvZClasses.Items
{
    class RocketItem : Item
    {
        private int m_Rockets;

        public RocketItem(HvZWorld hvzWorld, Vector2 position, Vector2 rotation, float radius, float lifespan, int rockets)
            : base(hvzWorld, position, rotation, radius, lifespan)
        {
            m_Rockets = rockets;
            this.m_Texture = TextureStatic.Get("RocketLauncher");
        }

        public override void OnPickup( Player player)
        {
            this.SetDead(true);
            player.AddRockets(m_Rockets);
        }
    }
}
