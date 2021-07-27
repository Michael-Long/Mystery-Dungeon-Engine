using UnityEngine;

namespace Assets.ObjectTypes.ItemData
{
    public enum ItemType
    {
        Edible,
        Orb,
        TM,
        Throwing,
        Treasure,
        Given,
        InBag,
        Exclusive
    }

    [CreateAssetMenu(fileName = "New Item", menuName = "Toolkit/Item", order = 3)]
    public class ItemData : ScriptableObject
    {
        [Header("Basic Data")]
        [Tooltip("Unique ID for this particular item")]
        public int id = -1;
        [Tooltip("Defines what kind of item this is")]
        public ItemType itemType = ItemType.Edible;
        [Tooltip("The name of the item")]
        public string itemName = "";
        [Tooltip("The description of the item")]
        public string description = "";

        [Header("Execute Item")]
        [Tooltip("The script that should run when this item is used")]
        public ScriptableObject itemScript = null;

        public ItemData() { }

        public ItemData(int id, ItemType type, string name)
        {
            this.id = id;
            itemType = type;
            itemName = name;
        }

        // Item is lost upon use
        public virtual bool isConsumedOnUse()
        {
            switch (itemType)
            {
                case ItemType.Edible:
                case ItemType.Orb:
                case ItemType.TM:
                case ItemType.Throwing:
                case ItemType.Treasure:
                    return true;
                default:
                    return false;
            }
        }

        // Is Usable inside dungeon
        public virtual bool isUsable()
        {
            switch (itemType)
            {
                case ItemType.Edible:
                case ItemType.Orb:
                case ItemType.TM:
                case ItemType.Throwing:
                    return true;
                default:
                    return false;
            }
        }

        // Multiple items can be stack
        public virtual bool isStackable()
        {
            return itemType == ItemType.Throwing;
        }
    }
}