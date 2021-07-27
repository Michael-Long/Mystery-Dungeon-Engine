using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

using Assets.ObjectTypes;
using Assets.ObjectTypes.Creature;
using Assets.ObjectTypes.MoveData;
using Assets.ObjectTypes.ItemData;
using Assets.ObjectTypes.AnimationData;

namespace Assets.GameEngine
{
    // This is a way of querying our Data folder for various different groups of static data. These searches can be chained together by passing in a previously made List back into
    // a search. This is a growing list of searches, and we'll prob add onto it as we find more use-cases.
    public class DatabaseManager
    {
        private const string CREATURE_ASSET_DIRECTORY = "Assets/Database/Creatures/";
        private const string MOVE_ASSET_DIRECTORY = "Assets/Database/Moves/";
        private const string ITEM_ASSET_DIRECTORY = "Assets/Database/Items/";
        //Commenting this out for now since we don't have animations in Resources.
        //During review let me know if I should uncomment and add animations to resources or if I should remove this line and associated functions
        //private const string ANIMATION_ASSET_DIRECTORY = "Assets/Resources/GameData/Animations/";

        /*
         * If you're wanting to make your own search, try to use the following template:

        public static List<STATICTYPE> GetMETHODNAME([x amount of parameters can be put here], List<STATICTYPE> currentList = null)
        {
            System.Predicate<STATICTYPE> keepFilter = asset =>
            {
                // Insert Filter condition here, should return true if you want it to be in the returned list, false if to ignore/remove from list.
            };

            if (currentList != null)
                return FilterAssetList(currentList, keepFilter);

            return GetNewAssetList(GetFileListOfDatabase(STATICTYPE_ASSET_DIRECTORY), keepFilter);
        }
         */

        // --- Creature Methods ---

        public static List<CreatureData> GetAllCreatureData()
        {
            return GetNewAssetList<CreatureData>(GetFileListOfDatabase(CREATURE_ASSET_DIRECTORY), creature =>
            {
                return true;
            });
        }

        public static List<CreatureData> GetCreatureDataWithID(int ID, List<CreatureData> currentList = null)
        {
            System.Predicate<CreatureData> keepFilter = creature =>
            {
                return creature.creatureNo == ID;
            };

            if (currentList != null)
                return FilterAssetList(currentList, keepFilter);
            
            return GetNewAssetList(GetFileListOfDatabase(CREATURE_ASSET_DIRECTORY), keepFilter);
        }

        public static List<CreatureData> GetCreatureDataWithIDs(List<int> IDList, List<CreatureData> currentList = null)
        {
            System.Predicate<CreatureData> keepFilter = creature =>
            {
                return IDList.Contains(creature.creatureNo);
            };

            if (currentList != null)
                return FilterAssetList(currentList, keepFilter);
            
            return GetNewAssetList(GetFileListOfDatabase(CREATURE_ASSET_DIRECTORY), keepFilter);
        }

        public static List<CreatureData> GetCreatureDataWithName(string name, List<CreatureData> currentList = null)
        {
            System.Predicate<CreatureData> keepFilter = creature =>
            {
                return creature.species == name;
            };

            if (currentList != null)
                return FilterAssetList(currentList, keepFilter);
            
            return GetNewAssetList(GetFileListOfDatabase(CREATURE_ASSET_DIRECTORY), keepFilter);
        }

        public static List<CreatureData> GetCreatureDataWithNames(List<string> nameList, List<CreatureData> currentList = null)
        {
            System.Predicate<CreatureData> keepFilter = creature =>
            {
                return nameList.Contains(creature.species);
            };

            if (currentList != null)
                return FilterAssetList(currentList, keepFilter);
            
            return GetNewAssetList(GetFileListOfDatabase(CREATURE_ASSET_DIRECTORY), keepFilter);
        }

        public static List<CreatureData> GetCreatureDataWithType(ElementType type, bool includeSecondaryType = true, List<CreatureData> currentList = null)
        {
            System.Predicate<CreatureData> keepFilter = creature =>
            {
                return creature.primaryType == type || (includeSecondaryType && creature.secondaryType == type);
            };

            if (currentList != null)
                return FilterAssetList(currentList, keepFilter);
            
            return GetNewAssetList(GetFileListOfDatabase(CREATURE_ASSET_DIRECTORY), keepFilter);
        }

