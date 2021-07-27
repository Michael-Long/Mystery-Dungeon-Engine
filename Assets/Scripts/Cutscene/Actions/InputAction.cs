using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Cutscene
{
    [System.Serializable]
    public class InputAction : CutsceneAction
    {
        [Tooltip("Keypress to wait for to complete this action.")]
        public KeyCode waitKey = KeyCode.Space;

        public override IEnumerator DoAction()
        {
            IsActive = true;
            while (!Input.GetKeyDown(waitKey))
            {
                yield return null;
            }
            IsActive = false;
        }
    }
}
