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
        
        public HumanBrains(HvZWorld hvzWorld, Player player)
        {
            this.hvzWorld = hvzWorld;
            this.player = player;
        }

        public void Update()
        {
            this.player.GoTo(new Vector2(this.player.GetPosition().X - 10 * GameWorld.controller.ContainsFloat(ActionType.MoveHorizontal), 
                this.player.GetPosition().Y - 10 * GameWorld.controller.ContainsFloat(ActionType.MoveVertical)));
        }

    }
}
