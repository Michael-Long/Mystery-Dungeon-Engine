using UnityEngine;
using UnityEditor;
using System.IO;

using Assets.ObjectTypes.Creature;
using Assets.GameEngine.Entities;
using Assets.Parsers;

namespace Assets.Editors
{
    public class BuildCreatureWindow : EditorWindow
    {
        private const string DATA_ASSET_DIRECTORY = "Assets/Database/Creatures/";
        private const string CREATURE_PREFAB_DIRECTORY = "Assets/Prefabs/Creatures/";

        public Sprite selectedSpritesheet = null;
        public string selectedXML = "";
        public string selectedCreatureName = "";

        [MenuItem("GameObject/Toolkit/Creature", false, 1)] // Change it's order priority
        private static void ShowWindow()
        {
            GetWindow<BuildCreatureWindow>("Build Creature");
        }

        private void OnGUI()
        {
            EditorStyles.label.wordWrap = true;

            string selectedSpritePath = "None Selected";
            if (selectedSpritesheet != null)
            {
                selectedSpritePath = AssetDatabase.GetAssetPath(selectedSpritesheet);
            }

            GUILayout.Label("Create a new Creature:");
            EditorGUILayout.LabelField("If you want to import from a spritesheet, find the spritesheet in your assets first and use the Sprite Editor (2D Sprite Package) to split it first.");
            EditorHelperMethods.DrawSpacer();

            selectedCreatureName = EditorGUILayout.TextField("Creature Name: ", selectedCreatureName);
            EditorGUILayout.LabelField("Spritesheet: " + selectedSpritePath);
            selectedSpritesheet = (Sprite)EditorGUILayout.ObjectField(selectedSpritesheet, typeof(Sprite), allowSceneObjects: false);

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
                if (selectedSpritesheet != null)
                {
                    CreateCreature();
                }
            }
            EditorGUILayout.Space();
            if (GUILayout.Button("Reset Fields"))
            {
                ResetFields();
            }
        }

        private void ResetFields()
        {
            selectedSpritesheet = null;
            selectedXML = "";
            selectedCreatureName = "";
        }

        private void CreateCreature()
        {
            string name = "New Creature";
            string assetName = "newCreature";
            if (!string.IsNullOrEmpty(selectedCreatureName))
            {
                name = assetName = selectedCreatureName;
            }
            else if (selectedXML != null)
            {
                name = assetName = Path.GetFileNameWithoutExtension(selectedXML);
            }
            else
            {
                assetName += "-" + Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(selectedSpritesheet));
            }

            string assetPath = CreateAsset(assetName);
            GameObject creature = CreateCreatureObject(name, assetPath);
            CreatePrefab(creature);
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

        private GameObject CreateCreatureObject(string name, string assetPath)
        {
            var obj = new GameObject(name);
            SpriteRenderer spriteRenderer = obj.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = selectedSpritesheet;

            CreatureData data = AssetDatabase.LoadAssetAtPath<CreatureData>(assetPath);
            if (File.Exists(selectedXML))
            {
                data = CreatureXMLParser.ParseCreatureData(selectedXML, data);
            }
            if (string.IsNullOrEmpty(selectedCreatureName) && !string.IsNullOrEmpty(data.species))
            {
                string creatureName = data.creatureNo + "_" + data.species;
                obj.name = creatureName;
            }
            Creature creatureItem = obj.AddComponent<Creature>();
            creatureItem.data = data;
            return obj;
        }

        private void CreatePrefab(GameObject creature)
        {
            string prefabPath = AssetDatabase.GenerateUniqueAssetPath(CREATURE_PREFAB_DIRECTORY + creature.name + ".prefab");

            if (!Directory.Exists(CREATURE_PREFAB_DIRECTORY))
            {
                Directory.CreateDirectory(CREATURE_PREFAB_DIRECTORY);
            }
            PrefabUtility.SaveAsPrefabAssetAndConnect(creature, prefabPath, InteractionMode.UserAction);
        }
    }
}