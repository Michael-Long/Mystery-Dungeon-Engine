using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.GameEngine.DungeonEngine.DungeonGen.Enviroment
{
    public class EnviromentMap : MonoBehaviour
    {
        public EnviromentType type;

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

        public List<Sprite> getSpriteList(EnviromentPosition pos)
        {
            switch (pos)
            {
                case EnviromentPosition.TopLeftCorner:
                    return TopLeftCornerSprites;
                case EnviromentPosition.Top:
                    return TopSprites;
                case EnviromentPosition.TopRightCorner:
                    return TopRightCornerSprites;
                case EnviromentPosition.Left:
                    return LeftSprites;
                case EnviromentPosition.Middle:
                    return MiddleSprites;
                case EnviromentPosition.Right:
                    return RightSprites;
                case EnviromentPosition.BottomLeftCorner:
                    return BottomLeftCornerSprites;
                case EnviromentPosition.Bottom:
                    return BottomSprites;
                case EnviromentPosition.BottomRightCorner:
                    return BottomRightCornerSprites;
                case EnviromentPosition.TopLeftSingleCorner:
                    return TopLeftSingleCornerSprites;
                case EnviromentPosition.HorizontialSingle:
                    return HorizontialSingleSprites;
                case EnviromentPosition.TopRightSingleCorner:
                    return TopRightSingleCornerSprites;
                case EnviromentPosition.VerticalSingle:
                    return VericalSingleSprites;
                case EnviromentPosition.Dot:
                    return DotSprites;
                case EnviromentPosition.BottomLeftSingleCorner:
                    return BottomLeftSingleCornerSprites;
                case EnviromentPosition.BottomRightSingleCorner:
                    return BottomRightSingleCornerSprites;
                case EnviromentPosition.TopDeadEnd:
                    return TopDeadEndSprites;
                case EnviromentPosition.LeftDeadEnd:
                    return LeftDeadEndSprites;
                case EnviromentPosition.Cross:
                    return CrossSprites;
                case EnviromentPosition.RightDeadEnd:
                    return RightDeadEndSprites;
                case EnviromentPosition.BottomDeadEnd:
                    return BottomDeadEndSprites;
                case EnviromentPosition.TopTIntersect:
                    return TopTIntersectSprites;
                case EnviromentPosition.LeftTIntersect:
                    return LeftTIntersectSprites;
                case EnviromentPosition.RightTIntersect:
                    return RightTIntersectSprites;
                case EnviromentPosition.BottomTIntersect:
                    return BottomTIntersectSprites;
                case EnviromentPosition.TopThickTIntersect:
                    return TopThickTIntersectSprites;
                case EnviromentPosition.LeftThickTIntersect:
                    return LeftThickTIntersectSprites;
                case EnviromentPosition.RightThickTIntersect:
                    return RightThickTIntersectSprites;
                case EnviromentPosition.BottomThickTIntersect:
                    return BottomThickTIntersectSprites;
                case EnviromentPosition.TopLeftInnerCorner:
                    return TopLeftInnerCornerSprites;
                case EnviromentPosition.TopRightInnerCorner:
                    return TopRightInnerCornerSprites;
                case EnviromentPosition.BottomLeftInnerCorner:
                    return BottomLeftInnerCornerSprites;
                case EnviromentPosition.BottomRightInnerCorner:
                    return BottomRightInnerCornerSprites;
                case EnviromentPosition.TopLeftInnerCornerVerticalEdge:
                    return TopLeftInnerCornerVerticalEdgeSprites;
                case EnviromentPosition.TopRightInnerCornerVerticalEdge:
                    return TopRightInnerCornerVerticalEdgeSprites;
                case EnviromentPosition.BottomLeftInnerCornerVerticalEdge:
                    return BottomLeftInnerCornerVerticalEdgeSprites;
                case EnviromentPosition.BottomRightInnerCornerVerticalEdge:
                    return BottomRightInnerCornerVerticalEdgeSprites;
                case EnviromentPosition.TopLeftInnerCornerHorizontialEdge:
                    return TopLeftInnerCornerHorizontialEdgeSprites;
                case EnviromentPosition.TopRightInnerCornerHorizontialEdge:
                    return TopRightInnerCornerHorizontialEdgeSprites;
                case EnviromentPosition.BottomLeftInnerCornerHorizontialEdge:
                    return BottomLeftInnerCornerHorizontialEdgeSprites;
                case EnviromentPosition.BottomRightInnerCornerHorizontialEdge:
                    return BottomRightInnerCornerHorizontialEdgeSprites;
                case EnviromentPosition.TopLeftTripleCorner:
                    return TopLeftTripleCornerSprites;
                case EnviromentPosition.TopRightTripleCorner:
                    return TopRightTripleCornerSprites;
                case EnviromentPosition.BottomLeftTripleCorner:
                    return BottomLeftTripleCornerSprites;
                case EnviromentPosition.BottomRightTripleCorner:
                    return BottomRightTriperCornerSprites;
                case EnviromentPosition.LeftDoubleCorner:
                    return LeftDoubleCornerSprites;
                case EnviromentPosition.RightDoubleCorner:
                    return RightDoubleCornerSprites;
            }
            return MiddleSprites;
        }
    }
}