        public static List<CreatureData> GetCreatureDataWithTypes(List<ElementType> typeList, bool includeSecondaryType = true, List<CreatureData> currentList = null)
        {
            System.Predicate<CreatureData> keepFilter = creature =>
            {
                return typeList.Contains(creature.primaryType) || (includeSecondaryType && typeList.Contains(creature.secondaryType));
            };

            if (currentList != null)
                return FilterAssetList(currentList, keepFilter);
            
            return GetNewAssetList(GetFileListOfDatabase(CREATURE_ASSET_DIRECTORY), keepFilter);
        }

        // --- Move Methods ---

        public static List<BaseMove> GetAllMoves()
        {
            return GetNewAssetList<BaseMove>(GetFileListOfDatabase(MOVE_ASSET_DIRECTORY), move =>
            {
                return true;
            });
        }

        public static List<BaseMove> GetMovesWithID(int ID, List<BaseMove> currentList = null)
        {
            System.Predicate<BaseMove> keepFilter = move =>
            {
                return move.id == ID;
            };

            if (currentList != null)
                return FilterAssetList(currentList, keepFilter);
            
            return GetNewAssetList(GetFileListOfDatabase(MOVE_ASSET_DIRECTORY), keepFilter);
        }

        public static List<BaseMove> GetMovesWithIDs(List<int> IDList, List<BaseMove> currentList = null)
        {
            System.Predicate<BaseMove> keepFilter = move =>
            {
                return IDList.Contains(move.id);
            };

            if (currentList != null)
                return FilterAssetList(currentList, keepFilter);

            return GetNewAssetList(GetFileListOfDatabase(MOVE_ASSET_DIRECTORY), keepFilter);
        }

        public static List<BaseMove> GetMovesWithName(string name, List<BaseMove> currentList = null)
        {
            System.Predicate<BaseMove> keepFilter = move =>
            {
                return move.inGameText.moveName == name;
            };

            if (currentList != null)
                return FilterAssetList(currentList, keepFilter);

            return GetNewAssetList(GetFileListOfDatabase(MOVE_ASSET_DIRECTORY), keepFilter);
        }

        public static List<BaseMove> GetMovesWithNames(List<string> nameList, List<BaseMove> currentList = null)
        {
            System.Predicate<BaseMove> keepFilter = move =>
            {
                return nameList.Contains(move.inGameText.moveName);
            };

            if (currentList != null)
                return FilterAssetList(currentList, keepFilter);

            return GetNewAssetList(GetFileListOfDatabase(MOVE_ASSET_DIRECTORY), keepFilter);
        }

        public static List<BaseMove> GetMovesWithType(ElementType type, List<BaseMove> currentList = null)
        {
            System.Predicate<BaseMove> keepFilter = move =>
            {
                return move.primaryType == type;
            };

            if (currentList != null)
                return FilterAssetList(currentList, keepFilter);

            return GetNewAssetList(GetFileListOfDatabase(MOVE_ASSET_DIRECTORY), keepFilter);
        }

        public static List<BaseMove> GetMovesWithTypes(List<ElementType> typeList, List<BaseMove> currentList = null)
        {
            System.Predicate<BaseMove> keepFilter = move =>
            {
                return typeList.Contains(move.primaryType);
            };

            if (currentList != null)
                return FilterAssetList(currentList, keepFilter);

            return GetNewAssetList(GetFileListOfDatabase(MOVE_ASSET_DIRECTORY), keepFilter);
        }

        // --- Item Methods ---

        public static List<ItemData> GetAllItems()
        {
            return GetNewAssetList<ItemData>(GetFileListOfDatabase(ITEM_ASSET_DIRECTORY), item =>
            {
                return true;
            });
        }

        public static List<ItemData> GetItemsWithID(int ID, List<ItemData> currentList = null)
        {
            System.Predicate<ItemData> keepFilter = item =>
            {
                return item.id == ID;
            };

            if (currentList != null)
                return FilterAssetList(currentList, keepFilter);

            return GetNewAssetList(GetFileListOfDatabase(ITEM_ASSET_DIRECTORY), keepFilter);
        }

