#region Using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace XNAPacman
{
    #region Level Class
   
    class Level
    {
        #region Public Variables
                                    
        public int bonusSymbol;
                          
        public int bonusPoints;
      
        public float pacmanSpeed;

        public float pacmanDotsSpeed;

        public float ghostSpeed;
 
        public float ghostTunnelSpeed;

        public int elroy1DotsLeft;
  
        public float elroy1Speed;

        public int elroy2DotsLeft;

        public float elroy2Speed;
  
        public float frightPacmanSpeed;
 
        public float frightPacmanDotsSpeed;
 
        public float frightGhostSpeed;

        public int frightTimeSeconds;
   
        public int flashes;
        #endregion
    }
    #endregion

    #region LevelDataBase

    class LevelDB
    {
        #region Public Variables
        
        public static int Count;
        #endregion

        #region Initialize
        public static void Initialize()
        {   
            Count = 0;
           
            AddLevel(0, (int)BonusSymb.Cherries, 100, 0.80f, 0.71f, 0.75f, 0.40f, 20, 0.80f, 10, 0.85f, 0.90f, 0.79f, 0.50f, 6, 5);
            AddLevel(1, (int)BonusSymb.Strawberry, 300, 0.90f, 0.79f, 0.85f, 0.45f, 30, 0.90f, 15, 0.95f, 0.90f, 0.83f, 0.55f, 5, 5);
            AddLevel(2, (int)BonusSymb.Peach, 500, 0.90f, 0.79f, 0.85f, 0.45f, 40, 0.90f, 20, 0.95f, 0.95f, 0.83f, 0.55f, 4, 5);
            AddLevel(3, (int)BonusSymb.Peach, 500, 0.90f, 0.79f, 0.85f, 0.45f, 40, 0.90f, 20, 0.95f, 0.95f, 0.83f, 0.55f, 4, 5);
            AddLevel(4, (int)BonusSymb.Apple, 700, 1.0f, 0.87f, 0.95f, 0.5f, 40, 1.0f, 20, 1.05f, 1.0f, 0.87f, 0.6f, 4, 5);
            AddLevel(5, (int)BonusSymb.Apple, 700, 1.0f, 0.87f, 0.95f, 0.5f, 50, 1.0f, 25, 1.05f, 1.0f, 0.87f, 0.6f, 4, 5);
            AddLevel(6, (int)BonusSymb.Grapes, 1000, 1.0f, 0.87f, 0.95f, 0.5f, 50, 1.0f, 25, 1.05f, 1.0f, 0.87f, 0.6f, 4, 5);
            AddLevel(7, (int)BonusSymb.Grapes, 1000, 1.0f, 0.87f, 0.95f, 0.5f, 50, 1.0f, 25, 1.05f, 1.0f, 0.87f, 0.6f, 4, 5);
            AddLevel(8, (int)BonusSymb.Galaxian, 2000, 1.0f, 0.87f, 0.95f, 0.5f, 60, 1.0f, 30, 1.05f, 1.0f, 0.87f, 0.6f, 4, 5);
            AddLevel(9, (int)BonusSymb.Galaxian, 2000, 1.0f, 0.87f, 0.95f, 0.5f, 60, 1.0f, 30, 1.05f, 1.0f, 0.87f, 0.6f, 4, 5);
            AddLevel(10, (int)BonusSymb.Bell, 3000, 1.0f, 0.87f, 0.95f, 0.5f, 60, 1.0f, 30, 1.05f, 1.0f, 0.87f, 0.6f, 4, 5);
            AddLevel(11, (int)BonusSymb.Bell, 3000, 1.0f, 0.87f, 0.95f, 0.5f, 80, 1.0f, 40, 1.05f, 1.0f, 0.87f, 0.6f, 4, 5);
            AddLevel(12, (int)BonusSymb.Key, 5000, 1.0f, 0.87f, 0.95f, 0.5f, 80, 1.0f, 40, 1.05f, 1.0f, 0.87f, 0.6f, 4, 5);
            AddLevel(13, (int)BonusSymb.Key, 5000, 1.0f, 0.87f, 0.95f, 0.5f, 80, 1.0f, 40, 1.05f, 1.0f, 0.87f, 0.6f, 4, 5);
            AddLevel(14, (int)BonusSymb.Key, 5000, 1.0f, 0.87f, 0.95f, 0.5f, 100, 1.0f, 50, 1.05f, 1.0f, 0.87f, 0.6f, 4, 5);
            AddLevel(15, (int)BonusSymb.Key, 5000, 1.0f, 0.87f, 0.95f, 0.5f, 100, 1.0f, 50, 1.05f, 1.0f, 0.87f, 0.6f, 4, 5);
            AddLevel(16, (int)BonusSymb.Key, 5000, 1.0f, 0.87f, 0.95f, 0.5f, 100, 1.0f, 50, 1.05f, 1.0f, 0.87f, 0.6f, 4, 5);
            AddLevel(17, (int)BonusSymb.Key, 5000, 1.0f, 0.87f, 0.95f, 0.5f, 100, 1.0f, 50, 1.05f, 1.0f, 0.87f, 0.6f, 4, 5);
            AddLevel(18, (int)BonusSymb.Key, 5000, 1.0f, 0.87f, 0.95f, 0.5f, 120, 1.0f, 60, 1.05f, 1.0f, 0.87f, 0.6f, 4, 5);
            AddLevel(19, (int)BonusSymb.Key, 5000, 1.0f, 0.87f, 0.95f, 0.5f, 120, 1.0f, 60, 1.05f, 1.0f, 0.87f, 0.6f, 4, 5);
            AddLevel(20, (int)BonusSymb.Key, 5000, 0.9f, 0.79f, 0.95f, 0.5f, 120, 1.0f, 60, 1.05f, 1.0f, 0.87f, 0.6f, 4, 5);
        }
        #endregion

        #region AddLevel
        
        private static void AddLevel(
            int key,
            int bonusSymbol,
            int bonusPoints,
            float pacmanSpeed,
            float pacmanDotsSpeed,
            float ghostSpeed,
            float ghostTunnelSpeed,
            int elroy1DotsLeft,
            float elroy1Speed,
            int elroy2DotsLeft,
            float elroy2Speed,
            float frightPacmanSpeed,
            float frightPacmanDotsSpeed,
            float frightGhostSpeed,
            int frightTimeSeconds,
            int flashes)
        {
           
            Level level = new Level();
        
            level.bonusSymbol = bonusSymbol;
            level.bonusPoints = bonusPoints;
            level.pacmanSpeed = pacmanSpeed;
            level.pacmanDotsSpeed = pacmanDotsSpeed;
            level.ghostSpeed = ghostSpeed;
            level.ghostTunnelSpeed = ghostTunnelSpeed;
            level.elroy1DotsLeft = elroy1DotsLeft;
            level.elroy1Speed = elroy1Speed;
            level.elroy2DotsLeft = elroy2DotsLeft;
            level.elroy2Speed = elroy2Speed;
            level.frightPacmanSpeed = frightPacmanSpeed;
            level.frightPacmanDotsSpeed = frightPacmanDotsSpeed;
            level.frightGhostSpeed = frightGhostSpeed;
            level.frightTimeSeconds = frightTimeSeconds;
            level.flashes = flashes;
     
            Items.LevelDB.Add(key, level);
        }

        #endregion
    }
    #endregion
}
