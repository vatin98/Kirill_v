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
    class Lives:Objects
    {
        #region Ctor
        public Lives(string textureName)
        {
            TextureName = textureName;
        }
        #endregion

        #region LoadContent
        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
        }
        #endregion

        #region Draw
        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Items.PlayerDB[Game1.CurrentPlayer].Lives; i++)
            {
                spriteBatch.Draw(Texture, new Vector2(i*32, 544), Color.Red);
            }
        }
        #endregion
    }
}
