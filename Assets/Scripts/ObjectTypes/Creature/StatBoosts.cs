using UnityEngine;
using UnityEditor;

namespace Assets.ObjectTypes.Creature
{
    [System.Serializable]
    public class StatBoosts
    {
        [Tooltip("Specifies the boosts on this creature's Attack stat")]
        [Range(-6, 6)]
        public int attackBoost = 0;
        [Tooltip("Specifies the boosts on this creature's Defense stat")]
        [Range(-6, 6)]
        public int defenseBoost = 0;
        [Tooltip("Specifies the boosts on this creature's Special Attack stat")]
        [Range(-6, 6)]
        public int spAttackBoost = 0;
        [Tooltip("Specifies the boosts on this creature's Special Defense stat")]
        [Range(-6, 6)]
        public int spDefenseBoost = 0;
    }
}
