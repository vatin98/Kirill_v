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
    class HUD:Objects
    {
        #region LoadContent
        public override void LoadContent(ContentManager content)
        {            
        }
        #endregion

        #region Draw
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Game1.HudFont, "1UP", new Vector2(48, 0), Color.White);
            spriteBatch.DrawString(Game1.HudFont, "High Score", new Vector2(160, 0), Color.White);

            spriteBatch.DrawString(Game1.HudFont, Items.PlayerDB[0].Score.ToString(), new Vector2(60, 16), Color.White);
        }
        #endregion
    }
}
