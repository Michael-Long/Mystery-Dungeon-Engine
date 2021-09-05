using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.GameEngine.DungeonEngine.DungeonGen.Generators;
using Assets.GameEngine.DungeonEngine.DungeonGen.Enviroment;
using Assets.GameEngine.Entities;

namespace Assets.GameEngine.DungeonEngine.DungeonGen
{
    public class DungeonLevel : MonoBehaviour
    {

        [Tooltip("What is the horizontial bounds of this dungeon level?")]
        [Min(10)]
        public int xRadius = 10;
        [Tooltip("What is the vertical bounds of this dungeon level?")]
        [Min(10)]
        public int yRadius = 10;
        [Tooltip("Which enviroment controller is this dungeon using?")]
        public EnviromentController enviroment;
        [Tooltip("What are the different generators that can be used to generate the levels of this dungeon")]
        public List<GeneratorInterface> generators;

        // Deff more information will be needed in here, like creature/item/tile spawn lists

        public void GenerateFloor()
        {
            DungeonMap dungeonMap = generators[Random.Range(0, generators.Count - 1)].GenerateLevel(xRadius, yRadius);

            enviroment.produceEnviroment(dungeonMap.getMap());

            PlayerCreature[] players = FindObjectsOfType<PlayerCreature>();
            foreach (PlayerCreature player in players) {
                Room startRoom = dungeonMap.getRooms()[Random.Range(0, dungeonMap.getRooms().Count - 1)];

                int startX = startRoom.getRoomCorner().Item1 + Random.Range(0, startRoom.getRoomSpace().GetLength(1));
                int startY = startRoom.getRoomCorner().Item2 + Random.Range(0, startRoom.getRoomSpace().GetLength(0));

                player.transform.position = new Vector2(startX, startY);
            }
        }
    }
}