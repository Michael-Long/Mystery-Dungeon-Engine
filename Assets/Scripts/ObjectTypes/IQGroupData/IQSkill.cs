using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.ObjectTypes.IQGroupData
{
    [System.Serializable]
    public class IQSkill
    {

        [Header("IQ Skill Data")]
        [Tooltip("Unique ID for this IQ Skill")]
        public int id = -1;
        [Tooltip("Name of this IQ Skill")]
        public string skillName = "";
        [Tooltip("Description of the effects of having this IQ Skill active")]
        public string description = "";
        [Tooltip("The total amount of IQ a creature needs to obtain this skill")]
        public long neededIQPoints = -1;
        [Tooltip("The script that is run when this IQ skill is active")]
        public ScriptableObject IQScript;

        public IQSkill() { }

        public IQSkill(string skillName, long neededPoints)
        {
            this.skillName = skillName;
            neededIQPoints = neededPoints;
        }

    }
}