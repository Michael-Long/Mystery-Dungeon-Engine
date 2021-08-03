using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.GameEngine.DungeonEngine.DungeonGen.Generators
{
    [CreateAssetMenu(fileName = "GridGenerator", menuName = "ScriptableObjects/Generators/GridGenerator", order = 1)]
    public class GridGenerator : GeneratorInterface
    {
        // This will generate a dungeon level with a 5x5 grid of rooms, each of which connected to each other
        public override void GenerateLevel(float xRadius, float yRadius)
        {
            throw new System.NotImplementedException();
        }
    }
}