using System.Collections.Generic;
using UnityEngine;

using Assets.ObjectTypes.IQGroupData;
using Assets.ObjectTypes.ItemData;
using Assets.ObjectTypes.MoveData;

namespace Assets.ObjectTypes.Creature
{
    //[CreateAssetMenu(fileName = "New Creature", menuName = "Toolkit/Creature", order = 1)]
    public class CreatureData : ScriptableObject
    {
        // This is the static data for any unique creature.
        public enum Gender
        {
            Genderless,
            Male,
            Female,
            Both
        }

        // Maybe extend this to better allow new types of movement?
        public enum MovementType
        {
            Standard,
            Water,
            Lava,
            Hovering,
            Mobile,
            Any
        }

        public enum PartySize
        {
            Standard,
            Medium,
            Large,
            Huge // Thicc
        }

        public enum ExperienceGrowthFormula
        {
            Erratic,
            Fast,
            MediumFast,
            MediumSlow,
            Slow,
            Fluctuating
        }

        // The number can be used as a unique id for a species.
        [Header("Basic Data")]
        [Tooltip("Creature's Unique ID Number")]
        public int creatureNo = 0;
        [Tooltip("Ceature's Name")]
        public string species = "";
        [Tooltip("Creature's Categorization Name")]
        public string category = "";
        [Tooltip("Potential Genders this creature can be")]
        public Gender possibleGender = Gender.Genderless;
        [Tooltip("Creature's primary elemental type")]
        public ElementType primaryType = ElementType.None;
        [Tooltip("Creature's secondary elemental type, if any")]
        public ElementType secondaryType = ElementType.None;

        [Header("Sprites")]
        [Tooltip("The portrait file for this creature")]
        public Sprite portriat = null;

        [Header("Evolution")]
        [Tooltip("Creature's collection evolution data, if any")]
        public List<Evolution> evolution = new List<Evolution>();

        [Header("Abilities")] // These could maybe be a dropdown of the found abilities? Or keep as IDs to link.
        [Tooltip("Creature's first potential ability")]
        public Ability primaryAbility = null;
        [Tooltip("Creature's second potential ability")]
        public Ability secondaryAbility = null;
        [Tooltip("Creature's secret ability")]
        public Ability secretAbility = null;

        [Header("Dungeon Properties")]
        [Tooltip("Creature's movement options. These effect whether this creature can walk in water, lava, walls, void, etc.")]
        public MovementType movementType = MovementType.Standard;
        [Tooltip("Creature's Body Size. Determines how much space within the party this creature takes")]
        public PartySize bodySize = PartySize.Standard;
        [Tooltip("Creature's 'size tier', is a factor in size-based moves")]
        [Min(0)]
        public int size = 0;
        [Tooltip("Creature's weight, is a factor in weight-based moves")]
        [Min(0)]
        public int weight = 0;
        [Tooltip("Creature's Recruit Rate, how common it is to recruit this creature upon defeat. This can be negative to require modifiers to be able to recruit them")]
        public int recruitRate = 0;
        [Tooltip("Creature's secondary recruit rate, currently unused")]
        [HideInInspector]
        public int recruitRate2 = 0; // Unused unless found otherwise
        [Tooltip("Creature's IQ group, determines the skills the creature will obtain as their IQ increases")]
        public IQGroup iQGroup; // This prob get it's own list of defined IQ groups?
        [Tooltip("Creature's Experience Yield, determines how much base experience the creaturew will give upon defeat")]
        [Min(0)]
        public int expYield = 0;
        [Tooltip("Creature's Exclusive Items list, these are the items that'd give this creature buffs if it is present in the party's bag")]
        public List<ExclusiveItem> exclusiveItems = new List<ExclusiveItem>();

        [Header("Stats")]
        [Tooltip("Creature's starting stats at level 1")]
        public Stats baseStats = new Stats();
        [Tooltip("Creature's total GAINED stats between levels 1 and 100. Used to calculate the gained stats per level")]
        public Stats totalStats = new Stats();
        [Tooltip("Selects which formula to use for this creature's growth rate")]
        public ExperienceGrowthFormula experienceFormula = ExperienceGrowthFormula.MediumFast;
        [Tooltip("Creature's total amount of experience needed to reach level 100. Used to calculate the needed experience per level")]
        public int totalExp = 0;

        [Header("Moves")]
        [Tooltip("Creature's moves that they learn at specific levels")]
        public List<LevelUpMove> levelUpMoves = new List<LevelUpMove>();
        [Tooltip("Creature's egg moves. These are possible moves the creature can know when they're hatched from an egg")]
        public List<BaseMove> eggMoves = new List<BaseMove>();
        [Tooltip("Creature's TM moves. These are possible moves the creature can learn from Technical Machines")]
        public List<BaseMove> tmMoves = new List<BaseMove>();
    }
}
