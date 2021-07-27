using UnityEngine;

namespace Assets.Cutscene
{
    public class FadeInController : MonoBehaviour
    {

        public Animator animator;

        private bool fadeActive = false;

        public void DoFade()
        {
            fadeActive = true;
            animator.SetTrigger("TriggerFade");
        }

        public bool IsFadeActive()
        {
            return fadeActive; 
        }

        public void ToggleFadeActive()
        {
            fadeActive = !fadeActive;
        }
    }
}
