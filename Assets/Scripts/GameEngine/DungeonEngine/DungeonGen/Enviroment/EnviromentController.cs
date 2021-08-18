using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.GameEngine.DungeonEngine.DungeonGen.Enviroment {
    public class EnviromentController : MonoBehaviour
    {
        [Tooltip("This is the list of tilemaps that is to be used in this enviroment. These are walls, floors, water, etc.")]
        public List<EnviromentMap> maps;
    }
}