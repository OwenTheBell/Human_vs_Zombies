using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Human_vs_Zombies.HvZClasses.Mobs
{
    public class Projectile : Mob
    {
        public Projectile(HvZWorld hvzWorld, Vector2 position, float rotation, float radius, Vector2 velocity)
            : base(hvzWorld, position, rotation, radius, velocity)
        {
        }
        public override void Draw() { }
    }
}
