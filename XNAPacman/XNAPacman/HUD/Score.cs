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
    class Score
    {
        #region public variables
        public int Value;

        public Timer Timer;

        public Vector2 Position;

        public bool IsAlive;
        #endregion

        #region Score
        public Score(int time)
        {
            IsAlive = false;
            Timer = new Timer(time);
        }
        #endregion

        #region Update
        public void Update()
        {
            if (IsAlive)
            {
                Timer.Update();
            }
        }
        #endregion

        #region Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsAlive)
            {
                if (!Timer.IsFinished)
                {
                    spriteBatch.DrawString(Game1.ScoreFont, Value.ToString(), Position, Color.White);
                }
                else
                {
                    Timer.Reset();
                    IsAlive = false;
                }
            }
        }
        #endregion
    }
}
