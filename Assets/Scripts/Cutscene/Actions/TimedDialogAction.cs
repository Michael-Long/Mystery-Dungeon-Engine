using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Dialog;

namespace Assets.Cutscene
{
    [System.Serializable]
    public class TimedDialogAction : CutsceneAction
    {
        [Tooltip("The dialog to display for this action.")]
        public Dialogue dialog;
        // Adjustments might need to be made for this to account till the sentence is fully displayed.
        [Tooltip("The amount of time to wait before moving on to the next sentence.")]
        public float waitTime = 0;

        public override IEnumerator DoAction()
        {
            IsActive = true;
            DialogueManager manager = FindObjectOfType<DialogueManager>();
            manager.AddDialog(dialog);
            manager.TriggerDialog();
            float timed = waitTime;
            while (manager.ProcessingDialog())
            {
                if (timed <= 0)
                {
                    manager.TriggerDialog();
                    timed = waitTime;
                }
                else
                {
                    timed -= Time.deltaTime;
                }
                yield return null;
            }
            IsActive = false;
        }
    }
}
