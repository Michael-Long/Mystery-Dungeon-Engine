using UnityEngine;

namespace Assets.Cutscene
{
    // A trigger needs to implement this interface, where the CheckTrigger function is where the check occurs on if we should start the cutscene or not.
    public abstract class CutsceneTrigger : MonoBehaviour
    {
        public abstract bool CheckTrigger();

    }
}