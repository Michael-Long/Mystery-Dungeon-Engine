using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.GameEngine.DungeonEngine.DungeonGen.Enviroment;

namespace Assets.GameEngine.DungeonEngine.DungeonGen
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class Dungeon : MonoBehaviour
    {
        [Tooltip("What is the name of this dungeon?")]
        public string dungeonName = "New Dungeon";
        [Tooltip("Does this dungeon go upwards?")]
        public bool isUpwards = false;
        [Tooltip("A list of all the dungeon levels within this dungeon. We need at least 1 floor to be able to enter this dungeon.")]
        public List<DungeonLevel> dungeonLevels;
        // Add a list of restrictions for this dungeon

        private int currentFloor = 0;

        public void enterDungeon()
        {
            currentFloor = 0;
            dungeonLevels[currentFloor].GenerateFloor();
        }

        public void goToNextFloor()
        {
            ++currentFloor;
            dungeonLevels[currentFloor].GenerateFloor();
        }
    }
}
