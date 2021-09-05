using UnityEngine;
using UnityEditor;

using Assets.ObjectTypes.AnimationData;

namespace Assets.Utility
{
    public class AnimationHelper
    {
        // This function is Editor-Only, as it is using AssetDatabase
        public static Sprite getSpriteFromSheet(string animKey, AnimationFrame frame, bool isShiny)
        {
            string shinyAppend = isShiny ? "_shiny" : "";
            string spritesheetName = animKey + "_sheet";
            spritesheetName = spritesheetName.Substring(spritesheetName.IndexOf("Assets/Visuals/Creatures")) + shinyAppend + ".png";

            // The spritesheet MUST be spliced first before this can process it correctly!
            Object[] sprites = AssetDatabase.LoadAllAssetRepresentationsAtPath(spritesheetName);
            if (sprites.Length == 0)
            {
                return null;
            }

            int index = frame.SpriteIndex;
            if (index < 0 || index > sprites.Length)
            {
                Debug.LogWarning("Invalid Sprite Value found: " + index);
                return null;
            }
            return (Sprite)sprites[index];
        }

        public static AnimationDirection convertMovementToDirection(Vector2 currPos, Vector2 targetPos)
        {
            float deltaX = currPos.x - targetPos.x;
            float deltaY = currPos.y - targetPos.y;

            if (deltaX == 0)
            {
                if (deltaY == 0)
                    return AnimationDirection.Bottom;
                if (deltaY < 0)
                    return AnimationDirection.Top;
                return AnimationDirection.Bottom;
            }
            if (deltaX < 0)
            {
                if (deltaY == 0)
                    return AnimationDirection.Right;
                if (deltaY < 0)
                    return AnimationDirection.TopRight;
                return AnimationDirection.BottomRight;
            }
            if (deltaY == 0)
                return AnimationDirection.Left;
            if (deltaY < 0)
                return AnimationDirection.TopLeft;
            return AnimationDirection.BottomLeft;
        }
    }
}