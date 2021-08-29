using System.Collections;
using System.Collections.Generic;

using Assets.Utility;

namespace Assets.GameEngine.DungeonEngine.DungeonGen
{
    public class Room
    {
        private bool[,] roomSpace;
        private (int, int) topLeftCorner;
        private List<(int, int)> exits;
        private bool isMonsterHouse;

        public Room(int x, int y)
        {
            roomSpace = new bool[y, x];
            topLeftCorner = (0, 0);
            exits = new List<(int, int)>();
            isMonsterHouse = false;
        }

        public bool[,] getRoomSpace() {
            return roomSpace;
        }

        public void setRoomBounds(int xBound, int yBound)
        {
            roomSpace = new bool[yBound, xBound];
        }

        public (int, int) getRoomCorner()
        {
            return topLeftCorner;
        }

        public void setRoomCorner(int x, int y)
        {
            topLeftCorner = (x, y);
        }

        public List<(int, int)> getRoomExits()
        {
            List<(int, int)> worldSpaceExits = new List<(int, int)>();
            foreach ((int, int) roomCoord in exits)
            {
                worldSpaceExits.Add((roomCoord.Item1 + topLeftCorner.Item1, roomCoord.Item2 + topLeftCorner.Item2));
            }
            return worldSpaceExits;
        }

        public bool addRoomExit(int xWorld, int yWorld)
        {
            // X and Y are worldspace coords, so we convert them to roomSpace
            int roomSpaceX = xWorld - topLeftCorner.Item1;
            int roomSpaceY = yWorld - topLeftCorner.Item2;

            // Should verify that it's on the edge of the room
            if (validRoomExit(roomSpaceX, roomSpaceY))
            {
                exits.Add((roomSpaceX, roomSpaceY));
                return true;
            }
            return false;
        }

        public void clearRoomExits()
        {
            exits.Clear();
        }

        public bool isRoomMonsterHouse()
        {
            return isMonsterHouse;
        }

        public void setIsMonsterHouse(bool isMH)
        {
            isMonsterHouse = isMH;
        }

        public bool isCoordWithinRoom(int xWorld, int yWorld)
        {
            // X and Y are worldspace coords, so we convert them to roomSpace
            int roomSpaceX = xWorld - topLeftCorner.Item1;
            int roomSpaceY = yWorld - topLeftCorner.Item2;

            if (roomSpaceX < -1 || roomSpaceY < -1)
                return false;
            if (roomSpaceX > roomSpace.GetLength(1) || roomSpaceY > roomSpace.GetLength(0))
                return false;
            // Could be either inside the room or an exit.
            foreach ((int, int) exit in exits)
            {
                if (exit.Item1 == roomSpaceX && exit.Item2 == roomSpaceY)
                    return true;
            }
            // Could either be inside the room or along the edge
            if (roomSpaceX < 0 || roomSpaceY < 0)
                return false;
            if (roomSpaceX > roomSpace.GetLength(1) - 1 || roomSpaceY > roomSpace.GetLength(0) - 1)
                return false;
            // Within the room
            return true;
        }

        private bool validRoomExit(int x, int y)
        {
            // X and Y are worldspace coords, so we convert them to roomSpace
            int roomSpaceX = x - topLeftCorner.Item1;
            int roomSpaceY = y - topLeftCorner.Item2;

            if (x == -1 || x == roomSpace.GetLength(1))
            {
                // This would be valid along the vertical edges
                return MathUtil.isBetweenInclusive(y, 0, roomSpace.GetLength(0) - 1);

            }
            else if (MathUtil.isBetweenInclusive(x, 0, roomSpace.GetLength(1)))
            {
                // Only two heights will be valid
                return y == -1 || y == roomSpace.GetLength(0);
            }
            return false;
        }
    }
}