using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.ObjectTypes;
using Assets.Player;
using Assets.GameEngine.Entities;

namespace Assets.GameEngine.DungeonEngine
{
    public class DungeonMaster : MonoBehaviour
    {
        // This is the big boy. This should be the main coordinator for obtaining information about the current dungeon/floor.
        // Also behaves somewhat like an API, where other features can call stuff on it for data/algorithms. Reusable stuff should be here and labeled well.

        // This is also a key spot to put A LOT of error checking and logging, since lots of stuff will be using these functions. It'll be handy in finding bugs early.

        private PlayerCreature Player = null;
        private List<AICreature> Teammates = new List<AICreature>();
        private List<AICreature> Escorts = new List<AICreature>();
        private List<AICreature> Enemies = new List<AICreature>();

        private List<Item> Items = new List<Item>();

        public void Awake()
        {
            Entity[] EntityList = FindObjectsOfType<Entity>();
            foreach (Entity entity in EntityList)
            {
                switch (entity.GetEntityType())
                {
                    case EntityType.Player:
                        SetPlayer(entity.gameObject);
                        break;
                    case EntityType.Teammate:
                        AddTeammate(entity.gameObject);
                        break;
                    case EntityType.Escort:
                        AddEscort(entity.gameObject);
                        break;
                    case EntityType.Enemy:
                        AddEnemy(entity.gameObject);
                        break;
                    case EntityType.Item:
                        AddItem(entity.gameObject);
                        break;
                }
            }
        }

        public void Start()
        {
            StartCoroutine(toggleSpeedUp());
        }

        public void RefreshObjects()
        {
            Player = null;
            ClearTeammates();
            ClearEscorts();
            ClearEnemies();
            ClearItems();

            Entity[] EntityList = FindObjectsOfType<Entity>();
            foreach (Entity entity in EntityList)
            {
                switch (entity.GetEntityType())
                {
                    case EntityType.Player:
                        SetPlayer(entity.gameObject);
                        break;
                    case EntityType.Teammate:
                        AddTeammate(entity.gameObject);
                        break;
                    case EntityType.Escort:
                        AddEscort(entity.gameObject);
                        break;
                    case EntityType.Enemy:
                        AddEnemy(entity.gameObject);
                        break;
                    case EntityType.Item:
                        AddItem(entity.gameObject);
                        break;
                }
            }
        }

        // --- Player Methods ---

        public PlayerCreature GetPlayer()
        {
            return Player;
        }

        public void SetPlayer(GameObject Player)
        {
            if (Player.GetComponent<Creature>())
                this.Player = Player.GetComponent<PlayerCreature>();
            else
                Debug.LogWarning("Tried to assign Non-Creature Object As Player");
        }

        public void SetPlayer(PlayerCreature Player)
        {
            this.Player = Player;
        }

        public IEnumerator toggleSpeedUp()
        {
            PlayerControls Controls = PlayerControls.getInstance();
            while (true)
            {
                List<KeyCode> PressedKeys = Controls.GetPressedPlayerControls();
                bool isRunning = PressedKeys.Contains(Controls.BackKey);

                Player.setRunning(isRunning);
                if (Player.GetAnimationController())
                    Player.GetAnimationController().SetDurationSpeedScale(isRunning ? 0.333f : 1f);

                foreach (var teammate in Teammates)
                {
                    teammate.setRunning(isRunning);
                    if (teammate.GetAnimationController())
                        teammate.GetAnimationController().SetDurationSpeedScale(isRunning ? 0.333f : 1f);
                }

                foreach (var escort in Escorts)
                {
                    escort.setRunning(isRunning);
                    if (escort.GetAnimationController())
                        escort.GetAnimationController().SetDurationSpeedScale(isRunning ? 0.333f : 1f);
                }

                foreach (var enemy in Enemies)
                {
                    enemy.setRunning(isRunning);
                    if (enemy.GetAnimationController())
                        enemy.GetAnimationController().SetDurationSpeedScale(isRunning ? 0.333f : 1f);
                }

                yield return null;
            }
        }

        // --- Teammate Methods ---

        public List<AICreature> GetAllTeammates()
        {
            return Teammates;
        }

        public AICreature GetTeammateByName(string searchName)
        {
            return Teammates.Find((AICreature obj) => { return obj.name == searchName; });
        }

        public void AddTeammate(GameObject Teammate)
        {
            if (Teammate.GetComponent<AICreature>())
                Teammates.Add(Teammate.GetComponent<AICreature>());
            else
                Debug.LogWarning("Tried to add Non-AICreature Object to Teammate List");
        }

        public void AddTeammate(AICreature Teammate)
        {
            Teammates.Add(Teammate);
        }

        public void RemoveTeammate(GameObject Teammate)
        {
            if (!Teammate.GetComponent<AICreature>())
            {
                Debug.LogWarning("Given GameObject doesn't have a AICreature");
                return;
            }

            if (!Teammates.Remove(Teammate.GetComponent<AICreature>()))
                Debug.LogWarning("Tried to remove Object not in Teammate List");
        }

