using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

using Assets.ObjectTypes.AnimationData;
using Assets.Parsers;

namespace Assets.Editors
{
    public class BuildAnimationWindow : EditorWindow
    {
        private const string ANIMATION_ASSET_DIRECTORY = "Assets/Database/Animations/Creatures/";

        public string selectedXML = "";
        public string animName = "";
        public bool isShiny = false;

        [MenuItem("Assets/Create/Toolkit/Legacy Animation", false, 20)]
        private static void ShowWindow()
        {
            GetWindow<BuildAnimationWindow>("Import Animation");
        }

        private void OnGUI()
        {
            EditorStyles.label.wordWrap = true;

            GUILayout.Label("Import an animation.xml file:");
            EditorHelperMethods.DrawSpacer();
            animName = EditorGUILayout.TextField("Animation Name:", animName);
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("XML File: ", Path.GetFileName(selectedXML));
            GUILayout.EndHorizontal();
            if (GUILayout.Button("Select XML File..."))
            {
                selectedXML = EditorUtility.OpenFilePanel("Select XML File...", "", "xml");
            }
            isShiny = EditorGUILayout.Toggle("Use Shiny Sprite?", isShiny);
            EditorHelperMethods.DrawSpacer();

            if (GUILayout.Button("New Blank Animation"))
            {
                if (!string.IsNullOrWhiteSpace(animName))
                {
                    CreateNewAnimation();
                } else
                {
                    Debug.LogWarning("No Name Specified for Animation. Can't create a new one without a name.");
                }
            }

            if (GUILayout.Button("Import Animation"))
            {
                if (!string.IsNullOrEmpty(selectedXML) && File.Exists(selectedXML))
                {
                    CreateAnimation();
                }
                else
                {
                    Debug.LogWarning("Couldn't find XML File: " + selectedXML);
                }
            }

            EditorGUILayout.Space();
            if (GUILayout.Button("Reset"))
            {
                ResetFields();
            }
        }

        private void ResetFields()
        {
            selectedXML = "";
        }

        private void CreateAnimation()
        {
            if (!File.Exists(selectedXML))
            {
                return;
            }

            string shinyAppend = isShiny ? "_shiny" : "";

            LegacyAnimation importedAnimation = CreateInstance<LegacyAnimation>();

            if (!string.IsNullOrWhiteSpace(animName))
                importedAnimation.Name = animName;
            importedAnimation = AnimationXMLParser.ParseAnimationData(selectedXML, importedAnimation);
            importedAnimation.Key = selectedXML.Substring(0, selectedXML.LastIndexOf('_'));
            foreach (var animTypedGroup in importedAnimation.AnimationGroups)
            {
                foreach (var animSequence in animTypedGroup.group.AnimationSequenceList)
                {
                    foreach (var animFrame in animSequence.Frames)
                    {
                        animFrame.FrameSprite = AnimationHelper.getSpriteFromSheet(importedAnimation.Key, animFrame, isShiny);
                    }
                }
            }

            if (!Directory.Exists(ANIMATION_ASSET_DIRECTORY))
            {
                Directory.CreateDirectory(ANIMATION_ASSET_DIRECTORY);
            }
            string animationPath = AssetDatabase.GenerateUniqueAssetPath(ANIMATION_ASSET_DIRECTORY + importedAnimation.Key.Substring(importedAnimation.Key.LastIndexOf("/") + 1) + shinyAppend + "_legacyAnimation.asset");
            AssetDatabase.CreateAsset(importedAnimation, animationPath);
        }

        private void CreateNewAnimation()
        {
            LegacyAnimation newAnimation = CreateInstance<LegacyAnimation>();
            newAnimation.initializeLists();
            newAnimation.Name = animName;

            if (!Directory.Exists(ANIMATION_ASSET_DIRECTORY))
            {
                Directory.CreateDirectory(ANIMATION_ASSET_DIRECTORY);
            }
            string animationPath = AssetDatabase.GenerateUniqueAssetPath(ANIMATION_ASSET_DIRECTORY + animName + "_legacyAnimation.asset");
            AssetDatabase.CreateAsset(newAnimation, animationPath);
        }
    }
}