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
    public class ExplosionItem : Item
    {
        private HvZWorld world;

        public ExplosionItem(HvZWorld hvzWorld, Vector2 position, Vector2 rotation, float radius, float lifespan)
            : base(hvzWorld, position, rotation, radius, lifespan)
        {
            this.m_Texture = TextureStatic.Get("Nuke");
            world = hvzWorld;
        }

        public override void OnPickup(Player player)
        {
            this.SetDead(true);
            world.KillAllZombies();
           
        }
    }
}
