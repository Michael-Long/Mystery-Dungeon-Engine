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

        private DungeonMap currentFloor = null;

        // Deff more information will be needed in here, like creature/item/tile spawn lists

        public void GenerateFloor()
        {
            currentFloor = generators[Random.Range(0, generators.Count - 1)].GenerateLevel(xRadius, yRadius);

            enviroment.produceEnviroment(currentFloor.getMap());

            PlayerCreature[] players = FindObjectsOfType<PlayerCreature>();
            foreach (PlayerCreature player in players) {

                (int, int) roomLoc = getRandomRoomLocation(currentFloor.getRooms());
                player.transform.position = new Vector2(roomLoc.Item1, roomLoc.Item2);
            }

            (int, int) stairsLoc = getRandomRoomLocation(currentFloor.getRooms());
            transform.parent.gameObject.transform.position = new Vector2(stairsLoc.Item1, stairsLoc.Item2);
        }

        private (int, int) getRandomRoomLocation(List<Room> roomList) {
            Room startRoom = roomList[Random.Range(0, roomList.Count - 1)];

            int startX = startRoom.getRoomCorner().Item1 + Random.Range(0, startRoom.getRoomSpace().GetLength(1));
            int startY = startRoom.getRoomCorner().Item2 + Random.Range(0, startRoom.getRoomSpace().GetLength(0));

            return (startX, startY);
        }
    }
}