        public void RemoveTeammate(AICreature Teammate)
        {
            if (!Teammates.Remove(Teammate))
                Debug.LogWarning("Tried to remove Object not in Teammate List");
        }

        public void ClearTeammates()
        {
            Teammates.Clear();
        }

        // --- Escort Methods ---

        public List<AICreature> GetAllEscorts()
        {
            return Escorts;
        }

        public AICreature GetEscortByName(string searchName)
        {
            return Escorts.Find((AICreature obj) => { return obj.name == searchName; });
        }

        public void AddEscort(GameObject Escort)
        {
            if (Escort.GetComponent<AICreature>())
                Escorts.Add(Escort.GetComponent<AICreature>());
            else
                Debug.LogWarning("Tried to assign Non-AICreature Object to Escort List");
        }

        public void AddEscort(AICreature Escort)
        {
            Escorts.Add(Escort);
        }

        public void RemoveEscort(GameObject Escort)
        {
            if (!Escort.GetComponent<AICreature>())
            {
                Debug.LogWarning("Given GameObject doesn't have a AICreature");
                return;
            }

            if (!Escorts.Remove(Escort.GetComponent<AICreature>()))
                Debug.LogWarning("Tried to remove Object that wasn't in Escort List");
        }

        public void RemoveEscort(AICreature Escort)
        {
            if (!Escorts.Remove(Escort))
                Debug.LogWarning("Tried to remove Object that wasn't in Escort List");
        }

        public void ClearEscorts()
        {
            Escorts.Clear();
        }

        // --- Enemy Methods ---

        public List<AICreature> GetAllEnemies()
        {
            return Enemies;
        }

        /*
        public List<GameObject> GetAllEnemiesInRoom(Room SearchRoom)
        {
            // We'll deff need this method at some point. Useful for a ton of moves/items
        }
        */

        public void AddEnemy(GameObject Enemy)
        {
            if (Enemy.GetComponent<AICreature>())
                Enemies.Add(Enemy.GetComponent<AICreature>());
            else
                Debug.LogWarning("Tried to add Non-AICreature Object to Enemy List");
        }

        public void AddEnemy(AICreature Enemy)
        {
            Enemies.Add(Enemy);
        }

        public void RemoveEnemy(GameObject Enemy)
        {
            if (!Enemy.GetComponent<AICreature>())
            {
                Debug.LogWarning("Given GameObject doesn't have a AICreature");
                return;
            }

            if (!Enemies.Remove(Enemy.GetComponent<AICreature>()))
                Debug.LogWarning("Tried to remove Object that wasn't in Enemy List");
        }

        public void RemoveEnemy(AICreature Enemy)
        {
            if (!Enemies.Remove(Enemy.GetComponent<AICreature>()))
                Debug.LogWarning("Tried to remove Object that wasn't in Enemy List");
        }

        public void ClearEnemies()
        {
            Enemies.Clear();
        }

        // --- Item Functions ---

        public List<Item> GetAllItemsOnFloor(bool includeHidden)
        {
            if (includeHidden)
                return Items;
            else
            {
                // Currently don't have this API for items. Might be worth, or to have their lists seperate?
                //return Items.FindAll((GameObject item) => { return item.isHidden; });
                return Items;
            }
        }

        public void AddItem(GameObject Item)
        {
            if (Item.GetComponent<Item>())
                Items.Add(Item.GetComponent<Item>());
            else
                Debug.LogWarning("Tried to Add Non-Item GameObject to Items List");
        }

        public void AddItem(Item Item)
        {
            Items.Add(Item);
        }

        public void RemoveItem(Item Item)
        {
            if (!Items.Remove(Item.GetComponent<Item>()))
                Debug.LogWarning("Tried to remove Object not in Items List");
        }

        public void ClearItems()
        {
            Items.Clear();
        }

        // --- Bulk Functions ---
        public List<AICreature> GetAllAICreatures()
        {
            List<AICreature> AICreatures = new List<AICreature>(GetAllTeammates());
            AICreatures.AddRange(GetAllEscorts());
            AICreatures.AddRange(GetAllEnemies());
            return AICreatures;
        }

        public List<AICreature> GetAllFriendlies()
        {
            List<AICreature> AICreatures = new List<AICreature>(GetAllTeammates());
            AICreatures.AddRange(GetAllEscorts());
            return AICreatures;
        }

        // --- Dungeon Management Functions ---

        public void CleanUp()
        {
            StopAllCoroutines();
        }

        // --- Turn Order Hooks ---
        // These are methods that will call the various hooks on different type of "things"
        // Implementations of these will be able to run code during these phases.

        public void PrePlayerProcess()
        {

        }

        public void PostPlayerProcess()
        {

        }

        public void PreMovementProcess()
        {

        }

        public void PostMovementProcess()
        {

        }

        public void PreTeammateProcess()
        {

        }

        public void PostTeammateProcess()
        {

        }

        public void PreEnemyProcess()
        {

        }

        public void PostEnemyProcess()
        {

        }
    }
}