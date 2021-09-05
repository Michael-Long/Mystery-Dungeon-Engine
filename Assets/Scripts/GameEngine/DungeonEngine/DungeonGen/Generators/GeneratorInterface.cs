using System.Collections.Generic;
using UnityEngine;

using Assets.GameEngine.DungeonEngine.DungeonGen.Enviroment;

namespace Assets.GameEngine.DungeonEngine.DungeonGen.Generators
{
    public abstract class GeneratorInterface : ScriptableObject
    {
        // This is the methods that every generator needs, that way it can be swapped out within the DungeonGenerator
        public abstract DungeonMap GenerateLevel(int xRadius, int yRadius);

    }
}