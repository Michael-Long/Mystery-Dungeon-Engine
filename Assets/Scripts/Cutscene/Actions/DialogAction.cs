using System.Collections;
using UnityEngine;

using Assets.Dialog;

namespace Assets.Cutscene
{
    [System.Serializable]
    public class DialogAction : CutsceneAction
    {
        [Tooltip("Dialog to display for this action.")]
        public Dialogue dialog;
        [Tooltip("Keypress to wait for to advance the dialog.")]
        public KeyCode nextSentence = KeyCode.Space;

        private DialogueManager manager = null;

        public void Start()
        {
            manager = FindObjectOfType<DialogueManager>();
        }

        public override IEnumerator DoAction()
        {
            IsActive = true;
            manager.AddDialog(dialog);
            if (!manager.ProcessingDialog())
            {
                manager.TriggerDialog();
                while (manager.ProcessingDialog())
                {
                    if (Input.GetKeyDown(nextSentence))
                    {
                        manager.TriggerDialog();
                    }
                    yield return null;
                }
            }
            IsActive = false;
        }
    }
}
