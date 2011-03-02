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
    class StartMenu : Menu
    {
        public StartMenu(Vector2 position, MenuAction[] actions, float spacing)
            : base(position, actions)
        {
            MenuEntry play = new MenuEntry(
                "Play",
                new MenuAction[] 
                { 
                    new MenuAction(ActionType.Select, new StartGameDelegate()) 
                },
                position);

            MenuEntry quit = new MenuEntry(
                "Quit",
                new MenuAction[] 
                { 
                    new MenuAction(ActionType.Select, new QuitGameDeleage())
                },
                position + new Vector2(0, spacing));

            play.UpperMenu = quit;
            play.LowerMenu = quit;

            quit.UpperMenu = play;
            quit.LowerMenu = play;

            this.Add(play);
            this.Add(quit);
        }
    }
}

