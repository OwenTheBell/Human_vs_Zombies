using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Human_vs_Zombies.HvZClasses;
using Human_vs_Zombies.GameElements;
using Human_vs_Zombies.Controls;
using Human_vs_Zombies.HvZClasses.Mobs.Player;
using Human_vs_Zombies.HvZClasses.Mobs;
using Microsoft.Xna.Framework;


namespace Human_vs_Zombies.Controls
{

    class HumanBrains:Brains
    {

        public Player player {get; set;}

        public HvZWorld hvzWorld { get; set; }

        public Vector2 shoot;

        public Vector2 walk;
        
        public HumanBrains(HvZWorld hvzWorld, Player player)
        {
            this.hvzWorld = hvzWorld;
            this.player = player;
        }

        public void Update()
        {
            walk = (new Vector2(this.player.GetPosition().X - 10 * GameWorld.controller.ContainsFloat(ActionType.MoveHorizontal), 
                this.player.GetPosition().Y - 10 * GameWorld.controller.ContainsFloat(ActionType.MoveVertical)));
            if ((GameWorld.controller.ContainsFloat(ActionType.LookVertical) + GameWorld.controller.ContainsFloat(ActionType.LookHorizontal)) > .5)
            {
                shoot = new Vector2(GameWorld.controller.ContainsFloat(ActionType.LookVertical), GameWorld.controller.ContainsFloat(ActionType.LookHorizontal));
            }

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
