using Assets.GameEngine.DungeonEngine.DungeonGen;
using Assets.GameEngine.DungeonEngine.DungeonGen.Enviroment;

namespace Assets.GameEngine.DungeonEngine.DungeonGen.Generators.RoomGenerators
{
    public class BoxRoomGenerator : RoomGeneratorInterface
    {
        public Room makeRoom(int x, int y, int xBound, int yBound)
        {
            Room room = new Room(xBound, yBound);
            room.setRoomCorner(x, y);

            for (int yRoomSpace = 0; yRoomSpace < yBound; ++yRoomSpace)
            {
                for (int xRoomSpace = 0; xRoomSpace < xBound; ++xRoomSpace)
                {
                    room.getRoomSpace()[yRoomSpace, xRoomSpace] = EnviromentType.Floor;
                }
            }

            return room;
        }

    }
}