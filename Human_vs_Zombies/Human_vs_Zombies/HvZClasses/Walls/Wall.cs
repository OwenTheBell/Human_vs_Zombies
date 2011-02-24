using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Human_vs_Zombies.HvZClasses.Walls
{
    public class Wall:Entity
    {
        public Wall(HvZWorld hvzWorld, Vector2 position, Vector2 rotation, float radius)
            : base(hvzWorld, position, rotation, radius)
        {
        }
        public override void Update(float dTime) { }
        public override void Draw() { }
    }
}
