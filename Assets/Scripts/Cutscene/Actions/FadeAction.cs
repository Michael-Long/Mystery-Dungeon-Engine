using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Cutscene
{
    [System.Serializable]
    public class FadeAction : CutsceneAction
    {
        [Tooltip("Canvas which contains the UI components and animations for fade.")]
        public Canvas fadeUI = null;

        private FadeInController fadeController = null;

        public void Start()
        {
            fadeController = fadeUI.GetComponent<FadeInController>();
        }

        public override IEnumerator DoAction()
        {
            IsActive = true;

            fadeController.DoFade();

            while (fadeController.IsFadeActive())
            {
                yield return null;
            }

            IsActive = false;
            yield return null;
        }
    }
}
