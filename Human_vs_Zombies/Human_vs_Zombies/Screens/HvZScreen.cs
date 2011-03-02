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
        private int m_LastUpdate;
        private HvZWorld zombieWorld;

        public HvZScreen()
        {
            GameWorld.audio.SongPlay("theme");
            this.zombieWorld = new HvZWorld();
            m_LastUpdate = System.Environment.TickCount;
        }

        public override void Update()
        {
            int time = System.Environment.TickCount;
            float dTime = (time - m_LastUpdate) / 1000f;

            if (dTime > 1f / 60f)
            {
                zombieWorld.Update(dTime);
                m_LastUpdate = time;
            }
            

            base.Update();
        }

        public override void Draw()
        {
            base.Draw();

            this.zombieWorld.Draw();
        }
    }
}
