using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Human_vs_Zombies.GameElements;
using Human_vs_Zombies.Rendering;
using Human_vs_Zombies.HvZClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Human_vs_Zombies.Screens
{
    public class HvZScreen:GameScreen
    {

        private HvZWorld zombieWorld;

        public HvZScreen()
        {
            this.zombieWorld = new HvZWorld();
        }

        public override void Draw()
        {
            base.Draw();

            this.zombieWorld.Draw();
        }
    }
}
