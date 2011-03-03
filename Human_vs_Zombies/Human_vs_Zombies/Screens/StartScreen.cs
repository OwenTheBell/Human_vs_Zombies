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
    public class StartScreen : GameScreen
    {
        /// <summary>
        /// This is the time (DateTime, not GameClock) 
        /// that the screen is created.
        /// </summary>
        private long initialTime;

        /// <summary>
        /// This is the menu used for the pause screen.
        /// </summary>
        private StartMenu menu;

        /// <summary>
        /// Where to write "Paused"
        /// </summary>
        private Vector2 textDrawPosition;
        private Vector2 textDrawPosition2;
        private Vector2 textDrawPosition3;
        private Vector2 textDrawPosition4;
        private Vector2 textDrawPosition5;

        /// <summary>
        /// The center of the word "Paused".
        /// </summary>
        private Vector2 textDrawOrigin;

        /// <summary>
        /// Initializes a new instance of the <see cref="PauseScreen"/> class.
        /// </summary>
        public StartScreen()
            : base()
        {
            // Note: Do not use GameClock, it will be paused!
            this.initialTime = DateTime.Now.Ticks;
            this.menu = new StartMenu(
                new Vector2(Settings.worldWidth / 2 + 500, 1000 * Drawer.GetRatio()),
                new MenuAction[]
                { 
                    new MenuAction(ActionType.GoBack, new StartGameDelegate()),
                },
                75f);

            this.textDrawPosition = new Vector2(Settings.worldWidth / 2, 700 * Drawer.GetRatio());
            this.textDrawPosition2 = new Vector2(Settings.worldWidth / 2, 700 * Drawer.GetRatio() + 200);
            this.textDrawPosition3 = new Vector2(Settings.worldWidth / 2, 700 * Drawer.GetRatio() + 300);
            this.textDrawPosition4 = new Vector2(Settings.worldWidth / 2, 700 * Drawer.GetRatio() + 400);
            this.textDrawPosition5 = new Vector2(Settings.worldWidth / 2, 700 * Drawer.GetRatio() + 500);
            this.textDrawOrigin = Drawer.font.MeasureString("Human Vs. Zombies") / 2f;
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
            int offset = (int)(initialTime - DateTime.Now.Ticks)/5000000;
            Drawer.Draw(
                TextureStatic.Get("MenuBackground"),
                new Rectangle(Math.Min(offset,300), 0, TextureStatic.Get("MenuBackground").Width, TextureStatic.Get("MenuBackground").Height),
                null,
                Color.White,
                0f,
                Vector2.Zero,
                SpriteEffects.None,
                0f);

            Drawer.DrawString(
                "HUMAN VS. ZOMBIES",
                this.textDrawPosition,
                Color.Red,
                0f,
                this.textDrawOrigin,
                4f,
                SpriteEffects.None,
                1f);

            Drawer.DrawString(
                "Ian Dimayuga",
                this.textDrawPosition2,
                Color.Red,
                0f,
                this.textDrawOrigin,
                2f,
                SpriteEffects.None,
                1f);

            Drawer.DrawString(
                "Owen Bell",
                this.textDrawPosition3,
                Color.Red,
                0f,
                this.textDrawOrigin,
                2f,
                SpriteEffects.None,
                1f);

            Drawer.DrawString(
                "Sean Kruer",
                this.textDrawPosition4,
                Color.Red,
                0f,
                this.textDrawOrigin,
                2f,
                SpriteEffects.None,
                1f);

            Drawer.DrawString(
                "Tom Dooner",
                this.textDrawPosition5,
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