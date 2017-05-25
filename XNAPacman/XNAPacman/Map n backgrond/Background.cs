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
    public class Background
    {

        #region Private variables

        private Texture2D backgroundTexture;

        #endregion

        #region Background ctor
        public Background()
        {

        }
        #endregion

        #region LoadContent
        public void LoadContent(ContentManager content)
        {
            backgroundTexture = content.Load<Texture2D>("background-white");
        }
        #endregion

        #region Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundTexture, new Vector2(0, 48), Color.Blue);
        }
        #endregion
    }
}
