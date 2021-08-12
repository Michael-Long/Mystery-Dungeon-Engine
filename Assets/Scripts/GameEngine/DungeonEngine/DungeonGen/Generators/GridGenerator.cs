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
        public override void GenerateLevel(int xRadius, int yRadius)
        {
            List<List<GridEntry>> grid = CreateGrid(xRadius, yRadius);

            for (int rowIndex = 0; rowIndex < grid.Count; ++rowIndex)
            {
                for (int colIndex = 0; colIndex < grid[rowIndex].Count; ++colIndex)
                {
                    (int, int) xRange = (0, grid[rowIndex][colIndex].bounds.Item1);
                    (int, int) yRange = (0, grid[rowIndex][colIndex].bounds.Item2);

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

                    grid[rowIndex][colIndex].makeRoom(roomBounds.Item1, roomBounds.Item2, topLeft.Item1, topLeft.Item2);

                    for (int index = 0; index < 4; ++index)
                    {
                        switch (index)
                        {
                            case 0:
                                if (colIndex == 0)
                                    break;
                                grid[rowIndex][colIndex].room.addRoomExit(topLeft.Item1 - 1, Random.Range(topLeft.Item2, topLeft.Item2 + roomBounds.Item2));
                                break;
                            case 1:
                                if (rowIndex == 0)
                                    break;
                                grid[rowIndex][colIndex].room.addRoomExit(Random.Range(topLeft.Item1, topLeft.Item1 + roomBounds.Item1), topLeft.Item2 - 1);
                                break;
                            case 2:
                                if (colIndex == grid[rowIndex].Count - 1)
                                    break;
                                grid[rowIndex][colIndex].room.addRoomExit(topLeft.Item1 + roomBounds.Item1 + 1, Random.Range(topLeft.Item2, topLeft.Item2 + roomBounds.Item2));
                                break;
                            case 3:
                                if (rowIndex == grid.Count - 1)
                                    break;
                                grid[rowIndex][colIndex].room.addRoomExit(Random.Range(topLeft.Item1, topLeft.Item1 + roomBounds.Item1), topLeft.Item2 + roomBounds.Item2 + 1);
                                break;
                        }
                    }
                }
            }

            bool stop = true;
        }
    }
}

