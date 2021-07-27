using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.ObjectTypes.IQGroupData
{
    [CreateAssetMenu(fileName = "New IQ Group", menuName = "Toolkit/IQ Group", order = 6)]
    public class IQGroup : ScriptableObject
    {

        [Header("Main Data")]
        [Tooltip("Unique Idea for this IQ Group")]
        public int id = -1;
        [Tooltip("Name of this IQ group")]
        public string groupName = "";

        [Tooltip("List of skills that are within this IQ Group")]
        public List<IQSkill> skillList = new List<IQSkill>();

        public IQGroup() { }

        public IQGroup(int id, string groupName)
        {
            this.id = id;
            this.groupName = groupName;
        }
    }
}