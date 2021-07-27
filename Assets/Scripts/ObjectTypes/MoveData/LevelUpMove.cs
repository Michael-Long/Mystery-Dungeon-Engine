using UnityEngine;

namespace Assets.ObjectTypes.MoveData
{
    [System.Serializable]
    public class LevelUpMove
    {
        [Header("Level-Up Move Data")]
        [Tooltip("Move to be learned at the specified level")]
        public BaseMove move = null;
        [Tooltip("Level that the move is learned by this creature")]
        public int level = 0;

        public LevelUpMove() { }

        public LevelUpMove(int lvl)
        {
            level = lvl;
        }
    }
}