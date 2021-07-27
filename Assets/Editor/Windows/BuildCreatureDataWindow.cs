using UnityEngine;
using UnityEditor;
using System.IO;

using Assets.ObjectTypes.Creature;
using Assets.Parsers;

namespace Assets.Editors
{
    public class BuildCreatureDataWindow : EditorWindow
    {
        private const string DATA_ASSET_DIRECTORY = "Assets/Database/Creatures/";

        public string selectedXML = "";

        [MenuItem("Assets/Create/Toolkit/Creature", false, 1)]
        private static void ShowWindow()
        {
            GetWindow<BuildCreatureDataWindow>("Build Creature");
        }

        private void OnGUI()
        {
            EditorStyles.label.wordWrap = true;

            GUILayout.Label("Create a new Creature:");
            EditorHelperMethods.DrawSpacer();

            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("XML File: ", Path.GetFileName(selectedXML));
            if (GUILayout.Button("Select XML File"))
            {
                selectedXML = EditorUtility.OpenFilePanel("Select XML File...", "", "xml");
            }
            GUILayout.EndHorizontal();

            EditorHelperMethods.DrawSpacer();
            if (GUILayout.Button("Create Creature"))
            {
                CreateCreature();
            }
        }

        private void CreateCreature()
        {
            string assetName = "newCreature";
            if (selectedXML != null)
            {
                assetName = Path.GetFileNameWithoutExtension(selectedXML);
            }

            string assetPath = CreateAsset(assetName);
            FillCreatureData(assetPath);
        }

        private string CreateAsset(string assetName)
        {
            if (!Directory.Exists(DATA_ASSET_DIRECTORY))
            {
                Directory.CreateDirectory(DATA_ASSET_DIRECTORY);
            }
            string assetPath = AssetDatabase.GenerateUniqueAssetPath(DATA_ASSET_DIRECTORY + assetName + ".asset");
            AssetDatabase.CreateAsset(CreateInstance<CreatureData>(), assetPath);
            return assetPath;
        }

        private void FillCreatureData(string assetPath)
        {

            CreatureData data = AssetDatabase.LoadAssetAtPath<CreatureData>(assetPath);
            if (File.Exists(selectedXML))
            {
                CreatureXMLParser.ParseCreatureData(selectedXML, data);
            }
        }
    }
}