using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.ObjectTypes.AnimationData;

namespace Assets.Logic.Animation {
    public class LegacyAnimationController : MonoBehaviour
    {
        [Tooltip("The Legacy Animation File to reference from. These can be created by creating a Legacy Animation Asset")]
        public LegacyAnimation AnimationData;
        [Tooltip("Animation Type - Which type of animation do you want to play?")]
        public AnimationType m_AnimationType = AnimationType.None;
        [Tooltip("Direction - Which Direction do you want the Sprite to face?")]
        public AnimationDirection m_AnimationDirection = AnimationDirection.Top;

        private const float frameRate = 1f / 60f; // Legacy Animation's "Duration" is measured at 60FPS

        private Vector2 posOffset;
        private int durationCount = 0;
        private SpriteRenderer render;
        private Coroutine AnimationCycle = null;

        private AnimationGroup selectedGroup;
        private AnimationSequence selectedSequence;
        private AnimationFrame currFrameData;
        private int currFrameIndex = 0;
        private int nextFrameIndex = 0;

        private float durationSpeedScale = 1f;

        public void Start()
        {
            render = GetComponent<SpriteRenderer>();
            selectedGroup = AnimationData.AnimationGroups[(int)m_AnimationType].group;
            selectedSequence = selectedGroup.AnimationSequenceList[(int)m_AnimationDirection];
            posOffset = new Vector2(0, 0);

            AnimationCycle = StartCoroutine(UpdateAnimation());
        }

        public IEnumerator UpdateAnimation()
        {
            while (true)
            {
                selectNextFrame();
                var time = frameRate * (durationCount * durationSpeedScale);
                yield return new WaitForSeconds(time);
            }
        }

        public void StartNewAnimation(AnimationType newType, AnimationDirection newDirection)
        {
            if (newType == m_AnimationType && newDirection == m_AnimationDirection)
                return;
            StopCoroutine(AnimationCycle);
            nextFrameIndex = 0;
            m_AnimationType = newType;
            m_AnimationDirection = newDirection;
            selectedGroup = AnimationData.AnimationGroups[(int)newType].group;
            selectedSequence = selectedGroup.AnimationSequenceList[(int)newDirection];
            AnimationCycle = StartCoroutine(UpdateAnimation());
        }

        public void StartNewAnimation(AnimationDirection newDirection)
        {
            StopCoroutine(AnimationCycle);
            nextFrameIndex = 0;
            selectedSequence = selectedGroup.AnimationSequenceList[(int)newDirection];
            AnimationCycle = StartCoroutine(UpdateAnimation());
        }
        
        public void StartAnimation()
        {
            AnimationCycle = StartCoroutine(UpdateAnimation());
        }

        public void SetDurationSpeedScale(float scale)
        {
            durationSpeedScale = scale;
        }

        public void StopAnimation()
        {
            StopCoroutine(AnimationCycle);
            nextFrameIndex = 0;
        }

        private void selectNextFrame()
        {
            currFrameIndex = nextFrameIndex;
            currFrameData = selectedSequence.Frames[currFrameIndex];
            durationCount = currFrameData.Duration;

            render.sprite = currFrameData.FrameSprite;
            render.flipX = currFrameData.HorizFlip;

            float newXOffset = currFrameData.spriteXPos - posOffset.x;
            float newYOffset = currFrameData.spriteYPos - posOffset.y;
            posOffset.x = currFrameData.spriteXPos;
            posOffset.y = currFrameData.spriteYPos;

            Vector2 newPos = transform.position;
            newPos.x += newXOffset / render.sprite.pixelsPerUnit;
            newPos.y += newYOffset / render.sprite.pixelsPerUnit;
            transform.position = newPos;

            nextFrameIndex = (currFrameIndex + 1) % selectedSequence.Frames.Count;
        }
    }
}