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
    class TileStrips
    {
        #region Private variable
        private KeyboardState currentKeyboardState;
        private KeyboardState previousKeyboardState;

        private Texture2D selector;

        private Rectangle sourceRect;

        private Rectangle destRect;
        #endregion

        #region Public variables
        public int Selected = 0;

        public int TileCount = 0;
        #endregion

        #region LoadContent
        public void LoadContent(ContentManager content)
        {
            Game1.TileStripTexture = content.Load<Texture2D>("tiles");

            selector = content.Load<Texture2D>("selector");

            TileCount = (Game1.TileStripTexture.Width / Game1.TileSize);
        }
        #endregion

        #region Update
        public void Update(GameTime gameTime)
        {
            currentKeyboardState = Keyboard.GetState();

            if (Utils.CheckKeyboard(currentKeyboardState, previousKeyboardState, Keys.Up))
            {
                Selected--;
            }
            if (Utils.CheckKeyboard(currentKeyboardState, previousKeyboardState, Keys.Down))
            {
                Selected++;

            }
            if (Selected < 0) Selected = 0;
            if (Selected > TileCount - 1) Selected = TileCount-1;
            

            previousKeyboardState = currentKeyboardState;
        }
        #endregion

        #region Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < TileCount; i++)
            {
                sourceRect = new Rectangle(i * Game1.TileSize, 0, Game1.TileSize, Game1.TileSize);
                destRect = new Rectangle(650, 48 + (i * selector.Width), Game1.TileSize, Game1.TileSize);

                spriteBatch.Draw(Game1.TileStripTexture, destRect, sourceRect, Color.White);
            }

            destRect = new Rectangle(648, 46 + (Selected * selector.Width), selector.Width, selector.Height);

            spriteBatch.Draw(selector, destRect, Color.White);
        }
        #endregion
    }
}
