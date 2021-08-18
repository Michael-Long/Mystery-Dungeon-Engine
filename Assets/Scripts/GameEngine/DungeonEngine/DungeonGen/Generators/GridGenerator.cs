using System.Collections.Generic;
using UnityEngine;

using Assets.GameEngine.DungeonEngine.DungeonGen;

namespace Assets.GameEngine.DungeonEngine.DungeonGen.Generators
{
    [CreateAssetMenu(fileName = "GridGenerator", menuName = "ScriptableObjects/Generators/GridGenerator", order = 1)]
    public class GridGenerator : GeneratorInterface
    {
        private class GridEntry
        {
            public GridEntry(int x, int y)
            {
                bounds = (x, y);
            }

            public void makeRoom(int xBound, int yBound, int topLeftCornerX, int topLeftCornerY)
            {
                room = new Room(xBound, yBound);
                room.setRoomCorner(topLeftCornerX, topLeftCornerY);
            }

            public bool isCoordFloor(int x, int y)
            {
                return room.isCoordWithinRoom(x, y);
            }

            public (int, int) bounds { get; set; } // x, y
            public Room room { get; set; }
        }

        List<List<GridEntry>> CreateGrid(int xRadius, int yRadius)
        {
            (int, int) gridBounds = (xRadius / 5, yRadius / 5);
            List<List<GridEntry>> grid = new List<List<GridEntry>>();
            for (int rowIndex = 0; rowIndex < 5; ++rowIndex)
            {
                List<GridEntry> gridRow = new List<GridEntry>();
                for (int colIndex = 0; colIndex < 5; ++colIndex)
                {
                    gridRow.Add(new GridEntry(gridBounds.Item1, gridBounds.Item2));
                }
                grid.Add(gridRow);
            }

            return grid;
        }

        // This will generate a dungeon level with a 5x5 grid of rooms, each of which connected to each other
        public override bool[,] GenerateLevel(int xRadius, int yRadius)
        {
            bool[,] dungeonMap = new bool[xRadius, yRadius];
            // roomGrid splits our working map into a 5x5, with a room in each section. All adjacent rooms are connected.
            List<List<GridEntry>> roomGrid = CreateGrid(xRadius, yRadius);

            for (int rowIndex = 0; rowIndex < roomGrid.Count; ++rowIndex)
            {
                for (int colIndex = 0; colIndex < roomGrid[rowIndex].Count; ++colIndex)
                {
                    (int, int) xRange = (0, roomGrid[rowIndex][colIndex].bounds.Item1);
                    (int, int) yRange = (0, roomGrid[rowIndex][colIndex].bounds.Item2);

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

                    (int, int) topLeft = (Random.Range(xRange.Item1, xRange.Item2), Random.Range(yRange.Item1, yRange.Item2));

                    (int, int) roomBounds = (Random.Range(4, xRange.Item2 - topLeft.Item1), Random.Range(4, yRange.Item2 - topLeft.Item2));

                    roomGrid[rowIndex][colIndex].makeRoom(roomBounds.Item1, roomBounds.Item2, topLeft.Item1, topLeft.Item2);

                    for (int index = 0; index < 4; ++index)
                    {
                        switch (index)
                        {
                            case 0:
                                if (colIndex == 0)
                                    break;
                                roomGrid[rowIndex][colIndex].room.addRoomExit(topLeft.Item1 - 1, Random.Range(topLeft.Item2, topLeft.Item2 + roomBounds.Item2));
                                break;
                            case 1:
                                if (rowIndex == 0)
                                    break;
                                roomGrid[rowIndex][colIndex].room.addRoomExit(Random.Range(topLeft.Item1, topLeft.Item1 + roomBounds.Item1), topLeft.Item2 - 1);
                                break;
                            case 2:
                                if (colIndex == roomGrid[rowIndex].Count - 1)
                                    break;
                                roomGrid[rowIndex][colIndex].room.addRoomExit(topLeft.Item1 + roomBounds.Item1 + 1, Random.Range(topLeft.Item2, topLeft.Item2 + roomBounds.Item2));
                                break;
                            case 3:
                                if (rowIndex == roomGrid.Count - 1)
                                    break;
                                roomGrid[rowIndex][colIndex].room.addRoomExit(Random.Range(topLeft.Item1, topLeft.Item1 + roomBounds.Item1), topLeft.Item2 + roomBounds.Item2 + 1);
                                break;
                        }
                    }
                }
            }

            for (int x = 0; x < xRadius; ++x)
            {
                for (int y = 0; y < yRadius; ++y)
                {
                    dungeonMap[x,y] = roomGrid[x / (xRadius / 5)][y / (yRadius / 5)].isCoordFloor(x, y);
                }
            }

            return dungeonMap;
        }
    }
}

