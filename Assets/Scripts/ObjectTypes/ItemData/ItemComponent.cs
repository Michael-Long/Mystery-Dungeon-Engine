using UnityEngine;

namespace Assets.ObjectTypes.ItemData
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class ItemComponent : MonoBehaviour
    {
        [Header("Main Data")]
        [Tooltip("Reference to static item data")]
        public ItemData itemData;

        [Header("Dungeon Data")]
        [Tooltip("Is the item sticky? Making it unusable in dungeons.")]
        public bool isSticky = false;

        public ItemComponent() { }

        public ItemComponent(ItemData item)
        {
            itemData = item;
        }
    }
}