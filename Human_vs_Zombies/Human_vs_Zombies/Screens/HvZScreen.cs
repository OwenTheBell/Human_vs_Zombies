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

        //private HvZWorld zombieWorld;

        public HvZScreen()
        {
        }

        public override void Draw()
        {
            Drawer.Draw(
                TextureStatic.Get("background"),
                Drawer.FullScreenRectangle,
                null,
                Color.White,
                0f,
                Vector2.Zero,
                SpriteEffects.None,
                0f
                );

            base.Draw();
        }
    }
}
