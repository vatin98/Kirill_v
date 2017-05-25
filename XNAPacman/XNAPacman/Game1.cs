using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace XNAPacman
{
    public class Game1 : Game
    {
        #region Private variable
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        #endregion

        #region Public variable
        public static SoundEffect GhostEyesSound;
        public static SoundEffectInstance GhostEyesSoundInstance;
        public static SoundEffect EatGhostSound;

        public static Game1 PacmanGame;

        public static GameState GameState;

        public static Background Background;

        public static Map Map;

        public static Texture2D DotStripTexture;
        public static Texture2D FruitStripTexture;

        public static Texture2D EnergizerStripTexture;
        public static Texture2D GhostEyesStripTexture;

        public static SpriteFont HudFont;
        public static SpriteFont ScoreFont;

        public static int CurrentPlayer;

        #endregion

        #region Constants

        public const int TileSize = 16;

        #endregion

        #region Ctor
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            PacmanGame = this;

            graphics.PreferredBackBufferHeight = 576;
            graphics.PreferredBackBufferWidth = 448;
            graphics.ApplyChanges();

            Background = new Background();

            Map = new Map();

            CurrentPlayer = 0;   
        }
        #endregion

        #region Initialize
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Items.Initialize();
            LevelDB.Initialize();
            GameState = GameState.Game;
            base.Initialize();
        }
        #endregion

        #region LoadContent
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            HudFont = Content.Load<SpriteFont>("HUDFont");
            ScoreFont = Content.Load<SpriteFont>("ScoreFont");

            foreach (Objects obj in Items.ObjectsList)
            {
                obj.LoadContent(Content);
            }
            Background.LoadContent(Content);

            Map.LoadContent(Content);

            DotStripTexture = Content.Load<Texture2D>("DotStrip");

            FruitStripTexture = Content.Load<Texture2D>("Bonuses");

            EnergizerStripTexture = Content.Load<Texture2D>("EnergizerTile");

            GhostEyesStripTexture = Content.Load<Texture2D>("EyesTile");

            GhostEyesSound = Content.Load<SoundEffect>("Sounds\\eyes");
            GhostEyesSoundInstance = GhostEyesSound.CreateInstance();
            GhostEyesSoundInstance.IsLooped = true;
            GhostEyesSoundInstance.Volume = 0.2f;

            EatGhostSound = Content.Load<SoundEffect>("Sounds\\ghost");

        }

        #endregion

        #region UnloadContent
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        #endregion

        #region Update
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            if (GameState == GameState.Game)
            {
                foreach (Objects obj in Items.ObjectsList)
                {
                    obj.Update(gameTime);
                }
            }


            base.Update(gameTime);
        }

        #endregion

        #region Draw
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            if (GameState == GameState.Game)
            {
                GraphicsDevice.Clear(Color.Black);

                #region DrawBackground
                spriteBatch.Begin();

                Background.Draw(spriteBatch);

                spriteBatch.End();
                #endregion

                spriteBatch.Begin();

                foreach(KeyValuePair<Point,Dot> dot in Items.Dotlist)
                {
                    if (dot.Value.IsAlive)
                        dot.Value.Draw(spriteBatch);
                }

                foreach (Objects obj in Items.ObjectsList)
                {
                    obj.Draw(spriteBatch);
                }

                spriteBatch.End();
            }

            base.Draw(gameTime);
        }
        #endregion


    }
}