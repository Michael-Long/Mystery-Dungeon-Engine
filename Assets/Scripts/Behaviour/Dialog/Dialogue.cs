using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Dialog
{
    [System.Serializable]
    public class Dialogue
    {
        [Tooltip("Name of character speaking.")]
        public string SpeakerName;
        [Tooltip("Color of the Speaker's name in the Dialog.")]
        public Color32 TalkerColor = new Color32(255, 255, 0, 255);
        [Tooltip("The Portiat in which to display in the Dialog Box")]
        public Sprite TalkerPortiat;
        [Tooltip("Sentences the speaker will say. Will be used if no TextAsset is defined.")]
        [TextArea(3, 5)]
        public string[] Sentences;
        [Tooltip("Text File containing the speaker's lines. Reading from the file will be prioritized over the sentences in the above array.")]
        public TextAsset ImportSentences;

        [Header("Animatred Portiat (Optional)")]
        [Tooltip("When should the portiat animate? Set to none if to never animate.")]
        public PortiatAnimateTrigger animateTrigger = PortiatAnimateTrigger.None;
        [Tooltip("The spritesheet in which to animate over.")]
        public List<Sprite> SpriteFrames = new List<Sprite>();
        [Tooltip("How long should one cycle of the animation last?")]
        public float Duration = 0;
        [Tooltip("How long should the animation be delayed for? (In Seconds)")]
        public float Delay = 0;
        [Tooltip("Should the animation loop till the dialog is complete?")]
        public bool Loop = false;
        [Tooltip("Sound to play during the animation")]
        public AudioSource AnimationSound = null;
    }
}
