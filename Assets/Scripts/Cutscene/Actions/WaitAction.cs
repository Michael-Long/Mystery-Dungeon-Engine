using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Cutscene {
    // Basically a blank action that'll tell the CutsceneManager to wait for all currently running Actions to complete before moving onto the next one.
    // Makes more sense to have that logic inside the CutsceneManager itself rather than in another process waiting on other processes to finish...
    // Could be wrong though, open to refactoring if deemed neccessary.
    [System.Serializable]
    public class WaitAction : CutsceneAction
    {
        public override IEnumerator DoAction()
        {
            yield return null;
        }
    }
}