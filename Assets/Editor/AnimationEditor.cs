using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using Assets.ObjectTypes.AnimationData;

namespace Assets.Editors
{
    [CustomEditor(typeof(LegacyAnimation))]
    public class AnimationEditor : Editor
    {
        private static bool showDebugFields = false;

        SerializedProperty Name;
        SerializedProperty FrameWidth;
        SerializedProperty FrameHeight;
        SerializedProperty animationList;
        SerializedProperty animationGroup;
        SerializedProperty animationFrames;

        string[] animationTypes = Enum.GetNames(typeof(AnimationType));
        SerializedProperty selectedType;

        string[] animationDirections = Enum.GetNames(typeof(AnimationDirection));
        SerializedProperty selectedDirection;

        public void OnEnable()
        {
            Name = serializedObject.FindProperty("Name");
            FrameWidth = serializedObject.FindProperty("FrameWidth");
            FrameHeight = serializedObject.FindProperty("FrameHeight");
            animationList = serializedObject.FindProperty("AnimationGroups");

            selectedType = serializedObject.FindProperty("selectedGroup");
            selectedDirection = serializedObject.FindProperty("selectedDirection");
            animationGroup = animationList.GetArrayElementAtIndex(selectedType.intValue).FindPropertyRelative("group");
            animationFrames = animationGroup.FindPropertyRelative("AnimationSequenceList").GetArrayElementAtIndex(selectedDirection.intValue).FindPropertyRelative("Frames");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            int prevType = selectedType.intValue;
            int prevDirection = selectedDirection.intValue;
            GUILayout.Label("General Animation Settings");
            Name.stringValue = EditorGUILayout.TextField("Associated Creature Name:", Name.stringValue);
            FrameWidth.intValue = EditorGUILayout.IntField("Animation Width:", FrameWidth.intValue);
            FrameHeight.intValue = EditorGUILayout.IntField("Animation Height:", FrameHeight.intValue);

            EditorHelperMethods.DrawSpacer();
            GUILayout.Label("Group & Direction Selection");
            GUILayout.BeginHorizontal();
            GUILayout.Label("Selected Animation Type:");
            selectedType.intValue = EditorGUILayout.Popup(selectedType.intValue, animationTypes);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Selected Animation Direction:");
            selectedDirection.intValue = EditorGUILayout.Popup(selectedDirection.intValue, animationDirections);
            GUILayout.EndHorizontal();

            EditorHelperMethods.DrawSpacer();
            GUILayout.Label("Selected Group & Direction Settings");
            if (prevType != selectedType.intValue || prevDirection != selectedDirection.intValue)
            {
                animationGroup = animationList.GetArrayElementAtIndex(selectedType.intValue).FindPropertyRelative("group");
                animationFrames = animationGroup.FindPropertyRelative("AnimationSequenceList").GetArrayElementAtIndex(selectedDirection.intValue).FindPropertyRelative("Frames");
            }
            animationFrames.arraySize = EditorGUILayout.IntField("Frame Count:", animationFrames.arraySize);
            GUILayout.Space(5);
            for (int frameIndex = 0; frameIndex < animationFrames.arraySize; ++frameIndex)
            {
                SerializedProperty frame = animationFrames.GetArrayElementAtIndex(frameIndex);
                GUILayout.Label("Frame #" + (frameIndex + 1) + ":");
                using (var cHorizontalScope = new GUILayout.HorizontalScope())
                {
                    GUILayout.Space(20);
                    using (var cVerticalScope = new GUILayout.VerticalScope())
                    {
                        frame.FindPropertyRelative("FrameSprite").objectReferenceValue = EditorGUILayout.ObjectField("Frame Sprite:", frame.FindPropertyRelative("FrameSprite").objectReferenceValue, typeof(Sprite), true);
                        if (showDebugFields)
                            frame.FindPropertyRelative("SpriteIndex").intValue = EditorGUILayout.IntField("(Debug) Sprite Index:", frame.FindPropertyRelative("SpriteIndex").intValue);
                        frame.FindPropertyRelative("Duration").intValue = EditorGUILayout.IntField("Frame Duration:", frame.FindPropertyRelative("Duration").intValue);
                        frame.FindPropertyRelative("HorizFlip").boolValue = EditorGUILayout.Toggle("Flip Sprite Horizontially?", frame.FindPropertyRelative("HorizFlip").boolValue);
                        GUILayout.Label("Sprite Offset:");
                        EditorGUI.indentLevel = 1;
                        frame.FindPropertyRelative("spriteXPos").intValue = EditorGUILayout.IntField("X:", frame.FindPropertyRelative("spriteXPos").intValue);
                        frame.FindPropertyRelative("spriteYPos").intValue = EditorGUILayout.IntField("Y:", frame.FindPropertyRelative("spriteYPos").intValue);
                        EditorGUI.indentLevel = 0;
                        GUILayout.Label("Shadow Offset:");
                        EditorGUI.indentLevel = 1;
                        frame.FindPropertyRelative("shadowXOffset").intValue = EditorGUILayout.IntField("X:", frame.FindPropertyRelative("shadowXOffset").intValue);
                        frame.FindPropertyRelative("shadowYOffset").intValue = EditorGUILayout.IntField("Y:", frame.FindPropertyRelative("shadowYOffset").intValue);
                        EditorGUI.indentLevel = 0;
                        GUILayout.Space(5);
                    }
                }
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}