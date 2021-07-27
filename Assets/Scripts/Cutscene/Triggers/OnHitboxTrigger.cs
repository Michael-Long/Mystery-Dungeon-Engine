using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Cutscene
{
    [System.Serializable]
    public class OnHitboxTrigger : CutsceneTrigger
    {
        [Tooltip("Hitbox in which the main character needs to intersect in order to trigger the cutscene.")]
        public Rect HitBox;

        private GameObject Player = null;

        public void Start()
        {
            // Prob not the final solution, but an option if we want to use tags.
            Player = GameObject.FindGameObjectWithTag("player");
        }

        // TODO :^)
        public override bool CheckTrigger()
        {
            return false;
        }
    }
}