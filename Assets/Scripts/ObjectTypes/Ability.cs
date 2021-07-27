using UnityEngine;

namespace Assets.ObjectTypes
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "Toolkit/Ability", order = 5)]
    public class Ability : ScriptableObject
    {

        [Header("Basic Data")]
        [Tooltip("Unique ID for this specific Ability")]
        public int id = -1;
        [Tooltip("Name for this Ability")]
        public string abilityName = "";
        [Tooltip("Description of this Ability")]
        public string description = "";

        [Header("Execution Data")]
        [Tooltip("Script to execute when this ability is triggered")]
        public ScriptableObject executeScript = null;

        public Ability() { }

        public Ability(int id, string name)
        {
            this.id = id;
            abilityName = name;
        }

    }
}