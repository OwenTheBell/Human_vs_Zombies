using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Human_vs_Zombies.HvZClasses.Mobs
{
    class Player:Mob
    {
        public const float radius = 100;

        Vector2 target;

        public Vector2 position;

        public Vector2 velocity;

        public HvZWorld hvzWorld;

        public Player(HvZWorld hvzWorld, Vector2 initialPosition)
        {
            this.target = initialPosition;
            this.hvzWorld = hvzWorld;
            this.position = initialPosition;
            this.velocity.X = 0;
            this.velocity.Y = 0;
        }


        public Vector2 GetPosition()
        {
            return position;
        }

        public void GoTo(Vector2 target)
        {
            this.target = target;
        }


        public void Update()
        {
        }
    }
}
