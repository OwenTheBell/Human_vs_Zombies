using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Human_vs_Zombies.GameElements
{
    class GameScreen
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GameScreen"/> is disposed.
        /// If this is set to true, the Screen Stack will automatically take it out of the stack.
        /// Just set this value to true and do nothing else if you want to delete the screen.
        /// </summary>
        /// <value><c>true</c> if disposed; otherwise, <c>false</c>.</value>
        public bool Disposed { get; set; }

        /// <summary>
        /// Gets the random number generator for this screen.
        /// </summary>
        /// <value>The random number generator.</value>
        public Random random { get; private set; }

/// <summary>
        /// Gets a value indicating whether [fading out].
        /// If a screen is fading out, the screen under it on the stack 
        /// will get updated. Otherwise, the screen under it on the stack
        /// will not get updated.
        /// </summary>        
        /// <value><c>true</c> if [fading out]; otherwise, <c>false</c>.</value>
        public bool FadingOut { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameScreen"/> class.
        /// </summary>
        public GameScreen()
        {
            this.random = new Random();
            this.Disposed = false;
            this.FadingOut = false;
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public virtual void Update()
        {
        }

        /// <summary>
        /// Draws this instance.
        /// </summary>
        public virtual void Draw()
        {
        }
    }
}
