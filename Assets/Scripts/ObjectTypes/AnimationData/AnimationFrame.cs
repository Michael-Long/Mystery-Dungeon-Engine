using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.ObjectTypes.AnimationData
{
    [System.Serializable]
    public class AnimationFrame
    {
        [Tooltip("The sprite for this frame.")]
        public Sprite FrameSprite = null;
        [Tooltip("Duration of this frame. 1 frame = 1/60th of a second.")]
        public int Duration = 0;
        [HideInInspector]
        [Tooltip("Sprite Index - Indicates which sprite in the spritelist to grab, reading from TopLeft to BottomRight")]
        public int SpriteIndex = 0;
        [Tooltip("Sprite xPos - Gives the offset to draw this sprite from it's starting location in the x direction.")]
        public int spriteXPos = 0;
        [Tooltip("Sprite yPos - Gives the offset to draw this sprite from it's starting location in the y direction.")]
        public int spriteYPos = 0;
        [Tooltip("Horizontial Flip - Indicates if the given sprite should be flipped horizontially")]
        public bool HorizFlip = false;
        [Tooltip("Shadow xOffset - Gives an xPos offset to draw the shadow. Used for lunge-type animations")]
        public int shadowXOffset = 0;
        [Tooltip("Shadow yOffset - Gives an yPos offset to draw the shadow. Used for lunge-type animations")]
        public int shadowYOffset = 0;

        public AnimationFrame() { }
    }
}