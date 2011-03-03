using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Human_vs_Zombies.Screens;
using Human_vs_Zombies.GameElements;

namespace Human_vs_Zombies.Menus.MenuDelegates
{
    class GameOverDelegate : IMenuDelegate
    {
        public GameOverDelegate()
        {
        }

        public void Run()
        {
            if (GameWorld.screens.Count > 0)
            {
                GameWorld.screens[GameWorld.screens.Count - 1].Disposed = true;
            }
            GameWorld.screens.Play(new StartScreen());
            GameWorld.audio.SongPlay("menu");
        }
    }
}
