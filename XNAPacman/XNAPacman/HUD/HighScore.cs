using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace XNAPacman
{
    class HighScore:Objects
    {
        #region Constants
        private const string File_Name = "Score.data";
        #endregion

        #region private variables

        private bool NewHighscore;

        private int highScore;
        #endregion

        #region Ctor
        public HighScore()
        {
            IsAlive = true;

            try
            {
                FileStream fs = new FileStream(File_Name,FileMode.Open,FileAccess.Read);
                BinaryReader reader = new BinaryReader(fs);

                highScore = reader.ReadInt32();

                reader.Close();
                fs.Close();
            }
            catch (IOException)
            {
                highScore = 0;
            }
        }
        #endregion

        #region LoadContent
        public override void LoadContent(ContentManager content)
        {
            
        }
        #endregion

        #region Update
        public override void Update(GameTime gameTime)
        {
            if (IsAlive)
            {
                if (Items.PlayerDB[Game1.CurrentPlayer].Score>highScore)
                {
                    highScore = Items.PlayerDB[Game1.CurrentPlayer].Score;

                    NewHighscore = true;
                }
                if (NewHighscore&&Items.Pacman.IsGameOver)
                {
                    WriteHighScore();
                }
            }
        }
        #endregion

        #region Draw
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsAlive)
            {
                spriteBatch.DrawString(Game1.HudFont, highScore.ToString(),
                    new Vector2(200 - highScore.ToString().Length, 16), Color.White);
            }
        }
        #endregion

        #region Write high score
        private void WriteHighScore()
        {
            try
            {
                FileStream fs = new FileStream(File_Name, FileMode.OpenOrCreate, FileAccess.Write);

                BinaryWriter writer = new BinaryWriter(fs);

                writer.Write(highScore);

                writer.Close();
                fs.Close();
            }
            catch (IOException)
            {              
            }
            NewHighscore = false;
        }
        #endregion

       
    }
}
