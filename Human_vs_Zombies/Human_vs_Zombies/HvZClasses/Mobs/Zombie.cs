using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Human_vs_Zombies.HvZClasses.Mobs
{


    class Zombie:Mob
    {

        public const float radius = 100;

        public Vector2 position;

        public Vector2 velocity;

        public Vector2 dir;

        public HvZWorld hvzWorld;

        public Player player;

        public Zombie(HvZWorld hvzWorld, Vector2 initialPosition, Player player)
        {
            this.hvzWorld = hvzWorld;
            this.position = initialPosition;
            this.velocity.X = 0;
            this.velocity.Y = 0;
            this.player = player;
        }

        public void Update()
        {

        }

    }
}
