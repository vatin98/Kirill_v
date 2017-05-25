using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XNAPacman
{
    class Ghost : Objects
    {
       
        
       
        #region Constants
        private int frameHeight = 32;
        private int frameWidth = 32;
        private int frameCount = 8;
        private int frameTime = 90;
        private float scale = 1.0f;

        
        
        #endregion

        #region Private Variables

        private Point mapPosition;

        private int startFrame;
        private int result;

        private GhostMods mods;

        private Vector2 direction;

        private bool walking;

        #endregion

        #region Public variables

        public Animation GhostAnimation;

        public Score Score;

        public bool IsMoving;
        #endregion
        //mods ghost
        #region Ctor
        public Ghost(Vector2 position, string textureName, int startFrame)
        {
            TextureName = textureName;
            Position = position;
            IsAlive = true;
            IsMoving = false;

            this.startFrame = startFrame;

            GhostAnimation = new Animation();

            Score = new Score(60);

            mods = GhostMods.Home;

        }
        #endregion

        #region LoadContent

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            Center = new Vector2(GhostAnimation.FrameWidth / 2, GhostAnimation.FrameWidth / 2);
            GhostAnimation.Initialize(Texture, Position, frameWidth, frameHeight,
                frameCount, frameTime, Color.White, scale, true, startFrame);
        }
        #endregion

        #region Update

        public override void Update(GameTime gameTime)
        {
            if (IsAlive)
            {

                if (Move() == Motion.left)
                {
                    Point mapLoc = Utils.WorldToMap(new Vector2(Position.X - 10, Position.Y - Game1.TileSize));

                    if (Game1.Map.IsOpenLocation(mapLoc.X - 1, mapLoc.Y))
                    {
                        walking = true;
                        Rotation = (int)RotationEnum.Nord;
                        Speed = 2.0f;
                        Position.X -= Speed;
                    }
                }
                if (Move()==Motion.right)
                {
                    Point mapLoc = Utils.WorldToMap(new Vector2(Position.X - 24, Position.Y - Game1.TileSize));

                    if (Game1.Map.IsOpenLocation(mapLoc.X + 1, mapLoc.Y))
                    {
                        walking = true;
                        Rotation = (int)RotationEnum.East;
                        Speed = 2.0f;
                        Position.X += Speed;
            
                        if (Game1.Map.IsInTunnel && mapLoc.X == 30)

                        {
                            Position.X = -88;
                        }
                    }
                }
                #region GhostCollision
                if (Items.Pacman.IsReady)
                    {
                        if (direction == new Vector2(-1, 0) || direction == new Vector2(1, 0))
                        {
                            Collision(Utils.WorldToMap(new Vector2(Items.Pacman.Position.X - Game1.TileSize,
                                Items.Pacman.Position.Y - Game1.TileSize)));

                        }
                        else
                        {
                            Collision(Utils.WorldToMap(new Vector2(Items.Pacman.Position.X - 32,
                                 Items.Pacman.Position.Y - Game1.TileSize)));
                        }
                    }
                    #endregion

                    #region energizer
                    if (Items.Pacman.IsEnergizer)
                    {
                        if (mods == GhostMods.Eyes)
                        {
                            GhostAnimation.SpriteStrip = Game1.GhostEyesStripTexture;
                        }
                        else
                        {
                            GhostAnimation.SpriteStrip = Game1.EnergizerStripTexture;
                        }
                    }
                    else
                    {
                        if (mods == GhostMods.Eyes)
                        {
                            GhostAnimation.SpriteStrip = Game1.GhostEyesStripTexture;
                        }
                        else
                        {
                            GhostAnimation.SpriteStrip = this.Texture;
                        }
                    }
                    #endregion

                    GhostAnimation.Position = Position;
                    GhostAnimation.Update(gameTime);


                    base.Update(gameTime);
                
            }
        }

        #endregion
        
        #region Draw
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsAlive)
            {
                if (Items.Pacman.IsEnergizer && mods != GhostMods.Eyes)
                {
                    if (!Items.Pacman.IsFlashing)
                    { GhostAnimation.Draw(spriteBatch, false); }

                    else
                    { GhostAnimation.Draw(spriteBatch, true); }

                }
                else
                {
                   //Упала коллизия
                    //Vector2 vec = new Vector2(Position.X - Game1.TileSize, Position.Y - Game1.TileSize);
                    GhostAnimation.Draw(spriteBatch);
                }
            }

        }
        #endregion

        #region Collision
        public void Collision(Point point)
        {
            if (IsAlive)
            {
                mapPosition = Utils.WorldToMap(new Vector2(Position.X - Game1.TileSize, Position.Y));
                if (point == mapPosition && IsAlive && mods != GhostMods.Eyes)
                {
                    if (Items.Pacman.IsEnergizer)
                    {
                        Game1.EatGhostSound.Play(0.2f, 0, 0);

                        mods = GhostMods.Eyes;

                        IsMoving = false;

                        Game1.GhostEyesSoundInstance.Play();

                        Items.Pacman.GhostDestroyedCounter++;

                        result = 0;
                        switch (Items.Pacman.GhostDestroyedCounter)
                        {
                            case 1: { result = 200; } break;
                            case 2: { result = 400; } break;
                            case 3: { result = 800; } break;
                            case 4: { result = 1600; } break;
                        }

                        Items.PlayerDB[Game1.CurrentPlayer].Score += result;

                        Score.Value = result;
                        Score.Position = new Vector2(Position.X - Game1.TileSize, Position.Y - Game1.TileSize);
                        Score.IsAlive = true;
                    }
                    else
                    {
                        Items.Pacman.Expire();
                    }
                }
            }
        }
        #endregion

        public Motion Move() 
        {

            //(int)Items.BlueGhost.Position.X + Game1.TileSize != Items.PinkyGhost

               
            Point mapLoc = Utils.WorldToMap(new Vector2(Position.X - 10, Position.Y - Game1.TileSize));
              if (Game1.Map.IsOpenLocation(mapLoc.X + 1, mapLoc.Y))
            {
                return Motion.right;
            }
            else if(Game1.Map.IsOpenLocation(mapLoc.X - 1, mapLoc.Y))
            {
                return Motion.left;
            }
          
            return Motion.nope; 
        }
        
    }
}
