using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.GameEngine.Entities.AI
{
    public abstract class BaseAI : MonoBehaviour
    {
        protected bool isProcessing = false;

        public abstract bool shouldDoMovement();

        public abstract IEnumerator AIMovement(bool isRunning);
        public abstract IEnumerator AIAction();

        public bool IsProcessing()
        {
            return isProcessing;
        }
    }
}