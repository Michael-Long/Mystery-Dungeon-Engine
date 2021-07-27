using System.Collections;
using UnityEngine;

namespace Assets.Cutscene
{
    public abstract class EmotionScript : MonoBehaviour
    {
        public abstract void StartEmotion();
        public abstract EmotionType GetEmotionType();
        public abstract void PlaySound();
        public abstract void StopEmotion();
        public abstract bool IsPlaying();
    }
}