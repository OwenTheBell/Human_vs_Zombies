using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Human_vs_Zombies.Rendering;
using Microsoft.Xna.Framework.Graphics;

namespace Human_vs_Zombies.HvZClasses.Items
{
    public class Item:Entity
    {
        public Item(HvZWorld hvzWorld, Vector2 position, Vector2 rotation, float radius)
            : base(hvzWorld, position, rotation, radius)
        {
        }
        public override void Update(float dTime) { }
        public override void Draw()
        {
            Drawer.Draw(
                TextureStatic.Get("Ammo"),
                this.GetPosition(),
                null,
                Color.White,
                (float)Math.Atan2(this.GetRotation().Y, this.GetRotation().X),
                new Vector2(0, TextureStatic.Get("Ammo").Width / 2),
                1f,
                SpriteEffects.None,
                1f);
        }
    }
}
