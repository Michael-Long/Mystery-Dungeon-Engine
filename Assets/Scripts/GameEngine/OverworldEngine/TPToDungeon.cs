using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.GameEngine.OverworldEngine
{
    public class TPToDungeon : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            Collider2D hitbox = Physics2D.OverlapBox(transform.position, Vector2.one * 0.8f, 0, LayerMask.GetMask("Player"));
            if (hitbox)
            {
                SceneManager.LoadScene("Test_Dungeon");
            }
        }
    }
}