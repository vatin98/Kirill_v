using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace XNAPacman
{
    class Animation
    {

        #region Private Variables
        private float scale;

        private int elapsedTime;

        private int timeToFrame;

        private int currentFrame;

        private Color color;

        private Rectangle sourceRect = new Rectangle();
        #endregion

        #region Public Variables
        public Rectangle DestenationRect = new Rectangle();

        public Texture2D SpriteStrip;

        public int FrameCount;

        public int FrameWidth;

        public int FrameHeight;

        public bool IsActive;

        public bool IsLooping;

        public Vector2 Position;

        public float Rotation;
        #endregion

        #region Initialize
        public void Initialize(Texture2D texture, Vector2 position, int frameWidth, int frameHeight,
            int frameCount, int timeToFrame, Color color, float scale, bool looping, int startFrame)
        {
            this.color = color;
            this.FrameHeight = frameHeight;
            this.FrameWidth = frameWidth;
            this.FrameCount = frameCount;
            this.timeToFrame = timeToFrame;
            this.scale = scale;

            IsLooping = looping;
            Position = position;
            SpriteStrip = texture;

            elapsedTime = 0;

            currentFrame = startFrame;

            IsActive = true;
        }
        #endregion

        #region Update
        public void Update(GameTime gameTime)
        {
            if (!IsActive) return;

            elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsedTime > timeToFrame)
            {
                currentFrame++;

                if (currentFrame == FrameCount)
                {
                    currentFrame = 0;
                    if (!IsLooping) IsActive = false;
                }

                elapsedTime = 0;
            }
            sourceRect = new Rectangle(currentFrame * FrameWidth, 0, FrameWidth, FrameHeight);

            DestenationRect = new Rectangle((int)Position.X - (int)(FrameWidth * scale) / 2,
                (int)Position.Y - (int)(FrameHeight * scale) / 2,
                (int)(FrameWidth * scale), (int)(FrameHeight * scale));

        }
        #endregion

        #region Draw
        public void Draw(SpriteBatch spriteBatch, Vector2 origin, bool walking)
        {
            if (IsActive)
            {

                if (!walking) sourceRect = new Rectangle(0, 0, FrameWidth, FrameHeight);

                spriteBatch.Draw(SpriteStrip, DestenationRect, sourceRect, color,
                    MathHelper.ToRadians(Rotation), origin, SpriteEffects.None, 0);
            }
        }

        public void Draw(SpriteBatch spriteBatch, float rotation, float scale)
        {
            if (IsActive)
            {
                sourceRect = new Rectangle(0, 0, FrameWidth, FrameHeight);

                spriteBatch.Draw(SpriteStrip, new Vector2(Position.X - 8, Position.Y - Game1.TileSize), sourceRect,
                    Color.White, rotation, new Vector2(Game1.TileSize, Game1.TileSize), scale, SpriteEffects.None, 0);
            }
        }
        //Отрисовка по дефолту призраков, упала коллизия
        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsActive) spriteBatch.Draw(SpriteStrip, new Vector2(Position.X - 8, Position.Y - Game1.TileSize), sourceRect,
                  Color.White, 0, new Vector2(Game1.TileSize, Game1.TileSize), scale, SpriteEffects.None, 0);/*spriteBatch.Draw(SpriteStrip, DestenationRect, sourceRect, color);*/

        }

        public void Draw(SpriteBatch spriteBatch, bool flashing)
        {
            if (IsActive)
            {
                if (!flashing)
                    sourceRect = new Rectangle(0, 0, FrameWidth, FrameHeight);

                spriteBatch.Draw(SpriteStrip, DestenationRect, sourceRect, Color.White);
            }
        }
      
        #endregion



    }
}
