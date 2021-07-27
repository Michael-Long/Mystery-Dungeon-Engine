using System.Collections.Generic;
using UnityEngine;

namespace Assets.ObjectTypes.AnimationData
{
    public class AnimationRotation : MonoBehaviour
    {
        public int CurrentDirection = 0;
        public List<UnityAnimationInfo> Directions = new List<UnityAnimationInfo>(8);


        public void SetCurrentDirection(AnimationDirection newDirection)
        {
            CurrentDirection = Directions.FindIndex((UnityAnimationInfo info) => { return info.Direction == newDirection; });
        }

        public void SetCurrentDirection(int newDirectionIndex)
        {
            CurrentDirection = newDirectionIndex;
        }

        public UnityAnimationInfo GetCurrentDirectionInfo()
        {
            return Directions[CurrentDirection];
        }

        public AnimationDirection GetCurrentDirection()
        {
            return Directions[CurrentDirection].Direction;
        }
    }
}