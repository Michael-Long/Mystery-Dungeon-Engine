using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.GameEngine.DungeonEngine;
using Assets.Logic.Animation;
using Assets.ObjectTypes.AnimationData;

namespace Assets.GameEngine.Entities.AI
{
    public class CircleAI : BaseAI
    {
        private short position = 0;
        private bool isMoving = false;
        private LegacyAnimationController AnimationController = null;

        public void Start()
        {
            AnimationController = gameObject.GetComponentInChildren<LegacyAnimationController>();
        }

        public override bool shouldDoMovement()
        {
            // This is just a movement test, so we'll always do the movement action.
            return true;
        }

        public override IEnumerator AIAction()
        {
            yield return null;
        }

        public override IEnumerator AIMovement(bool isRunning)
        {
            isProcessing = true;
            Vector2 targetPos = gameObject.transform.position;
            Vector2 hitboxPos = targetPos;
            bool canMove = false; 
            for (short index = 0; index < 4; ++index)
            {
                targetPos = gameObject.transform.position;
                switch (position)
                {
                    case 0:
                        targetPos.x += 1f;
                        hitboxPos = targetPos;
                        position++;
                        break;
                    case 1:
                        targetPos.y += 1f;
                        hitboxPos = targetPos;
                        position++;
                        break;
                    case 2:
                        targetPos.x -= 1f;
                        hitboxPos = targetPos;
                        position++;
                        break;
                    case 3:
                        targetPos.y -= 1f;
                        hitboxPos = targetPos;
                        position = 0;
                        break;
                }
                canMove = VerifyMovement.CanMove(gameObject.GetComponent<AICreature>(), hitboxPos);
                if (canMove)
                    break;
            }
            if (canMove && !isMoving)
                StartCoroutine(Move(targetPos, isRunning));
            else
                isProcessing = false;
            yield return null;
        }

        private IEnumerator Move(Vector2 targetPos, bool isRunning)
        {
            isMoving = true;

            AnimationDirection direction = AnimationHelper.convertMovementToDirection(transform.position, targetPos);
            AnimationController.StartNewAnimation(AnimationType.Walk, direction);

            GameObject blockMovement = new GameObject("AIBlocker");
            blockMovement.transform.position = targetPos;
            BoxCollider2D collison = blockMovement.AddComponent<BoxCollider2D>();
            collison.size = GetComponent<BoxCollider2D>().size;
            blockMovement.layer = gameObject.layer;

            float RunningMultiplier = 1f;
            if (isRunning)
                RunningMultiplier = 3f;

            while (Vector2.Distance(transform.position, targetPos) > 0.01f)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPos, 3 * RunningMultiplier * Time.deltaTime);
                yield return null;
            }
            transform.position = targetPos;
            isMoving = false;
            isProcessing = false;
            AnimationController.StartNewAnimation(AnimationType.Idle, direction);
            Destroy(blockMovement);
        }
    }
}