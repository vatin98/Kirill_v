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
    class Cursor
    {
        #region Private Variables

        private Vector2 origin = Vector2.Zero;

        private Texture2D texture;

        private MouseState currentMousState;

        private float scale;
        #endregion

        #region Public Variables

        public Vector2 position = Vector2.Zero;

        public Color color;

        #endregion

        #region Cursor Ctor
        public Cursor(Vector2 position, Color color, float scale)
        {
            this.position = position;
            this.color = color;
            this.scale = scale;
        }
        #endregion

        #region LoadContent
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Cursor");

            origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }
        #endregion

        #region Update cursor
        public void Update(GameTime gameTime)
        {
            currentMousState = Mouse.GetState();

            position = new Vector2(currentMousState.X, currentMousState.Y);

            SnapToGrid();
        }
        #endregion

        #region DrawCursor
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, color, 0, origin, scale, SpriteEffects.None, 0);

            spriteBatch.DrawString(Game1.HudFont, "World X : " + (position.X - Game1.TileSize).ToString() + " Y : " + position.Y, 
                new Vector2(160, 550), Color.White);
        }
        #endregion

        #region SnapToGrid
        public virtual void SnapToGrid()
        {
            position -= new Vector2(position.X % Game1.TileSize, position.Y % Game1.TileSize);
        }
        #endregion

    }
}
