using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Human_vs_Zombies.Controls;

namespace Human_vs_Zombies.HvZClasses.Mobs
{


    public class Zombie:Mob
    {

        public const float radius = 100;

        public Vector2 target;

        public Vector2 position;

        public Vector2 velocity;

        public Vector2 dir;

        public HvZWorld hvzWorld;

        public Player player;

        private Brains brains;

        public Zombie(HvZWorld hvzWorld, Vector2 initialPosition, Player player)
        {
            this.brains = new SimpleAIBrains(hvzWorld, player, this);
            this.hvzWorld = hvzWorld;
            this.position = initialPosition;
            this.velocity.X = 0;
            this.velocity.Y = 0;
            this.player = player;
            this.target = player.GetPosition();
        }

        public void Update()
        {
            this.position = this.target;
        }

        public void GoTo(Vector2 target)
        {
            this.target = target;
        }
    }
}
