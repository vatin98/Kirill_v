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
    class Utils
    {
       
        #region CheckKeyboard
        public static bool CheckKeyboard(KeyboardState current, KeyboardState previous, Keys key)
        {
            if (current.IsKeyDown(key) && previous.IsKeyUp(key)) return true;

            return false;
           
        }
        #endregion

        #region WorldToMap
        public static Point WorldToMap(Vector2 position)
        {
            Point mapPosition = new Point();

            mapPosition.X = (int)position.X / Game1.TileSize;
            mapPosition.Y = (int)position.Y / Game1.TileSize;

            return mapPosition;
        }
#endregion

    }
}
