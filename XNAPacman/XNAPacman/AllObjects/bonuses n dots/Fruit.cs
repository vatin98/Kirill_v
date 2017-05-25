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
    class Fruit:Objects
    {
        #region Constants
        private const int Fruit_Tile = 32;
        #endregion

        #region Private variables
        private Rectangle sourceRect;
        private Rectangle destRect;
        private Random random;
        #endregion

        #region Public variable
        public Score Score;

        public Timer Timer;


        #endregion

        #region Ctor
        public Fruit()
        {
            IsAlive = false;
            Position = new Vector2(208,312);

            Score = new Score(60);

            random = new Random();

            Timer = new Timer(GetNewTimeValue());
        }
        #endregion

        #region LoadContent
        public override void LoadContent(ContentManager content)
        {
            
        }
        #endregion

        #region Update
        public override void Update(GameTime gameTime)
        {
            if (IsAlive)
            {
                Timer.Update();

                if (Timer.IsFinished)
                {
                    Timer.Reset();

                    Timer.Value = GetNewTimeValue();

                    IsAlive = false;
                }
            }
            if (Score.IsAlive)
            {
                Score.Update();
            }
        }
        #endregion

        #region Draw
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsAlive)
            {
                sourceRect = new Rectangle(Items.LevelDB[LevelDB.Count].bonusSymbol * Fruit_Tile, 0, Fruit_Tile, Fruit_Tile);

                destRect = new Rectangle((int)Position.X, (int)Position.Y, Fruit_Tile, Fruit_Tile);

                spriteBatch.Draw(Game1.FruitStripTexture, destRect, sourceRect, Color.White);
            }
            if (Score.IsAlive)
            {
                Score.Draw(spriteBatch);
            }
        }

        #endregion

        #region GetNewTime
        public int GetNewTimeValue()
        {
            return random.Next(540, 600);
        }
        #endregion
    }
}
