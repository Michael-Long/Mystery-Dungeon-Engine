using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Cutscene
{
    [System.Serializable]
    public class MovementAction : CutsceneAction
    {
        [Tooltip("Reference to entity to apply movement to.")]
        public GameObject entity;
        [Tooltip("Location to move entity to.")]
        public Vector2 endLocation;
        [Tooltip("Speed of which to move the entity towards the end location.")]
        public float speed;

        public override IEnumerator DoAction()
        {
            IsActive = true;
            while (entity.transform.position.x != endLocation.x || entity.transform.position.y != endLocation.y)
            {
                // Works, but should be smarter in detecting if the animation needs to change.
                entity.transform.position = Vector2.MoveTowards(entity.transform.position, endLocation, speed * Time.deltaTime);
                yield return null;
            }
            IsActive = false;
        }
    }
}
