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
    class Items
    {
        #region Variables
        public static List<Objects> ObjectsList = new List<Objects>();

        public static Dictionary<Point, Dot> Dotlist = new Dictionary<Point, Dot>();

        public static Dictionary<int, Player> PlayerDB = new Dictionary<int, Player>();

        public static Dictionary<int, Level> LevelDB = new Dictionary<int, Level>();

        public static Pacman Pacman;

        public static Ghost BlueGhost;
        public static Ghost RedGhost;
        public static Ghost PinkyGhost;
        public static Ghost OrangeGhost;

        public static HUD HUD;
        public static HighScore HighScore;

        public static Fruit Fruit;
        #endregion

        #region Initialize
        public static void Initialize()
        {
            ObjectsList.Add(new Lives("lives"));

            ObjectsList.Add(new FruitStrip());
            ObjectsList.Add(Fruit=new Fruit());
           
            ObjectsList.Add(HUD = new HUD());

            ObjectsList.Add(HighScore = new HighScore());

            ObjectsList.Add(BlueGhost = new Ghost(new Vector2(170, 440), "blue_ghost_sprite",0));
            ObjectsList.Add(RedGhost = new Ghost(new Vector2(224, 288), "red_ghost_sprite", 2));
            ObjectsList.Add(PinkyGhost = new Ghost(new Vector2(192, 288), "purple_ghost_sprite", 4));
            ObjectsList.Add(OrangeGhost = new Ghost(new Vector2(256, 288), "orange_ghost_sprite", 6));
            
            ObjectsList.Add(Pacman = new Pacman(new Vector2(240, 440), "pacstill"));
       
            PlayerDB.Add(0, new Player());         
        }
        #endregion

    }
}
