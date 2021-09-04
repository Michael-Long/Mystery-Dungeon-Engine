using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.GameEngine.TileSpriteMapping
{
    public class SpriteLayoutSpawner : MonoBehaviour
    {

        private TileSpawnerManager manager;
        private TileSpawnerSpriteController wallSpriteControl;
        private TileSpawnerSpriteController floorSpriteControl;

        // Start is called before the first frame update
        void Start()
        {
            manager = FindObjectOfType<TileSpawnerManager>();
            wallSpriteControl = getSpriteControllerOfType(LayerMask.GetMask("Walls"));
            floorSpriteControl = getSpriteControllerOfType(LayerMask.GetMask("Floor"));
            LayerMask envMask = LayerMask.GetMask("Walls", "Floor"); // Need to add terrian

            float minX = manager.minX - 1;
            float maxX = manager.maxX + 1;
            float minY = manager.minY - 1;
            float maxY = manager.maxY + 1;
            for (float indexX = minX; indexX <= maxX; ++indexX)
            {
                for (float indexY = minY; indexY <= maxY; ++indexY)
                {
                    Vector2 targetPos = new Vector2(indexX, indexY);
                    Collider2D hit = Physics2D.OverlapBox(targetPos, Vector2.one * 0.8f, 0, envMask);
                    if (hit)
                    {
                        GameObject envObj = hit.gameObject;
                        if (envObj.layer == LayerMask.NameToLayer("Floor"))
                        {
                            long spriteIndex = calcSprite(targetPos, LayerMask.GetMask("Walls")); // Add Terrain?
                            List<Sprite> spriteList = convertCalcToSpritelist(spriteIndex, floorSpriteControl);
                            Sprite selectedSprite = spriteList[Random.Range(0, spriteList.Count)];
                            envObj.GetComponent<SpriteRenderer>().sprite = selectedSprite;
                        }
                    }
                    else
                    {
                        // Create Wall
                        long spriteIndex = calcSprite(targetPos, LayerMask.GetMask("Floor")); // Add Terrain
                        List<Sprite> spriteList = convertCalcToSpritelist(spriteIndex, wallSpriteControl);
                        Sprite selectedSprite = spriteList[Random.Range(0, spriteList.Count)];
                        GameObject goWall = Instantiate(manager.wallPrefab, targetPos, Quaternion.identity) as GameObject;
                        goWall.name = manager.wallPrefab.name + " - " + spriteIndex;
                        goWall.transform.SetParent(manager.transform);
                        goWall.GetComponent<SpriteRenderer>().sprite = selectedSprite;
                    }
                }
            }
        }

        private TileSpawnerSpriteController getSpriteControllerOfType(LayerMask selectLayer)
        {
            TileSpawnerSpriteController[] spriteControllers = FindObjectsOfType<TileSpawnerSpriteController>();
            foreach (TileSpawnerSpriteController control in spriteControllers)
            {
                if (control.SpritesetLayer == selectLayer)
                {
                    return control;
                }
            }
            return null;
        }

        private long calcSprite(Vector2 targetPos, LayerMask mask)
        {
            // Positioning format, a 3x3 grid with each section a different power of 2. This gives every combination of detection a unique value.
            // Used in connecting the found layout with a sprite.
            // 1  |  2  | 4
            // 8  |  16 | 32
            // 64 | 128 | 256
            double calc = 0;
            int powCount = 0;
            for (int y = -1; y <= 1; ++y)
            {
                for (int x = -1; x <= 1; ++x)
                {
                    Vector2 calcPos = new Vector2(targetPos.x + x, targetPos.y + y);
                    Collider2D hit = Physics2D.OverlapBox(calcPos, Vector2.one * 0.8f, 2, mask);
                    if (hit)
                    {
                        calc += System.Math.Pow(2, powCount);
                    }
                    ++powCount;
                }
            }
            return (long)calc;
        }

        private List<Sprite> convertCalcToSpritelist(long spriteIndex, TileSpawnerSpriteController spriteControl)
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
                    return spriteControl.getSpriteList(TileSheetPositions.BottomLeftCorner);
                case 2:
                case 3:
                case 7:
                case 6:
                    //Bottom
                    return spriteControl.getSpriteList(TileSheetPositions.Bottom);
                case 34:
                case 35:
                case 38:
                case 39:
                case 290:
                case 291:
                case 294:
                case 295:
                    //BottomRightCorner
                    return spriteControl.getSpriteList(TileSheetPositions.BottomRightCorner);
                case 8:
                case 9:
                case 73:
                case 72:
                    //Left
                    return spriteControl.getSpriteList(TileSheetPositions.Left);
                case 0:
                    //Middle
                    return spriteControl.getSpriteList(TileSheetPositions.Middle);
                case 32:
                case 36:
                case 292:
                case 288:
                    //Right
                    return spriteControl.getSpriteList(TileSheetPositions.Right);
                case 136:
                case 137:
                case 200:
                case 201:
                case 392:
                case 393:
                case 456:
                case 457:
                    // TopLeftCorner
                    return spriteControl.getSpriteList(TileSheetPositions.TopLeftCorner);
                case 128:
                case 192:
                case 448:
                case 384:
                    //Top
                    return spriteControl.getSpriteList(TileSheetPositions.Top);
                case 420:
                case 228:
                case 480:
                case 164:
                case 416:
                case 224:
                case 160:
                case 484:
                    //TopRightCorner
                    return spriteControl.getSpriteList(TileSheetPositions.TopRightCorner);
                case 266:
                case 267:
                case 270:
                case 271:
                case 330:
                case 331:
                case 334:
                case 335:
                    //BottomLeftSingleCorner
                    return spriteControl.getSpriteList(TileSheetPositions.BottomLeftSingleCorner);
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
                    return spriteControl.getSpriteList(TileSheetPositions.HorizontialSingle);
                case 98:
                case 99:
                case 102:
                case 103:
                case 354:
                case 355:
                case 358:
                case 359:
                    //BottomRightSingleCorner
                    return spriteControl.getSpriteList(TileSheetPositions.BottomRightSingleCorner);
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
                    return spriteControl.getSpriteList(TileSheetPositions.VerticalSingle);
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
                    return spriteControl.getSpriteList(TileSheetPositions.Dot);
                case 140:
                case 141:
                case 204:
                case 205:
                case 396:
                case 397:
                case 460:
                case 461:
                    //TopLeftSingleCorner
                    return spriteControl.getSpriteList(TileSheetPositions.TopLeftSingleCorner);
                case 421:
                case 229:
                case 481:
                case 165:
                case 417:
                case 225:
                case 161:
                case 485:
                    //TopRightSingleCorner
                    return spriteControl.getSpriteList(TileSheetPositions.TopRightSingleCorner);
                case 47:
                case 111:
                case 303:
                case 367:
                    //BottomDeadEnd
                    return spriteControl.getSpriteList(TileSheetPositions.BottomDeadEnd);
                case 203:
                case 207:
                case 459:
                case 463:
                    //LeftDeadEnd
                    return spriteControl.getSpriteList(TileSheetPositions.LeftDeadEnd);
                case 325:
                    //Cross
                    return spriteControl.getSpriteList(TileSheetPositions.Cross);
                case 422:
                case 423:
                case 486:
                case 487:
                    //RightDeadEnd
                    return spriteControl.getSpriteList(TileSheetPositions.RightDeadEnd);
                case 488:
                case 489:
                case 492:
                case 493:
                    //TopDeadEnd
                    return spriteControl.getSpriteList(TileSheetPositions.TopDeadEnd);
                case 322:
                case 323:
                case 326:
                case 327:
                    //BottomTIntersect
                    return spriteControl.getSpriteList(TileSheetPositions.BottomTIntersect);
                case 268:
                case 269:
                case 332:
                case 333:
                    //LeftTIntersect
                    return spriteControl.getSpriteList(TileSheetPositions.LeftTIntersect);
                case 97:
                case 101:
                case 353:
                case 357:
                    //RightTIntersect
                    return spriteControl.getSpriteList(TileSheetPositions.RightTIntersect);
                case 133:
                case 197:
                case 389:
                case 453:
                    //TopTIntersect
                    return spriteControl.getSpriteList(TileSheetPositions.TopTIntersect);
                case 320:
                    //BottomThickTIntersect
                    return spriteControl.getSpriteList(TileSheetPositions.BottomThickTIntersect);
                case 260:
                    //LeftThickTIntersect
                    return spriteControl.getSpriteList(TileSheetPositions.LeftThickTIntersect);
                case 65:
                    //RightThickTIntersect
                    return spriteControl.getSpriteList(TileSheetPositions.RightThickTIntersect);
                case 5:
                    //TopThickTIntersect
                    return spriteControl.getSpriteList(TileSheetPositions.TopThickTIntersect);
                case 256:
                    //BottomLeftInnerCorner
                    return spriteControl.getSpriteList(TileSheetPositions.BottomLeftInnerCorner);
                case 64:
                    //BottomRightInnerCorner
                    return spriteControl.getSpriteList(TileSheetPositions.BottomRightInnerCorner);
                case 4:
                    //TopLeftInnerCorner
                    return spriteControl.getSpriteList(TileSheetPositions.TopLeftInnerCorner);
                case 1:
                    //TopRightInnerCorner
                    return spriteControl.getSpriteList(TileSheetPositions.TopRightInnerCorner);
                case 329:
                    //BottomLeftInnerVertical
                    return spriteControl.getSpriteList(TileSheetPositions.BottomLeftInnerCornerVerticalEdge);
                case 352:
                case 356:
                    //BottomRightInnerVertical
                    return spriteControl.getSpriteList(TileSheetPositions.BottomRightInnerCornerVerticalEdge);
                case 77:
                    //TopLeftInnerVertical
                    return spriteControl.getSpriteList(TileSheetPositions.TopLeftInnerCornerVerticalEdge);
                case 293:
                    //TopRightInnerVertical
                    return spriteControl.getSpriteList(TileSheetPositions.TopRightInnerCornerVerticalEdge);
                case 263:
                    //BottomLeftCornerInnerHorizontial
                    return spriteControl.getSpriteList(TileSheetPositions.BottomLeftInnerCornerHorizontialEdge);
                case 71:
                    //BottomRightInnerCornerHorizontial
                    return spriteControl.getSpriteList(TileSheetPositions.BottomRightInnerCornerHorizontialEdge);
                case 452:
                    //TopLeftInnerCornerHorizontial
                    return spriteControl.getSpriteList(TileSheetPositions.TopLeftInnerCornerHorizontialEdge);
                case 449:
                    //TopRightInnerCornerHorizontial
                    return spriteControl.getSpriteList(TileSheetPositions.TopRightInnerCornerHorizontialEdge);
                case 69:
                    //BottomLeftTriple
                    return spriteControl.getSpriteList(TileSheetPositions.BottomLeftTripleCorner);
                case 261:
                    //BottomRightTriple
                    return spriteControl.getSpriteList(TileSheetPositions.BottomRightTripleCorner);
                case 321:
                    //TopLeftTriple
                    return spriteControl.getSpriteList(TileSheetPositions.TopLeftTripleCorner);
                case 324:
                    //TopRightTriple
                    return spriteControl.getSpriteList(TileSheetPositions.TopRightTripleCorner);
                case 257:
                    //LeftDouble
                    return spriteControl.getSpriteList(TileSheetPositions.LeftDoubleCorner);
                case 68:
                    //RightDouble
                    return spriteControl.getSpriteList(TileSheetPositions.RightDoubleCorner);
                default:
                    // Unspecified Case, so we'll just do a dot wall
                    return spriteControl.getSpriteList(TileSheetPositions.Dot);
            }
        }
    }
}
