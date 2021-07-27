using System.Collections;
using UnityEngine;

using Assets.ObjectTypes.AnimationData;

namespace Assets.Cutscene
{
    public class AnimationTransitionAction : CutsceneAction
    {
        [Tooltip("The Object to apply the Target Animation to. Must be using an Animator Component.")]
        public GameObject TargetObject = null;
        [Tooltip("The direction we want the target object to face.")]
        public AnimationDirection newDirection = AnimationDirection.Left;
        [Tooltip("The Type of Animation we want the target to use.")]
        public AnimationType newType = AnimationType.Idle;

        private Animator AnimationController = null;

        public void Start()
        {
            AnimationController = TargetObject.GetComponent<Animator>();
        }

        public override IEnumerator DoAction()
        {
            // This is all temp code. A massive spider of animations need to be made in the animator, with the right triggers needed to change between clips. Too lazy to do atm,
            // Could be a good learning project for a new dev
            IsActive = true;
            AnimationController.SetInteger("Type", (int)newType);
            AnimationController.SetInteger("Direction", (int)newDirection);
            IsActive = false;
            yield return null;
        }
    }
}