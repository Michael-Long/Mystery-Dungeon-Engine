using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.ObjectTypes.AnimationData;
using Assets.Logic.Animation;

namespace Assets.Cutscene
{
    public class LegacyAnimationRotateAction : CutsceneAction
    {
        [Tooltip("The object to rotate. Must be using a LegacyAnimationController.")]
        public GameObject TargetObject = null;
        [Tooltip("The direction in which we're rotating to")]
        public AnimationDirection TargetDirection = AnimationDirection.Left;
        [Tooltip("Should we rotate Clockwise?")]
        public bool Clockwise = true;

        private List<AnimationDirection> Rotation = new List<AnimationDirection>{ AnimationDirection.Left, AnimationDirection.TopLeft, AnimationDirection.Top, AnimationDirection.TopRight,
                                                AnimationDirection.Right, AnimationDirection.BottomRight, AnimationDirection.Bottom, AnimationDirection.BottomLeft };
        private LegacyAnimationController Controller = null;
        private int currIndex = 0;
        private int endIndex = 0;
        private float frameRate = 0.1f;

        public void Start()
        {
            Controller = TargetObject.GetComponent<LegacyAnimationController>();
            currIndex = Rotation.FindIndex((AnimationDirection direct) => { return direct == Controller.m_AnimationDirection; });
            endIndex = Rotation.FindIndex((AnimationDirection direct) => { return direct == TargetDirection; });
        }

        public override IEnumerator DoAction()
        {
            IsActive = true;
            int sign = Clockwise ? 1 : -1;
            float timeUntilNextRotate = frameRate;
            do
            {
                timeUntilNextRotate -= Time.deltaTime;
                if (timeUntilNextRotate <= 0)
                {
                    currIndex = (currIndex + 1 * sign) % Rotation.Count;
                    if (currIndex == -1)
                        currIndex = Rotation.Count - 1;
                    Controller.StartNewAnimation(Rotation[currIndex]);
                    timeUntilNextRotate += frameRate;
                }
                yield return null;
            } while (currIndex != endIndex);
            IsActive = false;
        }
    }
}
