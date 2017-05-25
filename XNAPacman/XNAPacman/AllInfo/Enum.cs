using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XNAPacman
{

    #region RotationEnum
    public enum RotationEnum
    {
        Nord = 270,
        East = 0,
        South = 90,
        West = 180,
    }
    #endregion

    #region GameState
    public enum GameState
    {
        End,
        Game,
        Intro,
    }
    #endregion

    #region MapType
    public enum MapType
    {
        MapEmpty,
        MapBarriier,
        MapStart,
        MapExit,
    }
    #endregion

    #region Bonuses
    public enum BonusSymb
    {
        Cherries,
        Strawberry,
        Peach,
        Apple,
        Grapes,
        Galaxian,
        Bell,
        Key,
    }
    #endregion

    #region GhostMods
    public enum GhostMods
    {
        Chase,
        Eyes,
        Frightened,
        Home,
        Respawn,
        Scatter,
    }
    #endregion

    #region Motion
    public enum Motion
    {
        up,
        down,
        left,
        right,
        nope, 
    }
    #endregion
}
