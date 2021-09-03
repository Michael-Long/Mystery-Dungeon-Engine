using Assets.GameEngine.DungeonEngine.DungeonGen;

namespace Assets.GameEngine.DungeonEngine.DungeonGen.Generators.RoomGenerators
{
    public interface RoomGeneratorInterface
    {
        public Room makeRoom(int x, int y, int xBound, int yBound);
    }
}