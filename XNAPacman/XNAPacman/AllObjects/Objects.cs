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
    class Objects
    {
        #region Variables 


        public Texture2D Texture = null;

        public string TextureName = string.Empty;

        public Vector2 Position = Vector2.Zero;

        public Vector2 Center = Vector2.Zero;

        public float Scale = 1.0f;

        public float Rotation = 0.0f;

        public float Speed = 0.0f;

        public bool IsAlive = true;


        #endregion

        #region Ctor
        public Objects(Vector2 position)
        {
            Position = position;
        }
        public Objects()
        {

        }
        #endregion

        #region LoadContent
        public virtual void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>("" + this.TextureName);

            Center = new Vector2(Texture.Width / 2, Texture.Height / 2);
        }
        #endregion

        #region Update
        public virtual void Update(GameTime gameTime)
        {

        }
        #endregion

        #region Draw
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!IsAlive) return;

            spriteBatch.Draw(Texture, Position, null, Color.White, MathHelper.ToRadians(Rotation),
                Center, Scale, SpriteEffects.None, 0);
        }
       
        #endregion

    }
}
