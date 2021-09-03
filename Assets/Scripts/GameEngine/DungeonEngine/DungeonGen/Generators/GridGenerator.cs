using UnityEngine;

using Assets.GameEngine.DungeonEngine.DungeonGen.Generators.RoomGenerators;
using Assets.GameEngine.DungeonEngine.DungeonGen.Enviroment;

namespace Assets.GameEngine.DungeonEngine.DungeonGen.Generators
{
    [CreateAssetMenu(fileName = "GridGenerator", menuName = "ScriptableObjects/Generators/GridGenerator", order = 1)]
    public class GridGenerator : GeneratorInterface
    {
        // This will generate a dungeon level with a 5x5 grid of rooms, each of which connected to each other
        public override EnviromentType[,] GenerateLevel(int xRadius, int yRadius)
        {
            EnviromentType[,] dungeonMap = new EnviromentType[xRadius, yRadius];
            BoxRoomGenerator roomGen = new BoxRoomGenerator();
            for (int x = 0; x < xRadius; ++x)
            {
                for (int y = 0; y < yRadius; ++y)
                {
                    dungeonMap[x, y] = EnviromentType.Wall;
                }
            }
            // roomGrid splits our working map into a 5x5, with a room in each section. All adjacent rooms are connected.
            Room[,] roomMap = new Room[5, 5];
            (int, int) gridOffset = (xRadius / 5, yRadius / 5);

            for (int yIndex = 0; yIndex < 5; ++yIndex)
            {
                for (int xIndex = 0; xIndex < 5; ++xIndex)
                {
                    (int, int) xRange = (xIndex * gridOffset.Item1, xIndex * gridOffset.Item1 + gridOffset.Item1 - 1);
                    (int, int) yRange = (yIndex * gridOffset.Item2, yIndex * gridOffset.Item2 + gridOffset.Item2 - 1);

                    if (System.Math.Abs(xRange.Item1 - xRange.Item2) > 2)
                    {
                        xRange.Item1 += 1;
                        xRange.Item2 -= 1;
                    }

                    if (System.Math.Abs(yRange.Item1 - yRange.Item2) > 2)
                    {
                        yRange.Item1 += 1;
                        yRange.Item2 -= 1;
                    }

                    // Bounds don't respect the border of the map. The position of the bottomLeft corner affects the room bounds.
                    // And the minimum bounds affect the position of the bottomLeft corner
                    (int, int) minRoomBounds = (4, 4);

                    int topLeftX = Random.Range(xRange.Item1, xRange.Item2 - minRoomBounds.Item1);
                    int topLeftY = Random.Range(yRange.Item1, yRange.Item2 - minRoomBounds.Item2);

                    int roomBoundsX = Random.Range(minRoomBounds.Item1, xRange.Item2 - topLeftX);
                    int roomBoundsY = Random.Range(minRoomBounds.Item2, yRange.Item2 - topLeftY);

                    roomMap[yIndex, xIndex] = roomGen.makeRoom(topLeftX, topLeftY, roomBoundsX, roomBoundsY);

                    for (int index = 0; index < 4; ++index)
                    {
                        switch (index)
                        {
                            case 0:
                                if (xIndex == 0)
                                    break;
                                roomMap[yIndex, xIndex].addRoomExit(topLeftX - 1, Random.Range(topLeftY, topLeftY + roomBoundsY));
                                break;
                            case 1:
                                if (yIndex == 0)
                                    break;
                                roomMap[yIndex, xIndex].addRoomExit(Random.Range(topLeftX, topLeftX + roomBoundsX), topLeftY - 1);
                                break;
                            case 2:
                                if (xIndex == 4)
                                    break;
                                roomMap[yIndex, xIndex].addRoomExit(topLeftX + roomBoundsX, Random.Range(topLeftY, topLeftY + roomBoundsY));
                                break;
                            case 3:
                                if (yIndex == 4)
                                    break;
                                roomMap[yIndex, xIndex].addRoomExit(Random.Range(topLeftX, topLeftX + roomBoundsX), topLeftY + roomBoundsY);
                                break;
                        }
                    }
                }
            }

            // This would be much easier to follow if I just do everything in terms of the global positions...
            for (int y = 0; y < 5; ++y)
            {
                for (int x = 0; x < 5; ++x)
                {
                    (int, int) topLeft = roomMap[y, x].getRoomCorner();
                    for (int yRoomSpace = 0; yRoomSpace < roomMap[y, x].getRoomSpace().GetLength(0); ++yRoomSpace)
                    {
                        for (int xRoomSpace = 0; xRoomSpace < roomMap[y, x].getRoomSpace().GetLength(1); ++ xRoomSpace)
                        {
                            dungeonMap[topLeft.Item1 + xRoomSpace, topLeft.Item2 + yRoomSpace] = roomMap[y, x].getRoomSpace()[yRoomSpace, xRoomSpace];
                        }
                    }
                    foreach ((int, int) exitCoord in roomMap[y, x].getRoomExits())
                    {
                        dungeonMap[exitCoord.Item1, exitCoord.Item2] = EnviromentType.Floor;
                    }
                }
            }

            return dungeonMap;
        }
    }
}

