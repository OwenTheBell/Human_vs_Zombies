using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Human_vs_Zombies.HvZClasses.Mobs;
using Human_vs_Zombies.HvZClasses;
using Human_vs_Zombies.Controls;
using Microsoft.Xna.Framework;

namespace Human_vs_Zombies.Controls
{

    public class SimpleAIBrains : Brains
    {
        public Player player { get; set; }

        public HvZWorld hvzWorld { get; set; }

        public Zombie zombie { get; set; }

        public Vector2 shoot;

        public Vector2 walk;

        public SimpleAIBrains(HvZWorld hvzWorld, Player player, Zombie zombie)
        {
            this.player = player;
            this.hvzWorld = hvzWorld;
            this.zombie = zombie;
        }

        public void Update()
        {
            Vector2 path = new Vector2(player.position.X - zombie.position.X, player.position.Y - zombie.position.Y);
            this.walk = new Vector2((float)(path.X / Math.Sqrt(Math.Pow(path.X, 2) + Math.Pow(path.Y, 2))), (float)(path.Y / Math.Pow(path.X, 2) + Math.Pow(path.Y, 2)));
            this.shoot = new Vector2(0, 0);
        }

        public Vector2 GetShoot()
        {
            return shoot;
        }

        public Vector2 GetWalk()
        {
            return walk;
        }


    }
}
