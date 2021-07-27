using System.Collections;
using UnityEngine;

namespace Assets.Cutscene
{
    public class DestroyObjectAction : CutsceneAction
    {
        [Tooltip("Reference to the object that'll be destroyed.")]
        public GameObject ReferenceObject = null;
        [Tooltip("(Optional) If no object is specified, it'll search for an object by this given name. Useful for getting objects made by SpawnSpriteAction.")]
        public string GameObjectName = "";

        public override IEnumerator DoAction()
        {
            IsActive = true;
            if (ReferenceObject)
                Destroy(ReferenceObject);
            else
                Destroy(GameObject.Find(GameObjectName));
            IsActive = false;
            yield return null;
        }

    }
}