using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Dialog
{
    public class PortiatAnimator : MonoBehaviour
    {
        // Is Animation Active?
        private bool Enabled = false;
        // Spritesheet to animate thru
        private List<Sprite> SpriteFrames = new List<Sprite>();
        // Loop the animation till disabled?
        private bool Loop = false;
        // How long should one cycle of the animation last? In Seconds.
        private float Length = 0;
        // How long to delay the animation once actived. In Seconds;
        private float Delay = 0;
        // A sound to play during the animation. This sound will play once 1/2 of the animation has played.
        private AudioSource AnimationSound = null;

        // Portiat SpriteRenderer
        private Image portiat = null;

        private bool IsAnimating = false;

        public void Start()
        {
            portiat = GetComponent<Image>();
        }

        public void StartAnimation()
        {
            if (!Enabled && SpriteFrames.Count > 0 && Length > 0)
            {
                Enabled = true;
            }
        }

        public void SetParams(List<Sprite> sheet, float Length, float Delay, bool Loop, AudioSource AnimationSound = null)
        {
            SpriteFrames = sheet;
            this.Length = Length;
            this.Delay = Delay;
            this.Loop = Loop;
            this.AnimationSound = AnimationSound;
        }

        public void StopAnimation()
        {
            Enabled = false;
            SpriteFrames.Clear();
            Length = 0;
            Loop = false;
            IsAnimating = false;
            StopAllCoroutines();
        }

        public void Update()
        {
            if (Enabled && !IsAnimating)
            {
                StartCoroutine(DoAnimation());
                if (!Loop)
                    Enabled = false;
            }
        }

        public IEnumerator DoAnimation()
        {
            float perFrame = Length / SpriteFrames.Count;
            int soundTime = SpriteFrames.Count / 2;
            int count = 0;
            IsAnimating = true;
            yield return new WaitForSecondsRealtime(Delay);
            foreach (Sprite frame in SpriteFrames)
            {
                portiat.sprite = frame;
                if (count == soundTime && AnimationSound)
                    AnimationSound.Play();
                count++;
                yield return new WaitForSecondsRealtime(perFrame);
            }
            IsAnimating = false;
        }
    }
}