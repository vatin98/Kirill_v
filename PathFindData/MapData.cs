using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFindData
{
    public class MapData
    {
        #region Public variables
        public int NumberRows;

        public int NumberColumns;

        public List<Point> Barriers;
        #endregion

        #region Ctors
        public MapData()
        {

        }
        public MapData(int columns, int rows, List<Point> barriersList)
        {
            NumberColumns = columns;
            NumberRows = rows;
            Barriers = barriersList;
        }
        #endregion
    }
}
