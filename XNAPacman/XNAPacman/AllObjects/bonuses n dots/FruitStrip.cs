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
    class FruitStrip:Objects
    {
        #region Constants
        private const int Fruit_Tile = 32;
        #endregion

        #region Private variables
        private Rectangle sourceRect;
        private Rectangle destRect;
        #endregion

        #region Ctor
        public FruitStrip()
        {

        }
        #endregion

        #region LoadContent
        public override void LoadContent(ContentManager content)
        {
            
        }
        #endregion

        #region Draw
        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Items.LevelDB[LevelDB.Count].bonusSymbol+1; i++)
            {
                sourceRect = new Rectangle(i * Fruit_Tile, 0, Fruit_Tile, Fruit_Tile);

                destRect = new Rectangle(448 - ((i + 1) * Fruit_Tile), 544, Fruit_Tile, Fruit_Tile);

                spriteBatch.Draw(Game1.FruitStripTexture, destRect, sourceRect, Color.White);
            }
        }
        #endregion
    }
}
