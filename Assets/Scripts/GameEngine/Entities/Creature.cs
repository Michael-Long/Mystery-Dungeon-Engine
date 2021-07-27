using System.Collections.Generic;
using System.Collections;
using UnityEngine;

using Assets.Player;
using Assets.ObjectTypes.ItemData;
using Assets.ObjectTypes.MoveData;
using Assets.ObjectTypes.Creature;
using Assets.ObjectTypes;

namespace Assets.GameEngine.Entities
{
    public abstract class Creature : Entity
    {
        public enum ExperienceGrowthChoice
        {
            Calculated,
            Formula
        }

        // This is the dynamic data for an actual creature in-game.
        [Header("Main Data")]
        [Tooltip("Determines the data to base this creature instance off of")]
        public CreatureData data;
        [Tooltip("Is this creature instance a 'shiny'? Does it use its alternate colors?")]
        public bool isShiny = false;
        [Tooltip("Determines the method to calculate the growth rate of a creature. 'Calculated' will create a formula based on a maximum amount of experience needed" +
    "to reach level 100. 'Formula' will use a chosen formula to follow")]
        public ExperienceGrowthChoice experienceGrowthOption = ExperienceGrowthChoice.Calculated;

        [Header("Dungeon Data")]
        // Current level and Accumulated Experience should be tied together when the experience equations are implemented.
        [Tooltip("Creature's Current Level")]
        [Min(1)]
        public int currentLevel = 1;
        [Tooltip("Creature's current Stats")]
        public Stats currentStats = new Stats();
        [Tooltip("Creature's total accumulated experience")]
        [Min(0)]
        public long experienceSum = 0;
        [Tooltip("Creature's total accumulated IQ points")]
        [Min(0)]
        public long IQSum = 0;
        [Tooltip("The current item being held by this creature")]
        public ItemComponent holdItem = null;
        [Tooltip("This creature's current known moves")]
        public BaseMove[] knownMoves = new BaseMove[4];
        [Tooltip("Current 'boosts' to this creature's stats. Used in damage calculations")]
        public StatBoosts statBoosts = new StatBoosts();
        [Tooltip("List of current Active Statuses on this creature. Stores the Status IDs")]
        public List<Status> activeStatuses = new List<Status>();

        public abstract void setRunning(bool isRunning);
    }
}
