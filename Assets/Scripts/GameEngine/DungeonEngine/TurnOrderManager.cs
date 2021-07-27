using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.GameEngine.Entities;
using Assets.GameEngine.Entities.AI;

namespace Assets.GameEngine.DungeonEngine
{
    public class TurnOrderManager : MonoBehaviour
    {
        [Tooltip("Reference to the Dungeon Master so we can make API calls.")]
        public DungeonMaster DM = null;

        private TurnState CurrentState = TurnState.PrePlayer;

        public void Start()
        {
            if (!DM)
                DM = FindObjectOfType<DungeonMaster>();

            if (DM)
            {
                CurrentState = TurnState.PrePlayer;
                StartCoroutine(PlayerTurn());
            } else
            {
                Debug.LogError("Turn Order Manager doesn't have a reference to the Dungeon Master");
            }
        }

        public TurnState GetTurnState()
        {
            return CurrentState;
        }

        private IEnumerator PlayerTurn()
        {
            CurrentState = TurnState.PrePlayer;
            DM.PrePlayerProcess();

            CurrentState = TurnState.Player;
            StartCoroutine(DM.GetPlayer().DoAction());
            while (!DM.GetPlayer().GetComponent<Creature>().IsActionComplete())
            {
                yield return null;
            }

            CurrentState = TurnState.PostPlayer;
            DM.PostPlayerProcess();

            yield return null;
            StartCoroutine(MovementTurn());
        }

        private IEnumerator MovementTurn()
        {
            CurrentState = TurnState.PreMovement;
            DM.PreMovementProcess();

            CurrentState = TurnState.Movement;
            foreach (AICreature AI in DM.GetAllAICreatures())
            {
                AI.DetermineActionState();
                if (AI.IsMovementAction())
                    StartCoroutine(AI.DoAction());
            }

            CurrentState = TurnState.PostMovement;
            DM.PostMovementProcess();

            yield return null;
            StartCoroutine(FriendlyTurn());
        }

        private IEnumerator FriendlyTurn()
        {
            while (!DM.GetPlayer().IsActionComplete())
            {
                yield return null;
            }

            CurrentState = TurnState.PreTeammates;
            DM.PreTeammateProcess();

            CurrentState = TurnState.Teammates;

            foreach (AICreature Friendly in DM.GetAllFriendlies())
            {
                if (!Friendly.IsMovementAction())
                {
                    StartCoroutine(Friendly.DoAction());
                    while (!Friendly.IsActionComplete())
                    {
                        yield return null;
                    }
                }
            }

            CurrentState = TurnState.PostTeammates;
            DM.PostTeammateProcess();

            yield return null;
            StartCoroutine(EnemyTurn());
        }

        private IEnumerator EnemyTurn()
        {
            CurrentState = TurnState.PreEnemies;
            DM.PreEnemyProcess();

            CurrentState = TurnState.Enemies;
            foreach (AICreature Enemy in DM.GetAllEnemies())
            {
                if (!Enemy.IsMovementAction())
                {
                    StartCoroutine(Enemy.DoAction());
                    while (!Enemy.IsActionComplete())
                    {
                        yield return null;
                    }
                }
            }

            CurrentState = TurnState.PostEnemies;
            DM.PostEnemyProcess();

            yield return null;
            StartCoroutine(PlayerTurn());
        }
    }
}