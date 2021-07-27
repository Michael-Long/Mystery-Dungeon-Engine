using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.ObjectTypes.AnimationData;

namespace Assets.Cutscene
{
    public class AnimationRotationAction : CutsceneAction
    {
        [Tooltip("The object to rotate. Must be using a LegacyAnimationController.")]
        public GameObject TargetObject = null;
        [Tooltip("The direction in which we're rotating to")]
        public AnimationDirection TargetDirection = AnimationDirection.Left;
        [Tooltip("Should we rotate Clockwise?")]
        public bool Clockwise = true;

        private List<AnimationDirection> Rotation = new List<AnimationDirection>{ AnimationDirection.Left, AnimationDirection.TopLeft, AnimationDirection.Top, AnimationDirection.TopRight,
                                                AnimationDirection.Right, AnimationDirection.BottomRight, AnimationDirection.Bottom, AnimationDirection.BottomLeft };
        private AnimationRotation animRotate = null;
        private Animator animator = null;
        private SpriteRenderer render = null;
        private int currIndex = 0;
        private int endIndex = 0;
        private float frameRate = 0.1f;

        public void Start()
        {
            animRotate = TargetObject.GetComponent<AnimationRotation>();
            animator = TargetObject.GetComponent<Animator>();
            render = TargetObject.GetComponent<SpriteRenderer>();
            currIndex = Rotation.FindIndex((AnimationDirection direct) => { return direct == animRotate.GetCurrentDirection(); });
            endIndex = Rotation.FindIndex((AnimationDirection direct) => { return direct == TargetDirection; });
        }

        public override IEnumerator DoAction()
        {
            IsActive = true;
            int sign = Clockwise ? 1 : -1;
            while (currIndex != endIndex)
            {
                currIndex = (currIndex + 1 * sign) % Rotation.Count;
                if (currIndex == -1)
                    currIndex = Rotation.Count - 1;
                animRotate.SetCurrentDirection(Rotation[currIndex]);

                UnityAnimationInfo currInfo = animRotate.GetCurrentDirectionInfo();
                render.sprite = currInfo.Sprite;
                render.flipX = currInfo.FlipX;
                render.flipY = currInfo.FlipY;

                yield return new WaitForSecondsRealtime(frameRate);
            } 
            IsActive = false;
        }
    }
}