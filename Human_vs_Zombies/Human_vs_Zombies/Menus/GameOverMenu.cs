using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Human_vs_Zombies.Menus.MenuDelegates;
using Human_vs_Zombies.GameElements;
using Human_vs_Zombies.Controls;

namespace Human_vs_Zombies.Menus
{
    class GameOverMenu : Menu
    {
        public GameOverMenu(Vector2 position, MenuAction[] actions, float spacing)
            : base(position, actions)
        {
            MenuEntry tryAgain = new MenuEntry(
                "Try Again",
                new MenuAction[] 
                { 
                    new MenuAction(ActionType.Select, new GameOverDelegate())
                },
                position);

            MenuEntry quit = new MenuEntry(
                "Quit",
                new MenuAction[] 
                { 
                    new MenuAction(ActionType.Select, new QuitGameDeleage())
                },
                position + new Vector2(0, spacing));

            quit.UpperMenu = tryAgain;
            quit.LowerMenu = tryAgain;

            tryAgain.UpperMenu = quit;
            tryAgain.LowerMenu = quit;

            this.Add(tryAgain);
            this.Add(quit);
        }
    }
}

