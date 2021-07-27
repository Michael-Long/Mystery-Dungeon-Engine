using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Cutscene
{
    public class SpawnSpriteAction : CutsceneAction
    {
        [Tooltip("Name of the newly spawned object.")]
        public string ObjectName = "";
        [Tooltip("The Sprite to give to the newly created object.")]
        public Sprite SpawnedSprite = null;
        [Tooltip("The location in which to spawn the sprite.")]
        public Vector2 SpawnPosition = new Vector2(0, 0);

        public override IEnumerator DoAction()
        {
            IsActive = true;
            GameObject spawnedItem = new GameObject(ObjectName);
            spawnedItem.layer = LayerMask.NameToLayer("Items");
            spawnedItem.transform.position = SpawnPosition;
            var spriteRender = spawnedItem.AddComponent<SpriteRenderer>();
            spriteRender.enabled = false;
            spriteRender.sprite = SpawnedSprite;
            spriteRender.sortingLayerName = "Entity";
            spriteRender.enabled = true;
            IsActive = false;
            yield return null;
        }
    }
}