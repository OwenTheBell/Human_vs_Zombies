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
    public class ItemReplenishAmmo : Item 
    {
        private int bulletsRefilled;

        public ItemReplenishAmmo(HvZWorld hvzWorld, Vector2 position, Vector2 rotation, float radius)
            : base(hvzWorld, position, rotation, radius)
        {
            bulletsRefilled = new Random().Next(100,200);
            this.SetRotation(Vector2.UnitX);
        }
        public override void Draw()
        {
            base.DrawCircular(TextureStatic.Get("Ammo"), 0.5f);
        }
        public override void Update(float dTime)
        {            
            List<Entity> cols = GetHvZWorld().Collisions(this);
            foreach (Entity butWho in cols)
            {
                if (butWho is Player) // ?????
                {
                    this.SetDead(true);
                    this.OnPickup();
                }
            }
        }
        public void OnPickup()
        {
            
        }
    }
}
