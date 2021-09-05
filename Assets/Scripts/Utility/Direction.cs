namespace Assets.Utility
{
    public static class Extensions
    {
        public static Direction otherDirection(this Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return Direction.South;
                case Direction.South:
                    return Direction.North;
                case Direction.East:
                    return Direction.West;
                case Direction.West:
                    return Direction.East;
                default:
                    return Direction.North;
            }
        }
    }
    public enum Direction
    {
        North,
        South,
        East,
        West
    }
}