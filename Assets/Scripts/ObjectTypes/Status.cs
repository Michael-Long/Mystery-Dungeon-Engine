using UnityEngine;

namespace Assets.ObjectTypes
{
    [CreateAssetMenu(fileName = "New Status", menuName = "Toolkit/Status", order = 4)]
    public class Status : ScriptableObject
    {

        [Header("Basic Data")]
        [Tooltip("Unique ID of this status")]
        public int id = -1;
        [Tooltip("Name of this status")]
        public string statusName = "";
        [Tooltip("Description of this status")]
        public string description = "";

        [Header("Execution Data")]
        [Tooltip("Script to execute for this status")]
        public ScriptableObject statusScript = null;

        public Status() { }

        public Status(int id, string name)
        {
            this.id = id;
            statusName = name;
        }

    }
}