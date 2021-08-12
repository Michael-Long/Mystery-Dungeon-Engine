using System.Collections;
using System.Collections.Generic;

using Assets.Utility;

namespace Assets.GameEngine.DungeonEngine.DungeonGen
{
    public class Room
    {
        private (int, int) bounds;
        private (int, int) topLeftCorner;
        private List<(int, int)> exits;
        private bool isMonsterHouse;

        public Room(int x, int y)
        {
            bounds = (x, y);
            topLeftCorner = (0, 0);
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
            return topLeftCorner;
        }

        public void setRoomCorner(int x, int y)
        {
            topLeftCorner = (x, y);
        }

        public List<(int, int)> getRoomExits()
        {
            return exits;
        }

        public void addRoomExit(int x, int y)
        {
            // Should verify that it's on the edge of the room
            if (validRoomExit(x, y))
                exits.Add((x, y));
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

        private bool validRoomExit(int x, int y)
        {
            if (x == topLeftCorner.Item1 - 1 || x == topLeftCorner.Item1 + bounds.Item1 + 1)
            {
                // This would be valid along the vertical edges
                return MathUtil.isBetweenInclusive(y, topLeftCorner.Item2, topLeftCorner.Item2 + bounds.Item2);

            }
            else if (MathUtil.isBetweenInclusive(x, topLeftCorner.Item1, topLeftCorner.Item1 + bounds.Item1))
            {
                // Only two heights will be valid
                return y == topLeftCorner.Item2 - 1 || y == topLeftCorner.Item2 + bounds.Item2 + 1;
            }
            return false;
        }
    }
}