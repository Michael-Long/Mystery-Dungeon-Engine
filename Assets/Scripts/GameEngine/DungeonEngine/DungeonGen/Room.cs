using System.Collections;
using System.Collections.Generic;

using Assets.Utility;

namespace Assets.GameEngine.DungeonEngine.DungeonGen
{
    public class Room
    {
        private (int, int) bounds;
        private (int, int) bottomLeftCorner;
        private List<(int, int)> exits;
        private bool isMonsterHouse;

        public Room(int x, int y)
        {
            bounds = (x, y);
            bottomLeftCorner = (0, 0);
            exits = new List<(int, int)>();
            isMonsterHouse = false;
        }

        public (int, int) getRoomBounds() {
            return bounds;
        }

        public void setRoomBounds(int xBound, int yBound)
        {
            bounds = (xBound, yBound);
        }

        public (int, int) getRoomCorner()
        {
            return bottomLeftCorner;
        }

        public void setRoomCorner(int x, int y)
        {
            bottomLeftCorner = (x, y);
            topRightCorner = (x + bounds.Item1, y + bounds.Item2);
        }

        public List<(int, int)> getRoomExits()
        {
            return exits;
        }

        public bool addRoomExit(int x, int y)
        {
            // Should verify that it's on the edge of the room
            if (validRoomExit(x, y))
            {
                exits.Add((x, y));
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

        public bool isCoordWithinRoom(int x, int y)
        {
            if (x < bottomLeftCorner.Item1 - 1 || y < bottomLeftCorner.Item2 - 1)
                return false;
            if (x > bottomLeftCorner.Item1 + bounds.Item1 + 1 || y > bottomLeftCorner.Item2 + bounds.Item2 + 1)
                return false;
            // Could be either inside the room or an exit.
            foreach ((int, int) exit in exits)
            {
                if (exit.Item1 == x && exit.Item2 == y)
                    return true;
            }
            // Could either be inside the room or along the edge
            if (x < bottomLeftCorner.Item1 || y < bottomLeftCorner.Item2)
                return false;
            if (x > bottomLeftCorner.Item1 + bounds.Item1 || y > bottomLeftCorner.Item2)
                return false;
            // Within the room
            return true;
        }

        private bool validRoomExit(int x, int y)
        {
            if (x == bottomLeftCorner.Item1 - 1 || x == bottomLeftCorner.Item1 + bounds.Item1)
            {
                // This would be valid along the vertical edges
                return MathUtil.isBetweenInclusive(y, bottomLeftCorner.Item2, bottomLeftCorner.Item2 + bounds.Item2);

            }
            else if (MathUtil.isBetweenInclusive(x, bottomLeftCorner.Item1, bottomLeftCorner.Item1 + bounds.Item1))
            {
                // Only two heights will be valid
                return y == bottomLeftCorner.Item2 - 1 || y == bottomLeftCorner.Item2 + bounds.Item2;
            }
            return false;
        }
    }
}