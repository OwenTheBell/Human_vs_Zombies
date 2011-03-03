using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Human_vs_Zombies.GameElements;
using Microsoft.Xna.Framework;
using Human_vs_Zombies.Menus;
using Human_vs_Zombies.Menus.MenuDelegates;
using Human_vs_Zombies.Rendering;
using Microsoft.Xna.Framework.Graphics;
using Human_vs_Zombies.Controls;

namespace Human_vs_Zombies.Screens
{
    /// <summary>
    /// This instanciates a new Pause Screen.
    /// </summary>
    public class PauseScreen : GameScreen
    {
        /// <summary>
        /// This is the time (DateTime, not GameClock) 
        /// that the screen is created.
        /// </summary>
        private long initialTime;

        /// <summary>
        /// This is the menu used for the pause screen.
        /// </summary>
        private PauseMenu menu;

        /// <summary>
        /// Where to write "Paused"
        /// </summary>
        private Vector2 textDrawPosition;

        /// <summary>
        /// The center of the word "Paused".
        /// </summary>
        private Vector2 textDrawOrigin;

        /// <summary>
        /// Initializes a new instance of the <see cref="PauseScreen"/> class.
        /// </summary>
        public PauseScreen()
            : base()
        {
            // Note: Do not use GameClock, it will be paused!
            this.initialTime = DateTime.Now.Ticks;
            this.menu = new PauseMenu(
                new Vector2(1440, 300),
                new MenuAction[]
                { 
                    new MenuAction(ActionType.GoBack, new QuitTopDelegate()),
                },
                75);

            this.textDrawPosition = new Vector2(1440, 125);
            this.textDrawOrigin = Drawer.font.MeasureString("Paused") / 2f;
        }

        /// <summary>
        /// Updates this instance. This makes sure that GameClock is paused,
        /// and it also updates the menu.
        /// </summary>
        public override void Update( float dTime)
        {
            base.Update( dTime);

            this.menu.Update();
        }

        /// <summary>
        /// Draws this instance.
        /// </summary>
        public override void Draw()
        {
            // Write "Paused" at the center of the screen.
            Drawer.DrawString(
                "Paused",
                this.textDrawPosition,
                Color.Black,
                0f,
                this.textDrawOrigin,
                0.35f,
                SpriteEffects.None,
                1f);

            // Draw menu
            this.menu.Draw();
        }
    }
}
