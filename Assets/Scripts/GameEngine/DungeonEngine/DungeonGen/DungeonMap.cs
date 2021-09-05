using System.Collections;
using System.Collections.Generic;

using Assets.GameEngine.DungeonEngine.DungeonGen.Enviroment;

namespace Assets.GameEngine.DungeonEngine.DungeonGen {
    public class DungeonMap {
        private EnviromentType[,] dungeonMap;
        private List<Room> dungeonRooms;

        public DungeonMap() {
            dungeonMap = new EnviromentType[1,1];
            dungeonRooms = new List<Room>();
        }

        public DungeonMap(int x, int y) {
            dungeonMap = new EnviromentType[x, y];
            dungeonRooms = new List<Room>();
        }

        public EnviromentType[,] getMap() {
            return dungeonMap;
        }

        public void createNewDungeonMap(int x, int y) {
            dungeonMap = new EnviromentType[x, y];
        }

        public List<Room> getRooms() {
            return dungeonRooms;
        }

        public void addRoom(Room room) {
            dungeonRooms.Add(room);
        }

        public void clearRooms() {
            dungeonRooms.Clear();
        }
    }
}