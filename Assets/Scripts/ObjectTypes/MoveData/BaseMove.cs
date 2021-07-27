using UnityEngine;

namespace Assets.ObjectTypes.MoveData
{
    public enum MoveCategory
    {
        Physical,
        Special,
        Status
    }

    public enum MoveTarget
    {
        EnemyInFront, // e.g. Tackle
        EnemyInFrontCutCorners, // e.g. Ember
        ThreeInFront, // e.g. Wide Slash
        EnemyInFrontUpTo2Away, // e.g. Quick Attack
        Facing, // e.g. Psych Up
        FacingCutCorners, // e.g. Gastro Acid
        LineOfSightEnemy, // e.g. Stay Away Orb
        LineOfSight, // e.g. Bubble
        EnemiesWithin1TileRange, // e.g. Lovely Kiss,
        User, // e.g. Harden
        FloorAllies, // e.g. Milk Drink
        FloorEnemies, // e.g. Perish Song
        FloorAll, // e.g. Trick Room
        RoomAllies, // e.g. Softboiled
        RoomAlliesExceptUser, // e.g. Helping Hand
        RoomEnemies, // e.g. Sweet Scent
        RoomAll, // e.g. Haze
        RoomAllExceptUser, // e.g. Earthquake
        Attacker, // e.g. Snore
    }

    [System.Serializable]
    //[CreateAssetMenu(fileName = "New Move", menuName = "Toolkit/Move", order = 2)]
    public class BaseMove : ScriptableObject
    {
        [Header("Basic Data")]
        [Tooltip("Unique ID for this move")]
        public int id = -1;
        [Tooltip("Name and category for this move")]
        public BaseMoveText inGameText = new BaseMoveText();

        [Header("Dungeon Data")]
        [Tooltip("Determines the elemental type for this move")]
        public ElementType primaryType = ElementType.Normal;
        [Tooltip("Determines what kind of move this is: Physical Attacking Move, Special Attacking Move, or a Status Move")]
        public MoveCategory category = MoveCategory.Physical;
        [Tooltip("The base power of a move, used in the damage calcuation when this move is used")]
        public int basePower = 100;
        [Tooltip("The amount of Power Points (PP) this move has. Indicates how many times it can be used before becoming unusable")]
        public int basePP = 5;
        [Tooltip("Determines the success rate of this move taking effect")]
        public int accuracy = 100;
        [Tooltip("Determines the range and affected targets by this move")]
        public MoveTarget target = MoveTarget.EnemyInFront;

        [Header("Execute Move")]
        [Tooltip("The script that will execute when this move is used")]
        public ScriptableObject moveScript = null;
    }
}