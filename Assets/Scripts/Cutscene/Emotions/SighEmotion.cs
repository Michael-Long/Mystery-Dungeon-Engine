using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Cutscene {
    [RequireComponent(typeof(SpriteRenderer), typeof(Animator), typeof(AudioSource))]
    public class SighEmotion : EmotionScript
    {
        [Tooltip("This is the Sigh Emotion.")]
        public const EmotionType Type = EmotionType.Sigh;

        private AudioSource Audio = null;
        private Animator Anime = null;

        public override void StartEmotion()
        {
            Audio = GetComponent<AudioSource>();
            Anime = GetComponent<Animator>();
            Anime.SetBool("Playing", true);
        }

        public override bool IsPlaying()
        {
            return Anime.GetBool("Playing");
        }

        public override void StopEmotion()
        {
            Anime.SetBool("Playing", false);
        }

        public override EmotionType GetEmotionType()
        {
            return Type;
        }

        public override void PlaySound()
        {
            Audio.Play();
        }
    }
}