using System.Collections.Generic;
using UnityEngine;

namespace Assets.GameEngine.DungeonEngine.DungeonGen.Generators
{


    [CreateAssetMenu(fileName = "GridGenerator", menuName = "ScriptableObjects/Generators/GridGenerator", order = 1)]
    public class GridGenerator : GeneratorInterface
    {
        //private class GridEntry
        //{
        //    public GridEntry(int x, int y)
        //    {
        //        bounds = (x, y);
        //    }

        //    public void makeRoom(int xBound, int yBound, int topLeftCornerX, int topLeftCornerY)
        //    {
        //        room = new Room(xBound, yBound);
        //        room.setRoomCorner(topLeftCornerX, topLeftCornerY);
        //    }

        //    public bool isCoordFloor(int x, int y)
        //    {
        //        return room.isCoordWithinRoom(x, y);
        //    }

        //    public (int, int) bounds { get; set; } // x, y
        //    public Room room { get; set; }
        //}

        //List<List<GridEntry>> CreateGrid(int xRadius, int yRadius)
        //{
        //    (int, int) gridBounds = (xRadius / 5, yRadius / 5);
        //    List<List<GridEntry>> grid = new List<List<GridEntry>>();
        //    for (int rowIndex = 0; rowIndex < 5; ++rowIndex)
        //    {
        //        List<GridEntry> gridRow = new List<GridEntry>();
        //        for (int colIndex = 0; colIndex < 5; ++colIndex)
        //        {
        //            gridRow.Add(new GridEntry(gridBounds.Item1, gridBounds.Item2));
        //        }
        //        grid.Add(gridRow);
        //    }

        //    return grid;
        //}

        // This will generate a dungeon level with a 5x5 grid of rooms, each of which connected to each other
        public override bool[,] GenerateLevel(int xRadius, int yRadius)
        {
            bool[,] dungeonMap = new bool[xRadius, yRadius];
            // roomGrid splits our working map into a 5x5, with a room in each section. All adjacent rooms are connected.
            Room[,] roomMap = new Room[5, 5];
            (int, int) gridOffset = (xRadius / 5, yRadius / 5);

            for (int yIndex = 0; yIndex < 5; ++yIndex)
            {
                for (int xIndex = 0; xIndex < 5; ++xIndex)
                {
                    (int, int) xRange = (xIndex * gridOffset.Item1, xIndex * gridOffset.Item1 + gridOffset.Item1);
                    (int, int) yRange = (yIndex * gridOffset.Item2, yIndex * gridOffset.Item2 + gridOffset.Item2);

                    if (System.Math.Abs(xRange.Item1 - xRange.Item2) > 2)
                    {
                        xRange.Item1 += 2;
                        xRange.Item2 -= 2;
                    }

                    if (System.Math.Abs(yRange.Item1 - yRange.Item2) > 2)
                    {
                        yRange.Item1 += 2;
                        yRange.Item2 -= 2;
                    }

                    (int, int) topLeft = (Random.Range(xRange.Item1 + 1, xRange.Item2 - 1), Random.Range(yRange.Item1 + 1, yRange.Item2 - 1));

                    (int, int) roomBounds = (Random.Range(4, xRange.Item2 - topLeft.Item1), Random.Range(4, yRange.Item2 - topLeft.Item2));

                    roomMap[yIndex, xIndex] = new Room(roomBounds.Item1, roomBounds.Item2);
                    roomMap[yIndex, xIndex].setRoomCorner(topLeft.Item1, topLeft.Item2);

                    for (int index = 0; index < 4; ++index)
                    {
                        switch (index)
                        {
                            case 0:
                                if (xIndex == 0)
                                    break;
                                roomMap[yIndex, xIndex].addRoomExit(topLeft.Item1 - 1, Random.Range(topLeft.Item2, topLeft.Item2 + roomBounds.Item2));
                                break;
                            case 1:
                                if (yIndex == 0)
                                    break;
                                roomMap[yIndex, xIndex].addRoomExit(Random.Range(topLeft.Item1, topLeft.Item1 + roomBounds.Item1), topLeft.Item2 - 1);
                                break;
                            case 2:
                                if (xIndex == 4)
                                    break;
                                roomMap[yIndex, xIndex].addRoomExit(topLeft.Item1 + roomBounds.Item1, Random.Range(topLeft.Item2, topLeft.Item2 + roomBounds.Item2));
                                break;
                            case 3:
                                if (yIndex == 4)
                                    break;
                                roomMap[yIndex, xIndex].addRoomExit(Random.Range(topLeft.Item1, topLeft.Item1 + roomBounds.Item1), topLeft.Item2 + roomBounds.Item2);
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
                    for (int yRoom = topLeft.Item2; yRoom < topLeft.Item2 + roomMap[y, x].getRoomBounds().Item2; ++yRoom)
                    {
                        for (int xRoom = topLeft.Item1; xRoom < topLeft.Item1 + roomMap[y, x].getRoomBounds().Item1; ++ xRoom)
                        {
                            dungeonMap[xRoom, yRoom] = true;
                        }
                    }
                    foreach ((int, int) exitCoord in roomMap[y, x].getRoomExits())
                    {
                        dungeonMap[exitCoord.Item1, exitCoord.Item2] = true;
                    }
                }
            }

            return dungeonMap;
        }
    }
}

