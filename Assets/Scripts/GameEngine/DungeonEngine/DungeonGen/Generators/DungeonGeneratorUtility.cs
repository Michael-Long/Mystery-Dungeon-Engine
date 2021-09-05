using System;

using Assets.GameEngine.DungeonEngine.DungeonGen.Enviroment;

namespace Assets.GameEngine.DungeonEngine.DungeonGen.Generators {
    public static class DungeonGeneratorUtility {
        public static void drawRoom(Room room, EnviromentType[,] dungeonMap) {
            (int, int) topLeft = room.getRoomCorner();
            for (int yRoomSpace = 0; yRoomSpace < room.getRoomSpace().GetLength(0); ++yRoomSpace) {
                for (int xRoomSpace = 0; xRoomSpace < room.getRoomSpace().GetLength(1); ++xRoomSpace) {
                    dungeonMap[topLeft.Item1 + xRoomSpace, topLeft.Item2 + yRoomSpace] = room.getRoomSpace()[yRoomSpace, xRoomSpace];
                }
            }

            foreach ((int, int) exitCoord in room.getRoomExits()) {
                dungeonMap[exitCoord.Item1, exitCoord.Item2] = EnviromentType.Floor;
            }
        }

        public static void drawStraightLine((int, int) startPt, (int, int) endPt, EnviromentType[,] dungeonMap) {
            if (startPt.Item1 != endPt.Item1 && startPt.Item2 != endPt.Item2)
                return;

            if (startPt.Item1 == endPt.Item1) {
                for (int y = Math.Min(startPt.Item2, endPt.Item2); y <= Math.Max(startPt.Item2, endPt.Item2); ++y)
                    dungeonMap[startPt.Item1, y] = EnviromentType.Floor;
            }
            else {
                for (int x = Math.Min(startPt.Item1, endPt.Item1); x <= Math.Max(startPt.Item1, endPt.Item1); ++x)
                    dungeonMap[x, startPt.Item2] = EnviromentType.Floor;
            }
        }
    }
}