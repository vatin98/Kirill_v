using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;


namespace PacmanMapBuilder
{
    class Tile
    {
        #region Private vatiables

        private Rectangle sourceRect;
        private Rectangle destRect;

        #endregion

        #region Public variables

        public Vector2 Position;
        public int Selected = 0;

        #endregion

        #region Tile ctor
        public Tile(Vector2 position, int selected)
        {
            Selected = selected;
            Position = position;
        }
        #endregion

        #region Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            sourceRect = new Rectangle(Selected * Game1.TileSize, 0, Game1.TileSize, Game1.TileSize);

            destRect = new Rectangle((int)Position.X, (int)Position.Y, Game1.TileSize, Game1.TileSize);

            spriteBatch.Draw(Game1.TileStripTexture, destRect, sourceRect, Color.White);    
        }
        #endregion
    }
}
