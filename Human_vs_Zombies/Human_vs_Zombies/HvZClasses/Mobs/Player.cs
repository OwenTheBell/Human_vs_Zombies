using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Human_vs_Zombies.Controls;

namespace Human_vs_Zombies.HvZClasses.Mobs
{
    public class Player:Mob
    {
        public const float radius = 100;

        Vector2 target;

        public Vector2 position;

        public Vector2 velocity;

        public HvZWorld hvzWorld;

        private Brains brains;

        public Player(HvZWorld hvzWorld, Vector2 initialPosition)
        {
            this.brains = new HumanBrains(hvzWorld, this);
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
            this.position = this.target;
        }
    }
}
