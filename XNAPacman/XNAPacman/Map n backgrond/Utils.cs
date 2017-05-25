using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNAPacman
{
    class Utils
    {    
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
