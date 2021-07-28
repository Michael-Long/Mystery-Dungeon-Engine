using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Logic.Animation;
using Assets.GameEngine.DungeonEngine;
using Assets.Player;
using Assets.ObjectTypes;
using Assets.ObjectTypes.AnimationData;
using Assets.ObjectTypes.ItemData;
using Assets.ObjectTypes.MoveData;
using Assets.ObjectTypes.Creature;

namespace Assets.GameEngine.Entities
{
    public class PlayerCreature : Creature
    {
        //--- PRIVATE FIELDS ---
        private bool isMoving = false;
        private bool isRunning = false;
        private PlayerControls Controls = null;
        private LegacyAnimationController AnimationController = null;

        public PlayerCreature() { }

        public PlayerCreature(CreatureData newData)
        {
            data = newData;
        }

        public void Start()
        {
            Controls = PlayerControls.getInstance();
            AnimationController = GetComponentInChildren<LegacyAnimationController>();
        }

        public override EntityType GetEntityType()
        {
            return EntityType.Player;
        }

        public override LegacyAnimationController GetAnimationController()
        {
            return AnimationController;
        }

        public override IEnumerator DoAction()
        {
            SetActionState(false);
            while (!IsActionComplete())
            {
                while (!Input.anyKey)
                {
                    yield return null;
                }

                List<KeyCode> PressedKeys = Controls.GetPressedPlayerControls();

                if (Controls.IsMovementControl(PressedKeys))
                    DoMovementAction(PressedKeys);

                yield return null;
            }
        }

        public override void setRunning(bool isRunning)
        {
            this.isRunning = isRunning;
        }

        private void DoMovementAction(List<KeyCode> PressedKeys)
        {
            float xMove = 0f;
            float yMove = 0f;
            if (PressedKeys.Contains(Controls.UpDirection))
                yMove = 1f;
            else if (PressedKeys.Contains(Controls.DownDirection))
                yMove = -1f;
            if (PressedKeys.Contains(Controls.LeftDirection))
                xMove = -1f;
            else if (PressedKeys.Contains(Controls.RightDirection))
                xMove = 1f;

            if ((xMove != 0 || yMove != 0) && !isMoving)
            {
                Vector2 targetPos = new Vector2(transform.position.x, transform.position.y);
                if (xMove != 0)
                {
                    targetPos.x = transform.position.x + xMove;
                }
                if (yMove != 0)
                {
                    targetPos.y = transform.position.y + yMove;
                }

                if (VerifyMovement.CanMove(this, targetPos))
                {
                    StartCoroutine(MovePlayer(targetPos));
                } 
                else
                {
                    AnimationController.StartNewAnimation(AnimationType.Idle, AnimationHelper.convertMovementToDirection(transform.position, targetPos));
                }
            }
        }

        private IEnumerator MovePlayer(Vector2 targetPos)
        {
            isMoving = true;
            AnimationDirection direction = AnimationHelper.convertMovementToDirection(transform.position, targetPos);
            AnimationController.StartNewAnimation(AnimationType.Walk, direction);
            GameObject blockMovement = new GameObject("PlayerBlocker");
            blockMovement.transform.position = targetPos;
            BoxCollider2D collison = blockMovement.AddComponent<BoxCollider2D>();
            collison.size = GetComponent<BoxCollider2D>().size;
            blockMovement.layer = gameObject.layer;

            float RunningMultiplier = 1f;
            if (isRunning)
            {
                RunningMultiplier = 3f;
            }

            SetActionState(true);

            while (Vector2.Distance(transform.position, targetPos) > 0.01f)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPos, 3 * RunningMultiplier * Time.deltaTime);
                yield return null;
            }
            transform.position = targetPos;
            isMoving = false;
            if (Controls.GetPressedPlayerControls().Count == 0)
                AnimationController.StartNewAnimation(AnimationType.Idle, direction);
            Destroy(blockMovement);
        }
    }
}