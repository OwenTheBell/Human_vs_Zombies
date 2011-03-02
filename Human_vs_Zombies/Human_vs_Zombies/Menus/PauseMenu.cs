using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Human_vs_Zombies.Menus;
using Human_vs_Zombies.Screens;
using Human_vs_Zombies.Controls;
using Human_vs_Zombies.Menus.MenuDelegates;


namespace Human_vs_Zombies.Menus
{
    /// <summary>
    /// An instance of a pause menu. This will support the options
    /// to resume the game, restart the level, go to the options menu,
    /// buy the game (if in trial mode), go to the title sceen, and 
    /// quit the game.
    /// </summary>
    public class PauseMenu : Menu
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PauseMenu"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="actions">The actions.</param>
        /// <param name="spacing">The spacing.</param>
        public PauseMenu(Vector2 position, MenuAction[] actions, float spacing)
            : base(position, actions)
        {
            MenuEntry resume = new MenuEntry(
                "Resume",
                new MenuAction[] 
                { 
                    new MenuAction(ActionType.Select, new QuitTopDelegate()) 
                },
                position);

            MenuEntry quit = new MenuEntry(
                "Quit",
                new MenuAction[] 
                { 
                    new MenuAction(ActionType.Select, new QuitGameDeleage())
                },
                position + new Vector2(0, spacing));

            resume.UpperMenu = quit;
            resume.LowerMenu = quit;

            quit.UpperMenu = resume;
            quit.LowerMenu = resume;

            this.Add(resume);
            this.Add(quit);
        }
    }
}
