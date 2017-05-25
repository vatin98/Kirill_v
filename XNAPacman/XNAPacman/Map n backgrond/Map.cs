using PFindingData;
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
    public class Map
    {
        #region private variables

        private int numberColumns;

        private int numberRows;

        private int currentMap = 0;

        private List<MapData> maps;

        private MapType[,] mapTiles;

        #endregion

        #region public vatiables
        public bool IsInTunnel = false;
        #endregion

        #region LoadContent
        public void LoadContent(ContentManager content)
        {
            maps = new List<MapData>();

            maps.Add(content.Load<MapData>("Map1"));

            SetMapData();
        }
        #endregion

        #region SetMapData
        private void SetMapData()
        {
            numberColumns = maps[currentMap].NumberColumns;
            numberRows = maps[currentMap].NumberRows;

            mapTiles = new MapType[numberColumns, numberRows];

            int x = 0;
            int y = 0;

            for (int i = 0; i < maps[currentMap].Barriers.Count; i++)
            {
                x = maps[currentMap].Barriers[i].X;
                y = maps[currentMap].Barriers[i].Y;
                mapTiles[x, y] = MapType.MapBarriier;
            }

            int size = 0;

            for (int i = 0; i < maps[currentMap].Dots.Count; i++)
            {
                x = (int)maps[currentMap].Dots[i].X;
                y = (int)maps[currentMap].Dots[i].Y;
                size = (int)maps[currentMap].Dots[i].Z;

                Dot dot = new Dot(new Vector2(x * Game1.TileSize, y * Game1.TileSize), size);
                Point pnt = new Point(x,y);

                Items.Dotlist.Add(pnt, dot);
            }

        }
        #endregion

        #region InMap
        public bool InMap(Point pnt)
        {
            return (pnt.Y >= 0 && pnt.Y < numberRows
                && pnt.X >= 0 && pnt.X < numberColumns);
        }
        public bool InMap(int column, int row)
        {
            return (row >= 0 && row < numberRows
                && column >= 0 && column < numberColumns);
        }
        #endregion

        #region CheckIsOpenLoc
        public bool IsOpenLocation(int column, int row)
        {
            if (InTunnel(column, row)) return true;

            if (InHome(column, row)) return false;

            return InMap(column, row) && mapTiles[column, row] != MapType.MapBarriier;
        
            

        }
        #endregion

        #region CheckInTunnel
        private bool InTunnel(int column, int row)
        {
            if (row == 17 && column <= 1 || column >= 28)
            {
                IsInTunnel = true;
                return IsInTunnel;
            }
            IsInTunnel = false;
            return IsInTunnel;
        }
        #endregion

        #region CheckInHome
        public bool InHome(int column, int row)
        {
            if (row == 15 && column == 13 || row == 15 && column == 14)
                return true;


            return false;         
        }
        #endregion
    }
}
