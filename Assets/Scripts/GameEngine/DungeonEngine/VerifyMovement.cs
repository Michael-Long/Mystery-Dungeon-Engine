using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.GameEngine.Entities;
using Assets.ObjectTypes;

namespace Assets.GameEngine.DungeonEngine
{
    // This is the generic algorithm for detecting if a Creature can move into the targetted position. While this is the original basic implementation, this will eventually also
    // Check if the creature can move into the various different terrian types.
    public static class VerifyMovement
    {
        public static bool CanMove(Creature currCreature, Vector2 targetPosition)
        {
            int hitboxCheck = LayerMask.GetMask("Player", "Teammate", "Enemy", "Walls");
            return CanMove(currCreature, targetPosition, hitboxCheck);
        }

        public static bool CanMove(Creature currCreature, Vector2 targetPosition, int hitboxCheck)
        {
            Vector2 hitboxSize = Vector2.one * 0.8f;
            Collider2D hitbox = Physics2D.OverlapBox(targetPosition, hitboxSize, 0, hitboxCheck);

            Vector2 startPosition = currCreature.transform.position;
            if (startPosition.x != targetPosition.x && startPosition.y != targetPosition.y) // This is a diagonal movement, so we have to check 3 spots for walls.
            {
                return !hitbox && CanMove(currCreature, new Vector2(startPosition.x, targetPosition.y), LayerMask.GetMask("Walls")) && CanMove(currCreature, new Vector2(targetPosition.x, startPosition.y), LayerMask.GetMask("Walls"));
            }

            return !hitbox;
        }
    }
}
