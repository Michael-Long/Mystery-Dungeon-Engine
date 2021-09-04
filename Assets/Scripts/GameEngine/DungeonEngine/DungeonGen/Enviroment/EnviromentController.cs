using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Utility;

namespace Assets.GameEngine.DungeonEngine.DungeonGen.Enviroment {
    public class EnviromentController : MonoBehaviour
    {
        [Tooltip("This is the list of tilemaps that is to be used in this enviroment. These are walls, floors, water, etc.")]
        public List<EnviromentMap> maps;

        public void produceEnviroment(EnviromentType[,] dungeonMap)
        {
            for (int x = 0; x < dungeonMap.GetLength(0); ++x)
            {
                for (int y = 0; y < dungeonMap.GetLength(1); ++y)
                {
                    createTile(dungeonMap[x, y], x, y, dungeonMap);
                }
            }
        }

        private GameObject createTile(EnviromentType type, int x, int y, EnviromentType[,] dungeonMap)
        {
            GameObject tile = new GameObject();
            tile.transform.position = new Vector3(x, y, 0);
            SpriteRenderer tileSprite = tile.AddComponent<SpriteRenderer>();
            EnviromentPosition currPositionType = convertCalcToPosition(calcSprite(x, y, dungeonMap));
            switch (type)
            {
                case EnviromentType.Floor:
                    tile.name = "(" + x + ", " + y + ") Floor - " + currPositionType.ToString();
                    tile.layer = LayerMask.NameToLayer("Floor");

                    List<Sprite> floorSpriteList = maps[1].getSpriteList(currPositionType);
                    tileSprite.sprite = floorSpriteList[Random.Range(0, floorSpriteList.Count - 1)];
                    break;
                case EnviromentType.Wall:
                    tile.name = "(" + x + ", " + y + ") Walls - " + currPositionType.ToString();
                    tile.layer = LayerMask.NameToLayer("Walls");

                    List<Sprite> wallSpriteList = maps[0].getSpriteList(currPositionType);
                    tileSprite.sprite = wallSpriteList[Random.Range(0, wallSpriteList.Count - 1)];
                    break;
            }
            return tile;
        }

        private long calcSprite(int x, int y, EnviromentType[,] dungeonMap)
        {
            // Positioning format, a 3x3 grid with each section a different power of 2. This gives every combination of detection a unique value.
            // Used in connecting the found layout with a sprite.
            // 1  |  2  | 4
            // 8  |  16 | 32
            // 64 | 128 | 256
            double calc = 0;
            int powCount = 0;
            for (int yIndex = -1; yIndex <= 1; ++yIndex)
            {
                for (int xIndex = -1; xIndex <= 1; ++xIndex)
                {
                    EnviromentType adjType = EnviromentType.Wall;
                    if (MathUtil.isBetweenInclusive(x + xIndex, 0, dungeonMap.GetLength(0) - 1) && MathUtil.isBetweenInclusive(y + yIndex, 0, dungeonMap.GetLength(1) - 1))
                        adjType = dungeonMap[x + xIndex, y + yIndex];

                    if (dungeonMap[x, y] != adjType)
                        calc += System.Math.Pow(2, powCount);
                    ++powCount;
                }
            }
            return (long)calc;
        }

