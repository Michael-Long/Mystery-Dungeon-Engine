using UnityEngine;

namespace Assets.ObjectTypes.Creature
{
    [System.Serializable]
    public class Evolution
    {
        [Tooltip("The ID value of one creature that evolves from this creature.")]
        public int nextEvo = 0;
        [Tooltip("Determines the requirement needed for evolution")]
        public EvolutionType evolutionType = EvolutionType.None;
        // Depending on the evolution type, this value has different meaning:
        // Level: Required level to evolve
        // Item: Required Item to evolve
        // IQ: Required IQ to evolve
        // Trade: nothing. Trade solely needs the link cable.
        [Tooltip("The requirement needed to be met. This depends on the type of requirement. Could be the level requirement, or Item ID, or IQ level needed.")]
        public int evolutionRequirement = 0; // Prob a better way to display this to be more descriptive? Dependant on the evolution type.
        [Tooltip("An optional item requirement, for if a creature needs to meet the main requirement and have this item.")]
        public int optionalEvolutionItem = 0;

        public Evolution() { }

        public Evolution(int next, int evoType, int requirement, int optional)
        {
            nextEvo = next;
            evolutionType = (EvolutionType)evoType;
            evolutionRequirement = requirement;
            optionalEvolutionItem = optional;
        }
    }

    public enum EvolutionType
    {
        None,
        Level,
        Item,
        IQ,
        Trade
    }
}
