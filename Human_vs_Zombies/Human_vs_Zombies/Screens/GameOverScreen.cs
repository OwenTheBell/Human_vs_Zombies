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
    public class GameOverScreen : GameScreen
    {
        /// <summary>
        /// This is the time (DateTime, not GameClock) 
        /// that the screen is created.
        /// </summary>
        private long initialTime;

        /// <summary>
        /// This is the menu used for the pause screen.
        /// </summary>
        private GameOverMenu menu;

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
        public GameOverScreen()
            : base()
        {
            // Note: Do not use GameClock, it will be paused!
            this.initialTime = DateTime.Now.Ticks;
            this.menu = new GameOverMenu(
                new Vector2(1000, 330),
                new MenuAction[]
                { 
                    new MenuAction(ActionType.GoBack, new GameOverDelegate()),
                },
                75f);

            this.textDrawPosition = new Vector2(Settings.worldWidth / 2, 200);
            this.textDrawOrigin = Drawer.font.MeasureString("YOU WERE OVERRUN BY THE HORDE") / 2f;
        }

        /// <summary>
        /// Updates this instance. This makes sure that GameClock is paused,
        /// and it also updates the menu.
        /// </summary>
        public override void Update(float dTime)
        {
            base.Update(dTime);

            this.menu.Update();
        }

        /// <summary>
        /// Draws this instance.
        /// </summary>
        public override void Draw()
        {

            Drawer.Draw(
                TextureStatic.Get("background"),
                Drawer.FullScreenRectangle,
                null,
                Color.Black,
                0f,
                Vector2.Zero,
                SpriteEffects.None,
                0f);

            Drawer.DrawString(
                "YOU WERE OVERRUN BY THE HORDE",
                this.textDrawPosition,
                Color.Red,
                0f,
                this.textDrawOrigin,
                2f,
                SpriteEffects.None,
                1f);

            // Draw menu
            this.menu.Draw();
        }
    }
}