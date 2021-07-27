using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Assets.ObjectTypes.AnimationData
{
    [System.Serializable]
    public class AnimationSequence
    {
        [Min(0)]
        [HideInInspector]
        [Tooltip("Unique Identifier for the AnimationSequence")]
        public int Index = 0;
        [HideInInspector]
        [Tooltip("Rush Point Data - Extracted from XML file")]
        public int RushPoint = 0;
        [HideInInspector]
        [Tooltip("Hit Point Data - Extracted from XML file")]
        public int HitPoint = 0;
        [HideInInspector]
        [Tooltip("Return Point Data - Extracted from XML file")]
        public int ReturnPoint = 0;
        [Tooltip("Animation Direction - Indicates which direction the creature is looking for this animation. This is mainly for reference, changing it doesn't effect the animations direction.")]
        public AnimationDirection Direction = AnimationDirection.Bottom;
        [Tooltip("The list of AnimationFrames that create this sequence. Order of Frames matter!")]
        public List<AnimationFrame> Frames = new List<AnimationFrame>();

        public AnimationSequence() { }
    }
}
