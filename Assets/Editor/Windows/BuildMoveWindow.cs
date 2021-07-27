using UnityEngine;
using UnityEditor;
using System.IO;

using Assets.ObjectTypes.MoveData;
using Assets.Parsers;

namespace Assets.Editors
{
    class BuildMoveWindow : EditorWindow
    {
        private const string ASSET_DIRECTORY = "Assets/Database/Moves/";

        public string moveName = "";
        public string xmlFile = "";

        [MenuItem("Assets/Create/Toolkit/Move", false)]
        private static void ShowWindow()
        {
            GetWindow<BuildMoveWindow>("Build Move");
        }

        private void OnGUI()
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("XML File: ", Path.GetFileName(xmlFile));
            if (GUILayout.Button("Select XML File"))
            {
                SelectXMLFile();
            }
            GUILayout.EndHorizontal();

            moveName = EditorGUILayout.TextField("Move Name: ", moveName);

            EditorHelperMethods.DrawSpacer();
            if (GUILayout.Button("Create Move"))
            {
                if (!string.IsNullOrEmpty(moveName))
                {
                    CreateMove();
                }
            }

            EditorGUILayout.Space();
            if (GUILayout.Button("Reset Fields"))
            {
                ResetFields();
            }
        }

        private void SelectXMLFile()
        {
            bool nameIsDefault = string.IsNullOrEmpty(moveName) || moveName == Path.GetFileNameWithoutExtension(xmlFile);
            xmlFile = EditorUtility.OpenFilePanel("Select XML File...", "", "xml");
            if (nameIsDefault)
            {
                moveName = Path.GetFileNameWithoutExtension(xmlFile);
            }
        }

        private void ResetFields()
        {
            moveName = "";
            xmlFile = "";
        }

        private void CreateMove()
        {
            if (!Directory.Exists(ASSET_DIRECTORY))
            {
                Directory.CreateDirectory(ASSET_DIRECTORY);
            }
            var move = CreateInstance<BaseMove>();
            if (File.Exists(xmlFile))
            {
                MoveXMLParser.ParseMoveData(xmlFile, move);
            }
            if (!string.IsNullOrEmpty(moveName) && moveName != Path.GetFileNameWithoutExtension(xmlFile))
            {
                move.name = moveName;
                move.inGameText.moveName = moveName;
            }
            else
            {
                move.name = move.inGameText.moveName;
            }
            string movePathName = move.id + "_" + move.name;
            string assetPath = AssetDatabase.GenerateUniqueAssetPath(ASSET_DIRECTORY + movePathName + ".asset");
            AssetDatabase.CreateAsset(move, assetPath);
        }
    }
}