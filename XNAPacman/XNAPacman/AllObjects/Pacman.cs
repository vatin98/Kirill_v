using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using XNAPacman;

namespace XNAPacman
{
    class Pacman : Objects
    {

        #region Private Variable

        private KeyboardState currentKeyboardState;
        private KeyboardState previousKeyboardState;

        private Animation pacmanAnimation;

        private GamePadState currentGamepadState;
        private GamePadState previousGamepadState;

        private SoundEffect walkingSound;
        private SoundEffectInstance walkingSoundInst;

        private SoundEffect energizerSound;

        private SoundEffect eatFruitSound;

        private SoundEffect pacmanDiesSound;

        private Point fruitMapPosition;

        private float volume = 0.1f;

        private bool walking;
        private bool fruit70Done;
        private bool fruit170Done;
        private bool isRespawned;


        private int frameHeight = 32;
        private int frameWidth = 32;
        private int frameCount = 3;
        private int frameTime = 60;

        private int energizerTimer;
        private int energizerTime;
        private int flashTime;

        private Timer expireTimer;

        #endregion

        #region Public variables

        public SoundEffect SireneSound;
        public SoundEffectInstance SireneSoundInstance;

        public SoundEffect EnergizerLoopSound;
        public SoundEffectInstance EnergizerSoundInstance;

        public bool IsGameOver;
        public int DotsCounter;

        public bool IsEnergizer;
        public bool IsFlashing;
        public bool IsReady;
        public bool isExpire;

        public int GhostDestroyedCounter;

        #endregion

        //Is READY TO FALSE
        #region PacmanCtor

        public Pacman(Vector2 position, string textureName)
        {
            TextureName = textureName;

            Position = position;

            IsAlive = true;

            Speed = 2.0f;

            pacmanAnimation = new Animation();

            IsGameOver = false;
            IsEnergizer = false;
            IsFlashing = false;
            IsReady = true;

            fruit70Done = false;
            fruit170Done = false;

            isRespawned = false;
            isExpire = false;

            GhostDestroyedCounter = 0;

            expireTimer = new Timer(120);
        }

        #endregion

        #region LoadContent
        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            pacmanAnimation.Initialize(Texture, Position, frameWidth, frameHeight,
                frameCount, frameTime, Color.White, Scale, true, 0);

            Center = new Vector2(pacmanAnimation.FrameWidth / 2, pacmanAnimation.FrameWidth / 2);

            walkingSound = content.Load<SoundEffect>("Sounds\\pacman_walk");

            walkingSoundInst = walkingSound.CreateInstance();
            walkingSoundInst.IsLooped = false;
            walkingSoundInst.Volume = volume;

            SireneSound = content.Load<SoundEffect>("Sounds\\siren");
            SireneSoundInstance = SireneSound.CreateInstance();
            SireneSoundInstance.IsLooped = true;
            SireneSoundInstance.Volume = 5 * volume;

            EnergizerLoopSound = content.Load<SoundEffect>("Sounds\\jrenergizer");
            EnergizerSoundInstance = EnergizerLoopSound.CreateInstance();
            EnergizerSoundInstance.IsLooped = true;
            EnergizerSoundInstance.Volume = 2 * volume;
            EnergizerSoundInstance.Pitch = -0.2f;

            energizerSound = content.Load<SoundEffect>("Sounds\\energizer");

            eatFruitSound = content.Load<SoundEffect>("Sounds\\fruit");

            pacmanDiesSound = content.Load<SoundEffect>("Sounds\\death");
        }
        #endregion

