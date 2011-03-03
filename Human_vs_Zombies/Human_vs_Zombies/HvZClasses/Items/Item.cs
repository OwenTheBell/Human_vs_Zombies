using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Human_vs_Zombies.Rendering;
using Microsoft.Xna.Framework.Graphics;
using Human_vs_Zombies.HvZClasses.Mobs;

namespace Human_vs_Zombies.HvZClasses.Items
{
    public class Item:Entity
    {
        float lifespan;

        public Item(HvZWorld hvzWorld, Vector2 position, Vector2 rotation, float radius)
            : base(hvzWorld, position, rotation, radius)
        {
            lifespan = 300f;


        }
        public override void Update(float dTime)
        {
            lifespan -= dTime;
            if (lifespan < 0)
            {
                this.SetDead(true);
            }
        }

        public override void Draw()
        {
            base.DrawCircular(TextureStatic.Get("Ammo"), 0.5f);
        }
        public static Item NewRandomItem(HvZWorld hvzWorld, Vector2 position, Vector2 rotation, float radius)
        {
            // Eventually, put logic in here to determine which random item to spawn.
            return new ItemReplenishAmmo(hvzWorld, position, rotation, radius);
        }
    }
}
