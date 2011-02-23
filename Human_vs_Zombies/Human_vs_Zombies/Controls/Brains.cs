using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Human_vs_Zombies.HvZClasses;
using Human_vs_Zombies.HvZClasses.Mobs;

namespace Human_vs_Zombies.Controls
{
    /// <summary>
    /// The interface for controlling a player.
    /// </summary>
    public interface Brains
    {
        /// <summary>
        /// Updates this instance.
        /// </summary>
        void Update();

        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        /// <value>The player.</value>
        Player player { get; set; }

        /// <summary>
        /// Gets or sets the Human_vs_Zombies world.
        /// </summary>
        /// <value>The Human_vs_Zombies world.</value>
        HvZWorld hvzWorld { get; set; }
    }
}
