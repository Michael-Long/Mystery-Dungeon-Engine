using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.GameEngine.DungeonEngine.DungeonGen.Enviroment {
    public class EnviromentController : MonoBehaviour
    {
        [Tooltip("This is the list of tilemaps that is to be used in this enviroment. These are walls, floors, water, etc.")]
        public List<EnviromentMap> maps;

        public void produceEnviroment(bool[,] dungeonMap)
        {
            for (int x = 0; x < dungeonMap.GetLength(0); ++x)
            {
                for (int y = 0; y < dungeonMap.GetLength(1); ++y)
                {
                    // This will have to be expanded upon. Perhaps dungeon map should be EnviromentType[,] instead of bool[,]
                    if (dungeonMap[x, y])
                    {
                        createTile(EnviromentType.Floor, x, y);
                    }
                    else
                    {
                        createTile(EnviromentType.Wall, x, y);
                    }
                }
            }
        }

        private GameObject createTile(EnviromentType type, int x, int y)
        {
            GameObject tile = new GameObject();
            tile.transform.position = new Vector3(x, y, 0);
            SpriteRenderer tileSprite = tile.AddComponent<SpriteRenderer>();
            switch (type)
            {
                case EnviromentType.Floor:
                    tile.layer = LayerMask.NameToLayer("Floor");
                    tileSprite.sprite = maps[1].DotSprites[0];
                    break;
                case EnviromentType.Wall:
                    tile.layer = LayerMask.NameToLayer("Walls");
                    tileSprite.sprite = maps[0].DotSprites[0];
                    break;
            }
            return tile;
        }
    }
}