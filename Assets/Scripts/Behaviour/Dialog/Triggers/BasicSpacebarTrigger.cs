using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Dialog
{
    public class BasicSpacebarTrigger : MonoBehaviour
    {

        public Dialogue dialog;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                FindObjectOfType<DialogueManager>().AddDialog(dialog);
                FindObjectOfType<DialogueManager>().TriggerDialog();
            }
        }
    }
}
