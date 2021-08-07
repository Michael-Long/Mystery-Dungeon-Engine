using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Utility
{
    public class AudioLoopHelper : MonoBehaviour
    {

        public AudioSource[] musicSources;
        public float startLoop;
        public float endLoop;
        private int i;
        private bool started;
        // Start is called before the first frame update
        void Start()
        {
            //musicSources[0].Stop();
            //musicSources[1].Stop();
            started = false;
        }

        public void ClickHandle()
        {
            if (!started)
            {
                musicSources[0].Play();
                i = 0;
                started = true;
            }
            else
            {
                started = false;
                musicSources[0].Stop();
                musicSources[1].Stop();
                i = 0;
            }
        }
        void Update()
        {
            if (Input.GetKeyDown("space"))
            {
                ClickHandle();
            }
            if (started && musicSources[i].time >= endLoop)
            {
                musicSources[i].Stop();
                i = 1 - i;
                musicSources[i].time = startLoop;
                musicSources[i].Play();
            }
        }
    }
}