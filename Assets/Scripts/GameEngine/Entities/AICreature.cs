using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.GameEngine.Entities.AI;
using Assets.Logic.Animation;
using Assets.ObjectTypes;

namespace Assets.GameEngine.Entities
{
    public class AICreature : Creature
    {
        [Header("AI Information")]
        [Tooltip("What AI Logic should this Creature Use on their turn?")]
        public BaseAI AI = null;
        [Tooltip("Which Entity Group should this Creature be put into?")]
        public EntityType entityType = EntityType.Teammate;

        // Creature take their turn during the movement turn, or should wait to do a different action?
        private bool doMovementAction = false;
        private LegacyAnimationController AnimationController = null;
        private bool isRunning = false;

        public void Start()
        {
            AnimationController = gameObject.GetComponentInChildren<LegacyAnimationController>();
        }

        public override IEnumerator DoAction()
        {
            SetActionState(false);
            do
            {
                if (AI && !AI.IsProcessing())
                {
                    if (doMovementAction)
                        StartCoroutine(AI.AIMovement(isRunning));
                    else
                        StartCoroutine(AI.AIAction());
                }
                yield return null;
            } while (AI.IsProcessing());
            SetActionState(true);
        }

        public override EntityType GetEntityType()
        {
            return entityType;
        }

        public override LegacyAnimationController GetAnimationController()
        {
            return AnimationController;
        }

        public override void setRunning(bool isRunning)
        {
            this.isRunning = isRunning;
        }

        public void DetermineActionState()
        {
            doMovementAction = true;
            if (AI)
                doMovementAction = AI.shouldDoMovement();
        }

        public bool IsMovementAction()
        {
            return doMovementAction;
        }
    }
}