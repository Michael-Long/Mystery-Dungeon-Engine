using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Cutscene
{
    [System.Serializable]
    public class OnLoadTrigger : CutsceneTrigger
    {
        // Honestly just used to start a cutscene in a scene. May not be needed entirely.
        public override bool CheckTrigger()
        {
            return true;
        }
    }
}