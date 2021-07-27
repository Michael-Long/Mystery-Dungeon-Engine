using System.Collections;
using UnityEngine;

using Assets.Logic.Animation;
using Assets.ObjectTypes.AnimationData;

namespace Assets.Cutscene
{
    [System.Serializable]
    public class LegacyAnimationTransitionAction : CutsceneAction
    {
        [Tooltip("Which GameObject should we apply the new animation data to? It should have a Legacy Animation Component attached to it.")]
        public GameObject TargetObject = null;
        [Tooltip("The new animation to apply to the targte object.")]
        public AnimationType TargetType = AnimationType.Idle;
        [Tooltip("The new direction to apply to the target object.")]
        public AnimationDirection TargetDirection = AnimationDirection.Left;

        public override IEnumerator DoAction()
        {
            IsActive = true;
            LegacyAnimationController animation = TargetObject.GetComponent<LegacyAnimationController>();
            animation.StartNewAnimation(TargetType, TargetDirection);
            IsActive = false;
            yield return null;
        }
    }
}