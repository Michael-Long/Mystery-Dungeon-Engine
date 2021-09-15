using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Logic.Animation;
using Assets.ObjectTypes;

namespace Assets.GameEngine.Entities
{
    public abstract class Entity : MonoBehaviour
    {
        private ActionState state = ActionState.Waiting;

        public abstract IEnumerator DoAction();
        public abstract EntityType GetEntityType();
        public abstract LegacyAnimationController GetAnimationController();

        public ActionState getActionState()
        {
            return state;
        }

        public void setActionState(ActionState actionState)
        {
            state = actionState;
        }
    }
}