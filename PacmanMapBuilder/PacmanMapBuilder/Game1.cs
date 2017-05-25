using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace PacmanMapBuilder
{
    #region GameState
    public enum GameState
    {
        MapEditor,
        Collision,
        DotsEditor,
    }
    #endregion
    public class Game1 : Game
    {
        #region Public variables

        public static SpriteFont HudFont;

        public static Texture2D TileStripTexture;

        public GameState GameState = GameState.MapEditor;
        #endregion

        #region Private variables
        private int gridWidth = 28;
        private int gridHeight = 31;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        private KeyboardState currentKeyboardState;
        private KeyboardState previousKeyboardState;

        private MouseState currentMouseState;
        private MouseState previousMouseState;

        private Grid grid;

        private Cursor cursor;
        private TileStrips tileStrip;

        private Point mapPosition = new Point();

        private Texture2D backgroundTexture;
        private Texture2D ScreenShotTexture;
        private List<Tile> tileList;

        #endregion

        #region Constants

        public const int TileSize = 16;

        #endregion

        #region Ctor
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 768;
            graphics.PreferredBackBufferHeight = 576;
            graphics.ApplyChanges();
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
            grid = new Grid(Game1.TileSize, gridWidth, gridHeight, new Vector2(160, 48));

            cursor = new Cursor(Vector2.Zero, Color.White, 1.0f);

            tileStrip = new TileStrips();

            tileList = new List<Tile>();
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

            spriteBatch = new SpriteBatch(GraphicsDevice);

            HudFont = Content.Load<SpriteFont>("HUD");

            tileStrip.LoadContent(Content);

            cursor.LoadContent(Content);

            backgroundTexture = Content.Load<Texture2D>("background-white");
        }
        #endregion

        #region UnloadContent
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {

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
            #region Get keyboard and mouse state

            currentKeyboardState = Keyboard.GetState();

            currentMouseState = Mouse.GetState();

            #endregion

            #region ESC
            if (Utils.CheckKeyboard(currentKeyboardState, previousKeyboardState, Keys.Escape))
            {
                this.Exit();
            }
            #endregion

            #region MapEditor F1
            if (Utils.CheckKeyboard(currentKeyboardState, previousKeyboardState, Keys.F1))
            {
                GameState = GameState.MapEditor;

                TileStripTexture = Content.Load<Texture2D>("tiles");

                tileStrip.TileCount = (TileStripTexture.Width / Game1.TileSize);

                cursor.color = Color.Red;
            }
            #endregion

            #region CollisionEditor F2
            if (Utils.CheckKeyboard(currentKeyboardState, previousKeyboardState, Keys.F2))
            {
                GameState = GameState.Collision;

                TileStripTexture = Content.Load<Texture2D>("collisionTile");

                tileStrip.TileCount = (TileStripTexture.Width / Game1.TileSize);

                cursor.color = Color.Pink;
            }
            #endregion

            #region DotsEditor F3
           
            if (Utils.CheckKeyboard(currentKeyboardState, previousKeyboardState, Keys.F3))
            {
                GameState = GameState.DotsEditor;

                TileStripTexture = Content.Load<Texture2D>("DotStrip");

                tileStrip.TileCount = (TileStripTexture.Width / Game1.TileSize);

                cursor.color = Color.Green;
            }
            
            #endregion

            #region Save Screen S     
            if (GameState == GameState.MapEditor)
            {
                if (Utils.CheckKeyboard(currentKeyboardState, previousKeyboardState, Keys.S))

                {
                    Draw(new GameTime());

                    int[] backBuffer = new int[graphics.PreferredBackBufferHeight * graphics.PreferredBackBufferWidth];

                    GraphicsDevice.GetBackBufferData(backBuffer);
                    try
                    {
                        using (ScreenShotTexture = new Texture2D(GraphicsDevice, graphics.PreferredBackBufferWidth, 
                            graphics.PreferredBackBufferHeight, false, SurfaceFormat.Color))
                        {
                            ScreenShotTexture.SetData(backBuffer);
                            Stream stream = File.OpenWrite("screenshot.png");
                            ScreenShotTexture.SaveAsPng(stream, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

                            stream.Dispose();
                            ScreenShotTexture.Dispose();
                        }
                    }
                    catch(Exception)
                    { }
                }
            }


            #endregion          

            #region Save collisionMap
            if (GameState == GameState.Collision)
            {
                if (Utils.CheckKeyboard(currentKeyboardState, previousKeyboardState, Keys.S))
                {
                    string fileName = "collision.txt";
                    File.Create(fileName).Dispose();

                    using (StreamWriter writer = new StreamWriter(fileName))
                    {
                        for (int x = 0; x < 28; x++)
                        {
                            for (int y = 0; y < 3; y++)
                            {
                                writer.WriteLine("  " + x.ToString() + " " + y.ToString());
                            }
                        }

                        foreach (Tile t in tileList)
                        {
                            if (t.Selected == 0)
                            {
                                mapPosition = Utils.WorldToMap(new Vector2(t.Position.X - 160, t.Position.Y));
                                string s = "  " + mapPosition.X.ToString() + " " + mapPosition.Y.ToString();
                                writer.WriteLine(s);
                            }
                        }

                        for (int x = 0; x < 28; x++)
                        {
                            for (int y = 34; y < 36; y++)
                            {
                                writer.WriteLine("  " + x.ToString() + " " + y.ToString());
                            }
                        }

                    }
                }
            }

            #endregion

            #region SaveDotMap
            if(GameState==GameState.DotsEditor)
            {
                if (Utils.CheckKeyboard(currentKeyboardState, previousKeyboardState, Keys.S))
                {
                    string filename = "dots.txt";
                    File.Create(filename).Dispose();

                    using (StreamWriter sw = new StreamWriter(filename))
                    {
                        foreach (Tile t in tileList)
                        {
                            mapPosition = Utils.WorldToMap(new Vector2(t.Position.X - 160, t.Position.Y));
                            string s = "  " + mapPosition.X + " " + mapPosition.Y + " " + t.Selected.ToString();
                            sw.WriteLine(s);
                        }

                    }
                }
            }
            #endregion

            #region Add and Del tiles
            if (cursor.position.X > 160 && cursor.position.X < 609 && cursor.position.Y > 47 && cursor.position.Y < 529)
            {
                if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
                {
                    AddTile();
                }
                if (currentMouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton == ButtonState.Released)
                {
                    DeleteTile(new Vector2(cursor.position.X - Game1.TileSize, cursor.position.Y));
                }

            }
            #endregion

            #region Update grid,cursor and tilestrip
            grid.Update(gameTime);

            cursor.Update(gameTime);

            tileStrip.Update(gameTime);
            #endregion

            #region Set keyboard and mouse state

            previousKeyboardState = currentKeyboardState;

            previousMouseState = currentMouseState;

            #endregion

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
            GraphicsDevice.Clear(Color.Black);

            #region Draw grid,cursor, tiles and tilestrip
            spriteBatch.Begin();

            if (GameState == GameState.Collision || GameState == GameState.DotsEditor)
            {
                spriteBatch.Draw(backgroundTexture, new Vector2(160, 48), Color.White);
            }

            grid.Draw(spriteBatch);

            cursor.Draw(spriteBatch);

            tileStrip.Draw(spriteBatch);

            foreach (Tile t in tileList)
            {
                t.Draw(spriteBatch);
            }


            spriteBatch.End();

            #endregion

            #region Draw map coord and gameState
            spriteBatch.Begin();

            string status = (GameState == GameState.MapEditor ? "Map editor" : (GameState == GameState.Collision) ? "Collision map" : "Dots editor");
            spriteBatch.DrawString(HudFont, status, new Vector2(160, 0), Color.White);


            mapPosition = Utils.WorldToMap(new Vector2(cursor.position.X - 176, cursor.position.Y));

            spriteBatch.DrawString(HudFont, "Map X : " + mapPosition.X.ToString() + " Y : " + mapPosition.Y.ToString(),
                new Vector2(410, 550), Color.White);

            spriteBatch.End();
            #endregion



            base.Draw(gameTime);
        }
        #endregion

        #region Checking tile(exist or no)
        private bool AlreadyExist(Tile tile)
        {
            foreach (Tile t in tileList)
            {
                if (t.Position == tile.Position)
                    return true;
            }
            return false;
        }
        #endregion

        #region Add tile
        private void AddTile()
        {
            Tile t = new Tile(new Vector2(cursor.position.X - Game1.TileSize, cursor.position.Y), tileStrip.Selected);
            if (!AlreadyExist(t))
            {
                tileList.Add(t);
            }
            t = null;
        }
        #endregion

        #region Del tile
        private void DeleteTile(Vector2 position)
        {
            for (int i = 0; i < tileList.Count; i++)
            {
                if (tileList[i].Position == position)
                    tileList.RemoveAt(i);
            }
        }
        #endregion    
    }
}
