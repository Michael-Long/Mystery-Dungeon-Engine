using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.GameEngine.TileSpriteMapping
{
    public class TileSpawner : MonoBehaviour
    {
        TileSpawnerManager manager;

        private void Awake()
        {
            manager = FindObjectOfType<TileSpawnerManager>();
            GameObject goFloor = Instantiate(manager.floorPrefab, transform.position, Quaternion.identity) as GameObject;
            goFloor.name = manager.floorPrefab.name;
            goFloor.transform.SetParent(manager.transform);
            manager.minX = System.Math.Min(manager.minX, transform.position.x);
            manager.minY = System.Math.Min(manager.minY, transform.position.y);
            manager.maxX = System.Math.Max(manager.maxX, transform.position.x);
            manager.maxY = System.Math.Max(manager.maxY, transform.position.y);
        }

        void Start()
        {
            Destroy(gameObject);
        }

        private void OnDrawGizmos()
        {
            // Gives a visual cube of where these TileSpawners are located. The cubes don't appear at runtime.
            Gizmos.color = new Color(1f, 1f, 1f, 0.5f);
            Gizmos.DrawCube(transform.position, Vector3.one);
        }
    }
}