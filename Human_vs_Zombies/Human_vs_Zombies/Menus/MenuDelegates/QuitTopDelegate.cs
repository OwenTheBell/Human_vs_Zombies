using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Human_vs_Zombies.GameElements;

namespace Human_vs_Zombies.Menus.MenuDelegates
{
    /// <summary>
    /// This pops the most recent screen off of the screen
    /// stack of GameWorld.
    /// </summary>
    public class QuitTopDelegate : IMenuDelegate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuitTopDelegate"/> class.
        /// </summary>
        public QuitTopDelegate()
            : base()
        {
        }

        /// <summary>
        /// Runs this instance. This pops the most recent screen off of the screen
        /// stack of GameWorld.
        /// </summary>
        public void Run()
        {
            if (GameWorld.screens.Count > 0)
            {
                GameWorld.screens[GameWorld.screens.Count - 1].Disposed = true;
            }
        }
    }
}
