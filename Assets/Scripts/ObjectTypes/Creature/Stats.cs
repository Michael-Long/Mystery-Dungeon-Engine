using UnityEngine;

namespace Assets.ObjectTypes.Creature
{
    [System.Serializable]
    public class Stats
    {
        [Tooltip("Hit Points Stat, how much health the creature has")]
        [Min(1)]
        public int HP = 1;
        [Tooltip("Attack Stat, used in the damage calculation for physical moves")]
        [Min(1)]
        public int attack = 1;
        [Tooltip("Defense Stat, used in the damage calculation when hit by a physical move")]
        [Min(1)]
        public int defense = 1;
        [Tooltip("Special Attack Stat, used in the damage calculation for special moves")]
        [Min(1)]
        public int spAttack = 1;
        [Tooltip("Special Defense Stat, used in the damage calculation when hit by a special move")]
        [Min(1)]
        public int spDefense = 1;

        public Stats() { }

        public Stats(int hp, int atk, int def, int spAtk, int spDef)
        {
            HP = hp;
            attack = atk;
            defense = def;
            spAttack = spAtk;
            spDefense = spDef;
        }
    }
}