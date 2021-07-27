using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using Assets.ObjectTypes.AnimationData;
using Assets.Logic.Animation;

namespace Assets.Editors
{
    [CustomPreview(typeof(LegacyAnimation))]
    public class AnimationPreview : ObjectPreview
    {

    // Here lies the attempts of adding a preview window to the LegacyAnimation class. The code is still here in case we
    // want to reimplement this, but there really isn't a need since ObjectField gives its own preview.

    //    LegacyAnimation SelectedAnimation = null;
    //    int SelectedGroup = 0;
    //    int SelectedDirection = 0;
    //    List<AnimationFrame> SelectedFrames = null;
    //    AnimationFrame ChangedFrame = null;

    //    public override void Initialize(Object[] targets)
    //    {
    //        base.Initialize(targets);

    //        SelectedAnimation = (LegacyAnimation)target;
    //    }

    //    public override bool HasPreviewGUI()
    //    {
    //        return true;
    //    }

    //    public override void OnPreviewGUI(Rect r, GUIStyle background)
    //    {
    //        if (SelectedAnimation == null)
    //            SelectedAnimation = (LegacyAnimation)target;
    //        // Praise to these threads
    //        // https://answers.unity.com/questions/953254/display-a-sprite-in-an-editorwindow.html
    //        // https://forum.unity.com/threads/flipping-texture2d-image-within-unity.35974/

    //        bool SelectedChanged = SelectedGroup != SelectedAnimation.selectedGroup || SelectedDirection != SelectedAnimation.selectedDirection;
    //        SelectedGroup = SelectedAnimation.selectedGroup;
    //        SelectedDirection = SelectedAnimation.selectedDirection;

    //        if (SelectedChanged)
    //        {
    //            SelectedFrames = SelectedAnimation.AnimationGroups[SelectedGroup].group.AnimationSequenceList[SelectedDirection].Frames;
    //        }

    //        var FrameSprite = AnimationHelper.getSpriteFromSheet(SelectedAnimation.Key, SelectedFrames[SelectedAnimation.changedFrame], false);

    //        if (SelectedFrames[SelectedAnimation.changedFrame].HorizFlip)
    //        {
    //            r.x = Screen.width;
    //            r.width *= -1;
    //        }
    //        GUI.DrawTextureWithTexCoords(r, FrameSprite.texture, getSubSpriteRect(FrameSprite));
    //    }

    //    private Rect getSubSpriteRect(Sprite sprite)
    //    {
    //        Rect spritePos = sprite.rect;
    //        spritePos.xMin /= sprite.texture.width;
    //        spritePos.xMax /= sprite.texture.width;
    //        spritePos.yMin /= sprite.texture.height;
    //        spritePos.yMax /= sprite.texture.height;
    //        return spritePos;
    //    }
        
    }
}