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

        private float m_Time;

        public HvZScreen()
        {
            GameWorld.audio.SongPlay("theme");
            this.zombieWorld = new HvZWorld();
        }

        public override void Update(float dTime)
        {
            m_Time += dTime;


            if (m_Time > 1f / 60f)
            {
                zombieWorld.Update(m_Time);
                m_Time = 0;
            }
            base.Update( dTime);
        }

        public override void Draw()
        {
            base.Draw();

            this.zombieWorld.Draw();
        }
    }
}
