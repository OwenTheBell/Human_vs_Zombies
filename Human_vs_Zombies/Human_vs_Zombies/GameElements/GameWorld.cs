using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Human_vs_Zombies.Controls;
using Human_vs_Zombies.Screens;
using Human_vs_Zombies.Rendering;
using Human_vs_Zombies.Mathematics;
using Human_vs_Zombies.Audio;

namespace Human_vs_Zombies.GameElements
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameWorld : Game
    {
        public static GraphicsDevice graphics;
        public static SpriteBatch spriteBatch;
        public static Vector2 screenDimensions;
        public static ScreenStack screens;
        public static ContentManager content;
        public static AudioManager audio;
        private static Boolean exitStatus;

        public static Controller controller { get; private set; }

        public GameWorld()
        {
            GraphicsDeviceManager manager = new GraphicsDeviceManager(this);
            manager.PreferredBackBufferWidth = Settings.screenWidth;
            manager.PreferredBackBufferHeight = Settings.screenHeight;
            exitStatus = false;

            Content.RootDirectory = "Content";
            screens = new ScreenStack();
            controller = new Controller(PlayerIndex.One);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            content = Content;
            graphics = GraphicsDevice;

            //graphics.PresentationParameters.BackBufferWidth
            //    = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width * 2;
            //graphics.PresentationParameters.BackBufferHeight
            //    = (int)(0.5625f * GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width * 2);
                        

            GameWorld.screenDimensions = new Vector2(1920, 1080);

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            TextureStatic.Load("background", @"Art\background");
            TextureStatic.Load("Dart", @"Art\TempDart");
            TextureStatic.Load("Zombie", @"Art\TempZombie");
            TextureStatic.Load("Human", @"Art\TempHuman");
            TextureStatic.Load("blank", @"Art\blank");
            TextureStatic.Load("Wall", @"Art\Wall");
            TextureStatic.Load("Shadow", @"Art\Shadow");
            TextureStatic.Load("Ammo", @"Art\Ammo");
            TextureStatic.Load("MenuBackground", @"Art\MenuBackground");

            audio = new AudioManager(this);

            audio.LoadSong("theme", "Sounds/zombie");
            audio.LoadSong("menu", "Sounds/zombie 2");
            audio.LoadSong("yakety", "Sounds/yaketysax");
            audio.LoadSong("death", "Sounds/death");
            audio.LoadSound("kill", "Sounds/kill");
            
            Drawer.Initiallize();
            
            screens.Play(new StartScreen());
            audio.SongPlay("menu");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || exitStatus)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);

            controller.Update();
            screens.Update();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            screens.Draw();

            spriteBatch.End();

            base.Draw(gameTime);
        }


        public static void ExitGame()
        {
            exitStatus = true;
        }
    }
}
