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
    public class Dot
    {
        
        #region Private variables
        private Rectangle sourceRect;
        private Rectangle destRect;
        #endregion

        #region Public variables
        public Vector2 Position;
        public int Size;
        public bool IsAlive;
        #endregion

        #region Ctor
        public Dot(Vector2 position, int size)
        {
            this.Position = position;
            this.Size = size;
            this.IsAlive = true;

            sourceRect = new Rectangle(size * Game1.TileSize, 0, Game1.TileSize, Game1.TileSize);

            destRect = new Rectangle((int)position.X, (int)position.Y, Game1.TileSize, Game1.TileSize);
        }
        #endregion

        #region Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsAlive)
                spriteBatch.Draw(Game1.DotStripTexture, destRect, sourceRect, Color.White);
        }
        #endregion

    }

}
