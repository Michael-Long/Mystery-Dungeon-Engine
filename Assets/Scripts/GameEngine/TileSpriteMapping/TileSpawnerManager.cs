using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.GameEngine.TileSpriteMapping
{
    public class TileSpawnerManager : MonoBehaviour
    {
        public GameObject floorPrefab, wallPrefab;
        [HideInInspector] public float minX, maxX, minY, maxY;
    }
}
