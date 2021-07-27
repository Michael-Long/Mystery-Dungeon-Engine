using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.ObjectTypes.AnimationData
{
    [System.Serializable]
    public class AnimationGroup
    {
        [Tooltip("This is the type of animation this group refers to")]
        public AnimationType Type = AnimationType.None;
        [HideInInspector]
        [Tooltip("List of index values to point to an AnimationSequence. Mainly used in importing XML files")]
        public List<int> AnimationSequenceIndexList = new List<int>();
        [Tooltip("This is the AnimationSequence to reference for each of the 8 directions for this AnimationType")]
        public List<AnimationSequence> AnimationSequenceList = new List<AnimationSequence>();

        public AnimationGroup() { }
    }
}