        private EnviromentPosition convertCalcToPosition(long spriteIndex)
        {
            // Is this hacky? A bit, but there isn't a clear pattern for defining these wall cases, so it's the best we got (I think)
            switch (spriteIndex)
            {
                case 10:
                case 11:
                case 14:
                case 15:
                case 74:
                case 75:
                case 78:
                case 79:
                    // BottomLeft Corner
                    return EnviromentPosition.BottomLeftCorner;
                case 2:
                case 3:
                case 7:
                case 6:
                    //Bottom
                    return EnviromentPosition.Bottom;
                case 34:
                case 35:
                case 38:
                case 39:
                case 290:
                case 291:
                case 294:
                case 295:
                    //BottomRightCorner
                    return EnviromentPosition.BottomRightCorner;
                case 8:
                case 9:
                case 73:
                case 72:
                    //Left
                    return EnviromentPosition.Left;
                case 0:
                    //Middle
                    return EnviromentPosition.Middle;
                case 32:
                case 36:
                case 292:
                case 288:
                    //Right
                    return EnviromentPosition.Right;
                case 136:
                case 137:
                case 200:
                case 201:
                case 392:
                case 393:
                case 456:
                case 457:
                    // TopLeftCorner
                    return EnviromentPosition.TopLeftCorner;
                case 128:
                case 192:
                case 448:
                case 384:
                    //Top
                    return EnviromentPosition.Top;
                case 420:
                case 228:
                case 480:
                case 164:
                case 416:
                case 224:
                case 160:
                case 484:
                    //TopRightCorner
                    return EnviromentPosition.TopRightCorner;
                case 266:
                case 267:
                case 270:
                case 271:
                case 330:
                case 331:
                case 334:
                case 335:
                    //BottomLeftSingleCorner
                    return EnviromentPosition.BottomLeftSingleCorner;
                case 130:
                case 131:
                case 134:
                case 135:
                case 194:
                case 195:
                case 198:
                case 199:
                case 386:
                case 387:
                case 390:
                case 391:
                case 450:
                case 451:
                case 454:
                case 455:
                    //Horizontial Single
                    return EnviromentPosition.HorizontialSingle;
                case 98:
                case 99:
                case 102:
                case 103:
                case 354:
                case 355:
                case 358:
                case 359:
                    //BottomRightSingleCorner
                    return EnviromentPosition.BottomRightSingleCorner;
                case 40:
                case 41:
                case 44:
                case 45:
                case 104:
                case 105:
                case 108:
                case 296:
                case 297:
                case 300:
                case 364:
                case 301:
                case 361:
                case 109:
                case 360:
                case 365:
                    //Vertical Single
                    return EnviromentPosition.VerticalSingle;
                case 170:
                case 171:
                case 174:
                case 175:
                case 234:
                case 235:
                case 238:
                case 239:
                case 426:
                case 427:
                case 430:
                case 431:
                case 490:
                case 491:
                case 494:
                case 495:
                    //Dot
                    return EnviromentPosition.Dot;
                case 140:
                case 141:
                case 204:
                case 205:
                case 396:
                case 397:
                case 460:
                case 461:
                    //TopLeftSingleCorner
                    return EnviromentPosition.TopLeftSingleCorner;
                case 421:
                case 229:
                case 481:
                case 165:
                case 417:
                case 225:
                case 161:
                case 485:
                    //TopRightSingleCorner
                    return EnviromentPosition.TopRightSingleCorner;
                case 47:
                case 111:
                case 303:
                case 367:
                    //BottomDeadEnd
                    return EnviromentPosition.BottomDeadEnd;
                case 203:
                case 207:
                case 459:
                case 463:
                    //LeftDeadEnd
                    return EnviromentPosition.LeftDeadEnd;
                case 325:
                    //Cross
                    return EnviromentPosition.Cross;
                case 422:
                case 423:
                case 486:
                case 487:
                    //RightDeadEnd
                    return EnviromentPosition.RightDeadEnd;
                case 488:
                case 489:
                case 492:
                case 493:
                    //TopDeadEnd
                    return EnviromentPosition.TopDeadEnd;
                case 322:
                case 323:
                case 326:
                case 327:
                    //BottomTIntersect
                    return EnviromentPosition.BottomTIntersect;
                case 268:
                case 269:
                case 332:
                case 333:
                    //LeftTIntersect
                    return EnviromentPosition.LeftTIntersect;
                case 97:
                case 101:
                case 353:
                case 357:
                    //RightTIntersect
                    return EnviromentPosition.RightTIntersect;
                case 133:
                case 197:
                case 389:
                case 453:
                    //TopTIntersect
                    return EnviromentPosition.TopTIntersect;
                case 320:
                    //BottomThickTIntersect
                    return EnviromentPosition.BottomThickTIntersect;
                case 260:
                    //LeftThickTIntersect
                    return EnviromentPosition.LeftThickTIntersect;
                case 65:
                    //RightThickTIntersect
                    return EnviromentPosition.RightThickTIntersect;
                case 5:
                    //TopThickTIntersect
                    return EnviromentPosition.TopThickTIntersect;
                case 256:
                    //BottomLeftInnerCorner
                    return EnviromentPosition.BottomLeftInnerCorner;
                case 64:
                    //BottomRightInnerCorner
                    return EnviromentPosition.BottomRightInnerCorner;
                case 4:
                    //TopLeftInnerCorner
                    return EnviromentPosition.TopLeftInnerCorner;
                case 1:
                    //TopRightInnerCorner
                    return EnviromentPosition.TopRightInnerCorner;
                case 329:
                    //BottomLeftInnerVertical
                    return EnviromentPosition.BottomLeftInnerCornerVerticalEdge;
                case 352:
                case 356:
                    //BottomRightInnerVertical
                    return EnviromentPosition.BottomRightInnerCornerVerticalEdge;
                case 77:
                    //TopLeftInnerVertical
                    return EnviromentPosition.TopLeftInnerCornerVerticalEdge;
                case 293:
                    //TopRightInnerVertical
                    return EnviromentPosition.TopRightInnerCornerVerticalEdge;
                case 263:
                    //BottomLeftCornerInnerHorizontial
                    return EnviromentPosition.BottomLeftInnerCornerHorizontialEdge;
                case 71:
                    //BottomRightInnerCornerHorizontial
                    return EnviromentPosition.BottomRightInnerCornerHorizontialEdge;
                case 452:
                    //TopLeftInnerCornerHorizontial
                    return EnviromentPosition.TopLeftInnerCornerHorizontialEdge;
                case 449:
                    //TopRightInnerCornerHorizontial
                    return EnviromentPosition.TopRightInnerCornerHorizontialEdge;
                case 69:
                    //BottomLeftTriple
                    return EnviromentPosition.BottomLeftTripleCorner;
                case 261:
                    //BottomRightTriple
                    return EnviromentPosition.BottomRightTripleCorner;
                case 321:
                    //TopLeftTriple
                    return EnviromentPosition.TopLeftTripleCorner;
                case 324:
                    //TopRightTriple
                    return EnviromentPosition.TopRightTripleCorner;
                case 257:
                    //LeftDouble
                    return EnviromentPosition.LeftDoubleCorner;
                case 68:
                    //RightDouble
                    return EnviromentPosition.RightDoubleCorner;
                default:
                    // Unspecified Case, so we'll just do a dot wall
                    return EnviromentPosition.Dot;
            }
        }
    }
}