using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Human_vs_Zombies.Rendering;
using Microsoft.Xna.Framework.Graphics;
using Human_vs_Zombies.HvZClasses.Items;
using Human_vs_Zombies.HvZClasses.Mobs;

namespace Human_vs_Zombies.HvZClasses.Items
{
    public class AmmoItem : Item 
    {
        private int m_Ammo;

        public AmmoItem(HvZWorld hvzWorld, Vector2 position, Vector2 rotation, float radius, float lifespan, int ammo)
            : base(hvzWorld, position, rotation, radius, lifespan)
        {
            m_Ammo = ammo;
            this.m_Texture = TextureStatic.Get("Ammo");
        }

        public override void OnPickup( Player player)
        {
            this.SetDead(true);
            player.AddAmmo(m_Ammo);
        }
    }
}
