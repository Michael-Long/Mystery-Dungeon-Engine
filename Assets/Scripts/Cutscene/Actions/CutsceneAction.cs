using System.Collections;
using UnityEngine;

namespace Assets.Cutscene
{
    // Interface in which a Cutscene Action needs to implement, as the DoAction() method is what is run during the cutscene's progression.
    public abstract class CutsceneAction : MonoBehaviour
    {
        protected bool IsActive = false;

        public abstract IEnumerator DoAction();

        public bool IsActionActive() { return IsActive; }
    }
}
