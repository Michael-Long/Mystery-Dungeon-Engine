using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Cutscene
{
    [System.Serializable]
    public class SoundAction : CutsceneAction
    {
        [Tooltip("Sound in which to play")]
        public AudioSource sound;
        [Tooltip("Should this sound loop till the given key is pressed?")]
        public bool loop = false;
        [Tooltip("The Key to wait on to end the loop")]
        public KeyCode endKey = KeyCode.Space;

        public override IEnumerator DoAction()
        {
            IsActive = true;
            if (!sound.isPlaying)
                sound.Play();
            if (loop)
            {
                yield return null;
                while (!Input.GetKeyDown(endKey))
                {
                    if (!sound.isPlaying)
                        sound.Play();
                    yield return null;
                }
            }
            IsActive = false;
        }
    }
}