        #region Update
        public override void Update(GameTime gameTime)
        {
            #region get keyboard
            currentKeyboardState = Keyboard.GetState();
            currentGamepadState = GamePad.GetState(PlayerIndex.One);
            #endregion

            #region ESC
            if (currentKeyboardState.IsKeyDown(Keys.Escape))
            {
                Game1.PacmanGame.Exit();
            }
            #endregion

            #region Game
            if (Game1.GameState == GameState.Game)
            {

                #region Expire
                if (isExpire && !IsGameOver)
                {
                    expireTimer.Update();
                    if (expireTimer.IsFinished)
                    {
                        DisableGhosts();

                        expireTimer.IsFinished = false;
                    }
                }
                #endregion

                if (!IsAlive) return;

                walking = false;
             
                #region Keys            
                #region keyUp
                if (currentKeyboardState.IsKeyDown(Keys.Up) && IsReady || (currentKeyboardState.IsKeyDown(Keys.W)) && IsReady
                    || currentGamepadState.DPad.Up == ButtonState.Pressed && IsReady)
                {
                    Point mapLoc = Utils.WorldToMap(new Vector2(Position.X - Game1.TileSize, Position.Y - 9));

                    if (Game1.Map.IsOpenLocation(mapLoc.X, mapLoc.Y - 1))
                    {

                        walking = true;
                        Rotation = (int)RotationEnum.Nord;

                        Position.Y -= Speed;
                        Position.X = SnapToX(Position);

                        CheckDotCollision(mapLoc);
                        CheckFruitCollision(mapLoc);
                        CheckGhostCollision(Utils.WorldToMap(new Vector2(Position.X - 32, Position.Y - Game1.TileSize)));
                    } 
                }
                
                #endregion

                #region keyDown
                if (currentKeyboardState.IsKeyDown(Keys.Down) && IsReady || (currentKeyboardState.IsKeyDown(Keys.S) && IsReady)
                    || currentGamepadState.DPad.Down == ButtonState.Pressed && IsReady)
                {
                    Point mapLoc = Utils.WorldToMap(new Vector2(Position.X - Game1.TileSize, Position.Y - 23));

                    if (Game1.Map.IsOpenLocation(mapLoc.X, mapLoc.Y + 1))
                    {
                        walking = true;
                        Rotation = (int)RotationEnum.South;

                        Position.Y += Speed;
                        Position.X = SnapToX(Position);

                        CheckDotCollision(mapLoc);
                        CheckFruitCollision(mapLoc);
                        CheckGhostCollision(Utils.WorldToMap(new Vector2(Position.X - 32, Position.Y - 22)));
                    }
                }
                #endregion

                #region keyLeft
                if (currentKeyboardState.IsKeyDown(Keys.Left) && IsReady || (currentKeyboardState.IsKeyDown(Keys.A)) && IsReady
                    || currentGamepadState.DPad.Left == ButtonState.Pressed && IsReady)
                {
                    Point mapLoc = Utils.WorldToMap(new Vector2(Position.X - 10, Position.Y - Game1.TileSize));

                    if (Game1.Map.IsOpenLocation(mapLoc.X - 1, mapLoc.Y))
                    {
                        walking = true;
                        Rotation = (int)RotationEnum.West;

                        Position.X -= Speed;
                        Position.Y = SnapToY(Position);

                        CheckDotCollision(mapLoc);
                        CheckFruitCollision(mapLoc);
                        CheckGhostCollision(mapLoc);

                        if (Game1.Map.IsInTunnel && mapLoc.X == -3)

                        {
                            Position.X = 540;
                        }
                    }
                }
                #endregion

                #region keyRight
                if (currentKeyboardState.IsKeyDown(Keys.Right) && IsReady || (currentKeyboardState.IsKeyDown(Keys.D)) && IsReady
                     || currentGamepadState.DPad.Right == ButtonState.Pressed && IsReady)
                {
                    Point mapLoc = Utils.WorldToMap(new Vector2(Position.X - 24, Position.Y - Game1.TileSize));

                    if (Game1.Map.IsOpenLocation(mapLoc.X + 1, mapLoc.Y))
                    {
                        walking = true;
                        Rotation = (int)RotationEnum.East;

                        Position.X += Speed;
                        Position.Y = SnapToY(Position);

                        CheckDotCollision(mapLoc);
                        CheckFruitCollision(mapLoc);
                        CheckGhostCollision(mapLoc);

                        if (Game1.Map.IsInTunnel && mapLoc.X == 30)

                        {
                            Position.X = -88;
                        }
                    }
                }
                #endregion
                #endregion

                #region Energizer
                if (IsEnergizer)
                {
                    if (energizerTimer > 0)
                    {
                        energizerTimer--;
                        if (energizerTimer == flashTime)
                        {
                            IsFlashing = true;
                        }
                    }
                    else
                    {
                        IsEnergizer = false;
                        IsFlashing = false;

                        EnergizerSoundInstance.Stop();

                        if (IsReady) SireneSoundInstance.Play();

                    }
                }
                #endregion

                #region Fruit

                #region Show fruit after 70 dots
                if (DotsCounter == 70 && !fruit70Done)
                {
                    Items.Fruit.Timer.Reset();

                    Items.Fruit.IsAlive = true;

                    fruit70Done = true;
                }
                #endregion

                #region Show fruit after 170 dots
                if (DotsCounter == 170 && !fruit170Done)
                {
                    Items.Fruit.Timer.Reset();

                    Items.Fruit.IsAlive = true;

                    fruit170Done = true;
                }

                #endregion
                #endregion

                #region update animation

                pacmanAnimation.Position = Position;
                pacmanAnimation.Rotation = Rotation;
                pacmanAnimation.Update(gameTime);

                #endregion

            }
            #endregion

            #region set keyboard
            previousKeyboardState = currentKeyboardState;
            previousGamepadState = currentGamepadState;
            #endregion


            base.Update(gameTime);
        }
        #endregion

