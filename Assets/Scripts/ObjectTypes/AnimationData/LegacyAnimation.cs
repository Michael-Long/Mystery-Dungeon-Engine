using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.ObjectTypes.AnimationData
{
    [System.Serializable]
    public struct TypedAnimationGroup
    {
        [HideInInspector]
        public AnimationType type;
        [Tooltip("Collection of Animation Sequences for this type of animation")]
        public AnimationGroup group;

        public TypedAnimationGroup(AnimationType type, AnimationGroup group)
        {
            this.type = type;
            this.group = group;
        }
    }

    [System.Serializable]
    public class LegacyAnimation : ScriptableObject
    {
        [Tooltip("Name of the animation. Useful for giving this animation a label. No Functional Importance.")]
        public string Name = "";
        [HideInInspector]
        [Tooltip("Key value. Used mainly during import, to help tie this animation to a resource folder to load in the proper sprites. Otherwise unused.")]
        public string Key = "";
        [Min(0)]
        [Tooltip("Frame Width - The amount of pixels the width of this animation is")]
        public int FrameWidth = 0;
        [Min(0)]
        [Tooltip("Frame Height - The amount of pixels the height of this animation is")]
        public int FrameHeight = 0;
        [Tooltip("AnimationGroups for each AnimationType.")]
        public List<TypedAnimationGroup> AnimationGroups = new List<TypedAnimationGroup>();

        // The following is for communication for the Inspector
        [HideInInspector]
        public int selectedGroup = 0;
        [HideInInspector]
        public int selectedDirection = 0;

        public void initializeLists()
        {
            foreach (string type in Enum.GetNames(typeof(AnimationType)))
            {
                AnimationGroup newGroup = new AnimationGroup();
                foreach (string direction in Enum.GetNames(typeof(AnimationDirection)))
                {
                    newGroup.AnimationSequenceList.Add(new AnimationSequence());
                }

                AnimationGroups.Add(new TypedAnimationGroup((AnimationType)Enum.Parse(typeof(AnimationType), type), newGroup));
            }
        }
    }
}
