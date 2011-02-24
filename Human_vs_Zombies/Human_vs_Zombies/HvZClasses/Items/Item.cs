using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Human_vs_Zombies.HvZClasses.Items
{
    public class Item:Entity
    {
        public Item(HvZWorld hvzWorld, Vector2 position, float rotation, float radius)
            : base(hvzWorld, position, rotation, radius)
        {
        }
        public override void Update(float dTime) { }
        public override void Draw() { }
    }
}
