using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Logic.Animation;
using Assets.ObjectTypes;

namespace Assets.GameEngine.Entities
{
    public abstract class Entity : MonoBehaviour
    {
        private bool actionComplete = false;

        public abstract IEnumerator DoAction();
        public abstract EntityType GetEntityType();
        public abstract LegacyAnimationController GetAnimationController();

        public bool IsActionComplete()
        {
            return actionComplete;
        }

        public void SetActionState(bool actionState)
        {
            actionComplete = actionState;
        }
    }
}