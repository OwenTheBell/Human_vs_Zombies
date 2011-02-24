using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Human_vs_Zombies.HvZClasses;
using Human_vs_Zombies.HvZClasses.Mobs;
using Microsoft.Xna.Framework;

namespace Human_vs_Zombies.Controls
{
    /// <summary>
    /// The class for controlling a player or zombie.
    /// </summary>
    public abstract class Brains
    {
        /// <summary>
        /// Updates this instance.
        /// </summary>
        public abstract void update( float dTime, Vector2 position);

        public abstract Vector2 getShoot();

        public abstract Vector2 GetWalk();
    }
}
