using UnityEngine;
using System.Collections.Generic;

using Assets.GameEngine.DungeonEngine.DungeonGen.Generators.RoomGenerators;
using Assets.GameEngine.DungeonEngine.DungeonGen.Enviroment;
using Assets.Utility;

namespace Assets.GameEngine.DungeonEngine.DungeonGen.Generators {
    [CreateAssetMenu(fileName = "GridGenerator", menuName = "ScriptableObjects/Generators/GridGenerator", order = 1)]
    public class GridGenerator : GeneratorInterface {
        private struct RoomData {
            public Room room;
            public Dictionary<Direction, (int, int)> exits;
        }

        // This will generate a dungeon level with a 5x5 grid of rooms, each of which connected to each other
        public override EnviromentType[,] GenerateLevel(int xRadius, int yRadius) {
            EnviromentType[,] dungeonMap = new EnviromentType[xRadius, yRadius];
            BoxRoomGenerator roomGen = new BoxRoomGenerator();
            for (int x = 0; x < xRadius; ++x) {
                for (int y = 0; y < yRadius; ++y) {
                    dungeonMap[x, y] = EnviromentType.Wall;
                }
            }
            // roomGrid splits our working map into a 5x5, with a room in each section. All adjacent rooms are connected.
            RoomData[,] roomMap = new RoomData[5, 5];
            (int, int) gridOffset = (xRadius / 5, yRadius / 5);

            for (int yIndex = 0; yIndex < 5; ++yIndex) {
                for (int xIndex = 0; xIndex < 5; ++xIndex) {
                    (int, int) xRange = (xIndex * gridOffset.Item1, xIndex * gridOffset.Item1 + gridOffset.Item1 - 1);
                    (int, int) yRange = (yIndex * gridOffset.Item2, yIndex * gridOffset.Item2 + gridOffset.Item2 - 1);

                    if (System.Math.Abs(xRange.Item1 - xRange.Item2) > 2) {
                        xRange.Item1 += 1;
                        xRange.Item2 -= 1;
                    }

                    if (System.Math.Abs(yRange.Item1 - yRange.Item2) > 2) {
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

                    roomMap[yIndex, xIndex] = new RoomData();
                    roomMap[yIndex, xIndex].room = roomGen.makeRoom(topLeftX, topLeftY, roomBoundsX, roomBoundsY);
                    roomMap[yIndex, xIndex].exits = new Dictionary<Direction, (int, int)>();

                    foreach (Direction direction in System.Enum.GetValues(typeof(Direction))) {
                        int x, y;
                        switch (direction) {
                            case Direction.West:
                                if (xIndex == 0)
                                    break;
                                x = topLeftX - 1;
                                y = Random.Range(topLeftY, topLeftY + roomBoundsY);
                                if (roomMap[yIndex, xIndex].room.addRoomExit(x, y))
                                    roomMap[yIndex, xIndex].exits.Add(direction, (x, y));
                                break;
                            case Direction.South:
                                if (yIndex == 0)
                                    break;
                                x = Random.Range(topLeftX, topLeftX + roomBoundsX);
                                y = topLeftY - 1;
                                if (roomMap[yIndex, xIndex].room.addRoomExit(x, y))
                                    roomMap[yIndex, xIndex].exits.Add(direction, (x, y));
                                break;
                            case Direction.East:
                                if (xIndex == 4)
                                    break;
                                x = topLeftX + roomBoundsX;
                                y = Random.Range(topLeftY, topLeftY + roomBoundsY);
                                if (roomMap[yIndex, xIndex].room.addRoomExit(x, y))
                                    roomMap[yIndex, xIndex].exits.Add(direction, (x, y));
                                break;
                            case Direction.North:
                                if (yIndex == 4)
                                    break;
                                x = Random.Range(topLeftX, topLeftX + roomBoundsX);
                                y = topLeftY + roomBoundsY;
                                if (roomMap[yIndex, xIndex].room.addRoomExit(x, y))
                                    roomMap[yIndex, xIndex].exits.Add(direction, (x, y));
                                break;
                        }
                    }
                }
            }

            for (int y = 0; y < 5; ++y) {
                for (int x = 0; x < 5; ++x) {
                    DungeonGeneratorUtility.drawRoom(roomMap[y, x].room, dungeonMap);
                }
            }

            for (int yPath = 0; yPath < 5; ++yPath) {
                for (int xPath = 0; xPath < 5; ++xPath) {
                    RoomData mainRoom = roomMap[yPath, xPath];
                    List<Direction> halfDirections = new List<Direction> { Direction.East, Direction.North };
                    foreach (Direction pathOut in halfDirections) {
                        if (!mainRoom.exits.ContainsKey(pathOut))
                            continue;
                        RoomData otherRoom = pathOut == Direction.East ? roomMap[yPath, xPath + 1] : roomMap[yPath + 1, xPath];
                        if (!otherRoom.exits.ContainsKey(pathOut.otherDirection()))
                            continue;

                        (int, int) mainExit = mainRoom.exits[pathOut];
                        (int, int) otherExit = otherRoom.exits[pathOut.otherDirection()];

                        if (pathOut == Direction.East) {
                            int avgX = (mainExit.Item1 + otherExit.Item1) / 2;

                            DungeonGeneratorUtility.drawStraightLine(mainExit, (avgX, mainExit.Item2), dungeonMap);
                            DungeonGeneratorUtility.drawStraightLine((avgX, mainExit.Item2), (avgX, otherExit.Item2), dungeonMap);
                            DungeonGeneratorUtility.drawStraightLine((avgX, otherExit.Item2), otherExit, dungeonMap);
                        }
                        else {
                            int avgY = (mainExit.Item2 + otherExit.Item2) / 2;

                            DungeonGeneratorUtility.drawStraightLine(mainExit, (mainExit.Item1, avgY), dungeonMap);
                            DungeonGeneratorUtility.drawStraightLine((mainExit.Item1, avgY), (otherExit.Item1, avgY), dungeonMap);
                            DungeonGeneratorUtility.drawStraightLine((otherExit.Item1, avgY), otherExit, dungeonMap);
                        }
                    }
                }
            }

            return dungeonMap;
        }
    }
}

