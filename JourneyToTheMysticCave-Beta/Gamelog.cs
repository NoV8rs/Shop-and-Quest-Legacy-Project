using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyToTheMysticCave_Beta
{
    internal class Gamelog
    {
        public Player player;
        public EnemyManager enemyManager;
        public ItemManager itemManager;
        public GameStats gameStats;
        public Map map;
        public QuestLog questLog;

        public string enemyAttack;
        int columnCount = 0;
        int rowCount = 0;
        
        private List<string> messages = new List<string>();

        public void Init(Player player, EnemyManager enemyManager, ItemManager itemManager, GameStats gamestats, Map map, QuestLog questLog)
        {
            this.player = player ?? throw new ArgumentNullException(nameof(player));
            this.enemyManager = enemyManager ?? throw new ArgumentNullException(nameof(enemyManager));
            this.itemManager = itemManager ?? throw new ArgumentNullException(nameof(itemManager));
            this.gameStats = gamestats ?? throw new ArgumentNullException(nameof(gamestats));
            this.map = map ?? throw new ArgumentNullException(nameof(map));
            this.questLog = questLog ?? throw new ArgumentNullException(nameof(questLog));
        }

        public void Update()
        {
            rowCount = map.GetMapRowCount() + 12;
            columnCount = 0;

            for (int i = 0; i < 13; i++) // Assuming 9 lines for HUD display
            {
                Console.SetCursorPosition(columnCount, rowCount + i);
                Console.Write(new string(' ', Console.WindowWidth));
            }
        }
        
        public void LogQuestStatus()
        {
            foreach (var quest in questLog.GetQuests())
            {
                string status = quest.IsComplete ? "Completed" : $"{quest.KillCount}/{quest.KillTarget} kills";
                Console.WriteLine($"{quest.Name}: {quest.Description} - {status}");
            }
        }

        public void Draw()
        {
            rowCount = map.GetMapRowCount() + 12; // Assuming 9 lines for HUD display
            columnCount = 0; // Assuming 9 lines for HUD display
            Console.SetCursorPosition(columnCount, rowCount);
            Console.Write("Game Log:\n");
            LogAttack();
            LogFloorDamage();
            LogTrap();
            LogPickUp();
            LogEnemyDeath();
        }
        
        public void AddMessage(string message)
        {
            messages.Add(message);
            Console.WriteLine(message);
        }
        
        public List<string> GetMessages()
        {
            return messages;
        }

        #region PickUps
        private void LogPickUp()
        {
            for (int i = 0; i < itemManager.items.Count; i++)
            {
                if (itemManager.items[i].pickedUp)
                {
                    if (itemManager.items[i].name == "Money")
                        Console.Write($"{player.name} picked up money \n");
                    else if (itemManager.items[i].name == "Potion")
                    {
                        if(player.healthSystem.health <= 100)
                            Console.Write($"{player.name} picked up potion, player has healed by {gameStats.PotionHeal} \n");
                        else if(player.healthSystem.health > 100)
                            Console.Write($"{player.name} cannot heal anymore \n");
                    }
                    else if (itemManager.items[i].name == "Sword")
                        Console.Write($"{player.name} picked up sword, player damage increased by {gameStats.SwordMultiplier} \n");

                    itemManager.items[i].pickedUp = false;
                }
            }
        }

        #endregion

        #region Attack 
        private void LogAttack()
        {
            if (player.healthSystem.hurt)
            {
                Console.Write($"{player.name} was attacked by {enemyAttack} \n");
                player.healthSystem.hurt = false;
                enemyAttack = null;
            }
            if (player.GetLastEnountered() != null)
            {
                if (player.GetLastEnountered().healthSystem.hurt)
                {
                    Console.Write($"Attacked {player.GetLastEnountered().name} - {player.damage} damage\n");
                    player.GetLastEnountered().healthSystem.hurt = false;
                }

            }
        }
        #endregion

        #region FloorDamage
        private void LogFloorDamage()
        {
            if (player.healthSystem.floorDamage)
            {
                Console.Write($"{player.name} hurt by poison spill\n");
                player.healthSystem.floorDamage = false;
            }

            for (int i = 0; i < enemyManager.enemies.Count; i++)
            {
                if (enemyManager.enemies[i].healthSystem.floorDamage)
                {
                    Console.Write($"{enemyManager.enemies[i].name}{i} has been hurt by poison spill \n");
                    enemyManager.enemies[i].healthSystem.floorDamage = false;
                }
            }
        }

        #endregion

        #region Trap
        private void LogTrap()
        {
            if (player.healthSystem.hurtByTrap)
            {
                Console.Write($"{player.name} hurt by a trap \n");
                player.healthSystem.hurtByTrap = false;
            }

            for (int i = 0; i < enemyManager.enemies.Count; i++)
            {
                if (enemyManager.enemies[i].healthSystem.floorDamage)
                {
                    Console.Write($"{enemyManager.enemies[i].name}{i} has been hurt by a trap \n");
                    enemyManager.enemies[i].healthSystem.floorDamage = false;
                }
            }
        }


        #endregion

        #region Death
        private void LogEnemyDeath()
        {
            for (int i = 0; i < enemyManager.enemies.Count; i++)
            {
                if (!enemyManager.enemies[i].processed && enemyManager.enemies[i].healthSystem.dead)
                {
                    Console.Write($"{enemyManager.enemies[i].name} has died \n");
                    enemyManager.enemies[i].healthSystem.dead = false;
                    enemyManager.enemies[i].processed = true;
                }
            }
        }
        #endregion
    }
}
