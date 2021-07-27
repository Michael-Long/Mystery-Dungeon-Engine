using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Cutscene
{
    [System.Serializable]
    public class EmotionAction : CutsceneAction
    {
        [Tooltip("The emotion to apply to the referenced entity.")]
        public GameObject Emotion = null;
        [Tooltip("Referenced Entity to apply this emotion to.")]
        public GameObject Entity = null;

        public void Start()
        {
            if (Emotion == null || Emotion.GetComponent<EmotionScript>() == null)
            {
                Debug.LogError("EmotionAction " + name + " doesn't have a EmotionScript attached to their Emotion Property.");
            }
        }

        public override IEnumerator DoAction()
        {
            IsActive = true;
            GameObject Teardrop = Instantiate(Emotion);
            Teardrop.transform.SetParent(Entity.transform);

            EmotionScript EmotionController = Teardrop.GetComponent<EmotionScript>();
            EmotionController.StartEmotion();
            while (EmotionController.IsPlaying())
                yield return null;

            Destroy(Teardrop);
            IsActive = false;
        }
    }
}
