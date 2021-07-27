using UnityEngine;

namespace Assets.ObjectTypes.MoveData
{
    [System.Serializable]
    public class BaseMoveText
    {
        public string moveName = "";
        public string moveCategory = "";

        public BaseMoveText() { }

        public BaseMoveText(string newName, string newCategory)
        {
            moveName = newName;
            moveCategory = newCategory;
        }
    }
}