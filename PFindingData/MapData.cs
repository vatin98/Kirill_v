using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PFindingData
{
    public class MapData
    {
        #region Public variables
        public int NumberRows;

        public int NumberColumns;

        public List<Point> Barriers;

        public List<Vector3> Dots;
        #endregion

        #region Ctors
        public MapData()
        {

        }
        public MapData(int columns, int rows, List<Point> barriers, List<Vector3> dots)
        {
            this.NumberColumns = columns;
            this.NumberRows = rows;
            this.Barriers = barriers;
            this.Dots = dots;
        }
        #endregion
    }
}