        public static List<ItemData> GetItemsWithID(List<int> IDList, List<ItemData> currentList = null)
        {
            System.Predicate<ItemData> keepFilter = item =>
            {
                return IDList.Contains(item.id);
            };

            if (currentList != null)
                return FilterAssetList(currentList, keepFilter);

            return GetNewAssetList(GetFileListOfDatabase(ITEM_ASSET_DIRECTORY), keepFilter);
        }

        public static List<ItemData> GetItemsWithName(string name, List<ItemData> currentList = null)
        {
            System.Predicate<ItemData> keepFilter = item =>
            {
                return item.itemName == name;
            };

            if (currentList != null)
                return FilterAssetList(currentList, keepFilter);

            return GetNewAssetList(GetFileListOfDatabase(ITEM_ASSET_DIRECTORY), keepFilter);
        }

        public static List<ItemData> GetItemsWithNames(List<string> nameList, List<ItemData> currentList = null)
        {
            System.Predicate<ItemData> keepFilter = item =>
            {
                return nameList.Contains(item.itemName);
            };

            if (currentList != null)
                return FilterAssetList(currentList, keepFilter);

            return GetNewAssetList(GetFileListOfDatabase(ITEM_ASSET_DIRECTORY), keepFilter);
        }

        public static List<ItemData> GetItemsWithItemType(ItemType itemType, List<ItemData> currentList = null)
        {
            System.Predicate<ItemData> keepFilter = item =>
            {
                return item.itemType == itemType;
            };

            if (currentList != null)
                return FilterAssetList(currentList, keepFilter);

            return GetNewAssetList(GetFileListOfDatabase(ITEM_ASSET_DIRECTORY), keepFilter);
        }

        public static List<ItemData> GetItemsWithItemTypes(List<ItemType> itemTypeList, List<ItemData> currentList = null)
        {
            System.Predicate<ItemData> keepFilter = item =>
            {
                return itemTypeList.Contains(item.itemType);
            };

            if (currentList != null)
                return FilterAssetList(currentList, keepFilter);

            return GetNewAssetList(GetFileListOfDatabase(ITEM_ASSET_DIRECTORY), keepFilter);
        }

        // -- Animation Methods ---

        // public static List<LegacyAnimation> GetAllAnimations()
        // {
        //     return GetNewAssetList<LegacyAnimation>(GetFileListOfDatabase(ANIMATION_ASSET_DIRECTORY), animation =>
        //     {
        //         return true;
        //     });
        // }

        // public static List<LegacyAnimation> GetAnimationsWithName(string name, List<LegacyAnimation> currentList = null)
        // {
        //     System.Predicate<LegacyAnimation> keepFilter = animation =>
        //     {
        //         return animation.Name == name;
        //     };

        //     if (currentList != null)
        //         return FilterAssetList(currentList, keepFilter);

        //     return GetNewAssetList(GetFileListOfDatabase(ANIMATION_ASSET_DIRECTORY), keepFilter);
        // }

        // public static List<LegacyAnimation> GetAnimationsWithNames(List<string> nameList, List<LegacyAnimation> currentList = null)
        // {
        //     System.Predicate<LegacyAnimation> keepFilter = animation =>
        //     {
        //         return nameList.Contains(animation.Name);
        //     };

        //     if (currentList != null)
        //         return FilterAssetList(currentList, keepFilter);

        //     return GetNewAssetList(GetFileListOfDatabase(ANIMATION_ASSET_DIRECTORY), keepFilter);
        // }

        // -- Private Helper Methods --

        private static List<string> GetFileListOfDatabase(string databasePath)
        {
            return Directory.EnumerateFiles(databasePath).Where(file => file.ToLower().EndsWith(".asset")).ToList();
        }

        private static List<T> GetNewAssetList<T>(List<string> filePaths, System.Predicate<T> filter) where T : Object
        {
            List<T> assetList = new List<T>();
            foreach (string filePath in filePaths)
            {
                T asset = Resources.Load<T>(filePath);
                if (asset != null && filter(asset))
                {
                    assetList.Add(asset);
                }
            }
            return assetList;
        }

        private static List<T> FilterAssetList<T>(List<T> currentAssetList, System.Predicate<T> keepFilter) where T : Object
        {
            currentAssetList.RemoveAll(asset =>
            {
                return asset == null || !keepFilter(asset);
            });
            return currentAssetList;
        }
    }
}