        #region Draw

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Game1.GameState == GameState.Game)
            {

                if (IsAlive && !isExpire || IsAlive && isRespawned)
                {
                    pacmanAnimation.Draw(spriteBatch, Center, walking);
                }
                else if (IsAlive && isExpire)
                {
                    if (Rotation > 0)
                    {
                        Rotation -= 4;
                        Scale -= 0.011f;
                        pacmanAnimation.Draw(spriteBatch, Rotation, Scale);
                    }
                    else
                    {
                        IsAlive = false;

                        Scale = 1.0f;
                    }
                }
            }
        }

        #endregion

        #region Snap to x
        public float SnapToX(Vector2 pos)
        {
            pos -= new Vector2(pos.X % Game1.TileSize, pos.Y % Game1.TileSize);
            return pos.X + 8;
        }
        #endregion

        #region Snap to y
        public float SnapToY(Vector2 pos)
        {
            pos -= new Vector2(pos.X % Game1.TileSize, pos.Y % Game1.TileSize);
            return pos.Y + 8;
        }
        #endregion

        #region DotsCollision
        private void CheckDotCollision(Point point)
        {
            if (Items.Dotlist.ContainsKey(point))
            {
                if (Items.Dotlist[point].IsAlive)
                {
                    Items.Dotlist[point].IsAlive = false;

                    walkingSoundInst.Play();

                    DotsCounter++;

                    switch (Items.Dotlist[point].Size)
                    {
                        case 0:
                            {
                                Items.PlayerDB[Game1.CurrentPlayer].Score += 10;
                            }
                            break;
                        case 1:
                            {
                                IsEnergizer = true;

                                energizerSound.Play(0.2f, 0, 0);

                                SireneSoundInstance.Stop();

                                EnergizerSoundInstance.Play();

                                energizerTime = Items.LevelDB[LevelDB.Count].frightTimeSeconds * 60;

                                energizerTimer = energizerTime;

                                IsFlashing = false;

                                flashTime = Items.LevelDB[LevelDB.Count].flashes * 24;

                                GhostDestroyedCounter = 0;

                                Items.PlayerDB[Game1.CurrentPlayer].Score += 50;
                            }
                            break;

                    }
                }
            }
        }
        #endregion
    
        #region FruitCollision
        private void CheckFruitCollision(Point point)
        {
            fruitMapPosition = Utils.WorldToMap(new Vector2(Items.Fruit.Position.X, Items.Fruit.Position.Y + 8));
            if (point == fruitMapPosition && Items.Fruit.IsAlive)
            {
                eatFruitSound.Play(volume, 0, 0);

                Items.Fruit.Timer.Reset();

                Items.Fruit.Timer.Value = Items.Fruit.GetNewTimeValue();

                Items.Fruit.IsAlive = false;

                Items.PlayerDB[Game1.CurrentPlayer].Score += Items.LevelDB[LevelDB.Count].bonusPoints;

                Items.Fruit.Score.Value = Items.LevelDB[LevelDB.Count].bonusPoints;

                Items.Fruit.Score.Position = Items.Fruit.Position;

                Items.Fruit.Score.IsAlive = true;
            }
        }
        #endregion

        #region GhostCollision
        private void CheckGhostCollision(Point point)
        {
            Items.BlueGhost.Collision(point);
            Items.OrangeGhost.Collision(point);
            Items.PinkyGhost.Collision(point);
            Items.RedGhost.Collision(point);
        }
        #endregion

        #region DisableGhosts
        public void DisableGhosts()
        {
            Items.BlueGhost.IsAlive = false;
            Items.RedGhost.IsAlive = false;
            Items.OrangeGhost.IsAlive = false;
            Items.PinkyGhost.IsAlive = false;
        }
        #endregion

        #region Expire
        public void Expire()
        {
            IsReady = false;
            isExpire = true;
            isRespawned = false;
            Rotation = 360;

            pacmanDiesSound.Play(0.2f, 0, 0);

            SireneSoundInstance.Stop();

            Game1.GhostEyesSoundInstance.Stop();

            expireTimer.Reset();

            expireTimer.IsAlive = true;

            Items.Fruit.IsAlive = false;

            if (Items.PlayerDB[Game1.CurrentPlayer].Lives - 1 == -1)
            {
                IsGameOver = true;
            }
        }
        #endregion


    }
}
