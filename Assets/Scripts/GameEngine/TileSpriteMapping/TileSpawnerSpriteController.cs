using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.GameEngine.TileSpriteMapping
{
    public class TileSpawnerSpriteController : MonoBehaviour
    {
        public LayerMask SpritesetLayer;
        public List<Sprite> TopLeftCornerSprites;
        public List<Sprite> TopSprites;
        public List<Sprite> TopRightCornerSprites;
        public List<Sprite> LeftSprites;
        public List<Sprite> MiddleSprites;
        public List<Sprite> RightSprites;
        public List<Sprite> BottomLeftCornerSprites;
        public List<Sprite> BottomSprites;
        public List<Sprite> BottomRightCornerSprites;
        public List<Sprite> TopLeftSingleCornerSprites;
        public List<Sprite> HorizontialSingleSprites;
        public List<Sprite> TopRightSingleCornerSprites;
        public List<Sprite> VericalSingleSprites;
        public List<Sprite> DotSprites;
        public List<Sprite> BottomLeftSingleCornerSprites;
        public List<Sprite> BottomRightSingleCornerSprites;
        public List<Sprite> TopDeadEndSprites;
        public List<Sprite> LeftDeadEndSprites;
        public List<Sprite> CrossSprites;
        public List<Sprite> RightDeadEndSprites;
        public List<Sprite> BottomDeadEndSprites;
        public List<Sprite> TopTIntersectSprites;
        public List<Sprite> LeftTIntersectSprites;
        public List<Sprite> RightTIntersectSprites;
        public List<Sprite> BottomTIntersectSprites;
        public List<Sprite> TopThickTIntersectSprites;
        public List<Sprite> LeftThickTIntersectSprites;
        public List<Sprite> RightThickTIntersectSprites;
        public List<Sprite> BottomThickTIntersectSprites;
        public List<Sprite> TopLeftInnerCornerSprites;
        public List<Sprite> TopRightInnerCornerSprites;
        public List<Sprite> BottomLeftInnerCornerSprites;
        public List<Sprite> BottomRightInnerCornerSprites;
        public List<Sprite> TopLeftInnerCornerVerticalEdgeSprites;
        public List<Sprite> TopRightInnerCornerVerticalEdgeSprites;
        public List<Sprite> BottomLeftInnerCornerVerticalEdgeSprites;
        public List<Sprite> BottomRightInnerCornerVerticalEdgeSprites;
        public List<Sprite> TopLeftInnerCornerHorizontialEdgeSprites;
        public List<Sprite> TopRightInnerCornerHorizontialEdgeSprites;
        public List<Sprite> BottomLeftInnerCornerHorizontialEdgeSprites;
        public List<Sprite> BottomRightInnerCornerHorizontialEdgeSprites;
        public List<Sprite> TopLeftTripleCornerSprites;
        public List<Sprite> TopRightTripleCornerSprites;
        public List<Sprite> BottomLeftTripleCornerSprites;
        public List<Sprite> BottomRightTriperCornerSprites;
        public List<Sprite> LeftDoubleCornerSprites;
        public List<Sprite> RightDoubleCornerSprites;

        public List<Sprite> getSpriteList(TileSheetPositions pos)
        {
            switch (pos)
            {
                case TileSheetPositions.TopLeftCorner:
                    return TopLeftCornerSprites;
                case TileSheetPositions.Top:
                    return TopSprites;
                case TileSheetPositions.TopRightCorner:
                    return TopRightCornerSprites;
                case TileSheetPositions.Left:
                    return LeftSprites;
                case TileSheetPositions.Middle:
                    return MiddleSprites;
                case TileSheetPositions.Right:
                    return RightSprites;
                case TileSheetPositions.BottomLeftCorner:
                    return BottomLeftCornerSprites;
                case TileSheetPositions.Bottom:
                    return BottomSprites;
                case TileSheetPositions.BottomRightCorner:
                    return BottomRightCornerSprites;
                case TileSheetPositions.TopLeftSingleCorner:
                    return TopLeftSingleCornerSprites;
                case TileSheetPositions.HorizontialSingle:
                    return HorizontialSingleSprites;
                case TileSheetPositions.TopRightSingleCorner:
                    return TopRightSingleCornerSprites;
                case TileSheetPositions.VerticalSingle:
                    return VericalSingleSprites;
                case TileSheetPositions.Dot:
                    return DotSprites;
                case TileSheetPositions.BottomLeftSingleCorner:
                    return BottomLeftSingleCornerSprites;
                case TileSheetPositions.BottomRightSingleCorner:
                    return BottomRightSingleCornerSprites;
                case TileSheetPositions.TopDeadEnd:
                    return TopDeadEndSprites;
                case TileSheetPositions.LeftDeadEnd:
                    return LeftDeadEndSprites;
                case TileSheetPositions.Cross:
                    return CrossSprites;
                case TileSheetPositions.RightDeadEnd:
                    return RightDeadEndSprites;
                case TileSheetPositions.BottomDeadEnd:
                    return BottomDeadEndSprites;
                case TileSheetPositions.TopTIntersect:
                    return TopTIntersectSprites;
                case TileSheetPositions.LeftTIntersect:
                    return LeftTIntersectSprites;
                case TileSheetPositions.RightTIntersect:
                    return RightTIntersectSprites;
                case TileSheetPositions.BottomTIntersect:
                    return BottomTIntersectSprites;
                case TileSheetPositions.TopThickTIntersect:
                    return TopThickTIntersectSprites;
                case TileSheetPositions.LeftThickTIntersect:
                    return LeftThickTIntersectSprites;
                case TileSheetPositions.RightThickTIntersect:
                    return RightThickTIntersectSprites;
                case TileSheetPositions.BottomThickTIntersect:
                    return BottomThickTIntersectSprites;
                case TileSheetPositions.TopLeftInnerCorner:
                    return TopLeftInnerCornerSprites;
                case TileSheetPositions.TopRightInnerCorner:
                    return TopRightInnerCornerSprites;
                case TileSheetPositions.BottomLeftInnerCorner:
                    return BottomLeftInnerCornerSprites;
                case TileSheetPositions.BottomRightInnerCorner:
                    return BottomRightInnerCornerSprites;
                case TileSheetPositions.TopLeftInnerCornerVerticalEdge:
                    return TopLeftInnerCornerVerticalEdgeSprites;
                case TileSheetPositions.TopRightInnerCornerVerticalEdge:
                    return TopRightInnerCornerVerticalEdgeSprites;
                case TileSheetPositions.BottomLeftInnerCornerVerticalEdge:
                    return BottomLeftInnerCornerVerticalEdgeSprites;
                case TileSheetPositions.BottomRightInnerCornerVerticalEdge:
                    return BottomRightInnerCornerVerticalEdgeSprites;
                case TileSheetPositions.TopLeftInnerCornerHorizontialEdge:
                    return TopLeftInnerCornerHorizontialEdgeSprites;
                case TileSheetPositions.TopRightInnerCornerHorizontialEdge:
                    return TopRightInnerCornerHorizontialEdgeSprites;
                case TileSheetPositions.BottomLeftInnerCornerHorizontialEdge:
                    return BottomLeftInnerCornerHorizontialEdgeSprites;
                case TileSheetPositions.BottomRightInnerCornerHorizontialEdge:
                    return BottomRightInnerCornerHorizontialEdgeSprites;
                case TileSheetPositions.TopLeftTripleCorner:
                    return TopLeftTripleCornerSprites;
                case TileSheetPositions.TopRightTripleCorner:
                    return TopRightTripleCornerSprites;
                case TileSheetPositions.BottomLeftTripleCorner:
                    return BottomLeftTripleCornerSprites;
                case TileSheetPositions.BottomRightTripleCorner:
                    return BottomRightTriperCornerSprites;
                case TileSheetPositions.LeftDoubleCorner:
                    return LeftDoubleCornerSprites;
                case TileSheetPositions.RightDoubleCorner:
                    return RightDoubleCornerSprites;
            }
            return MiddleSprites;
        }
    }
}