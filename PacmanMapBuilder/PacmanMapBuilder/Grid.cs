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
    class Grid
    {
        #region Private Variables

        private KeyboardState currentKeyboardState;

        private KeyboardState previousKeyboardState;

        private int gridWidth;
        private int gridHeight;
        private int tileSize;

        private Vector2 gridPosition;

        private bool draw;

        #endregion

        #region Grid Ctor
        public Grid(int tileSize, int gridWidth, int gridHeight, Vector2 gridPosition)
        {
            this.tileSize = tileSize;
            this.gridHeight = gridHeight;
            this.gridWidth = gridWidth;
            this.gridPosition = gridPosition;

            draw = true;
        }
        #endregion

        #region Update
        public void Update(GameTime gameTime)
        {
            currentKeyboardState = Keyboard.GetState();

            if (Utils.CheckKeyboard(currentKeyboardState, previousKeyboardState, Keys.G)) draw = !draw;


            previousKeyboardState = currentKeyboardState;
        }
        #endregion

        #region Draw Grid
        public void Draw(SpriteBatch spriteBatch)
        {
            if (draw)
            {
                for(int x = 0;x<=gridWidth;x++)
                {
                    for (int y = 0; y <= gridHeight; y++)
                    {
                        Lines2D.DrawLine(spriteBatch, new Vector2(gridPosition.X, (y * tileSize) + gridPosition.Y),
                            new Vector2((gridWidth * tileSize) + gridPosition.X, (y * tileSize) + gridPosition.Y), Color.White);

                        Lines2D.DrawLine(spriteBatch, new Vector2((x * tileSize) + gridPosition.X, gridPosition.Y),
                            new Vector2((x * tileSize) + gridPosition.X, (gridHeight * tileSize) + gridPosition.Y), Color.White);
                    }
                }
            }
        }
        #endregion


    }
}
