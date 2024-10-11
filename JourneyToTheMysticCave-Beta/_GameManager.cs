using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace JourneyToTheMysticCave_Beta
{
    internal class _GameManager
    {
        #region Declarations

        Map map = new Map();
        GameStats gameStats = new GameStats();
        Gamelog gamelog = new Gamelog();
        Player player = new Player();
        LevelManager levelManager = new LevelManager();
        LegendColors legendColors = new LegendColors();
        HUD hUD = new HUD();
        EnemyManager enemyManager = new EnemyManager();
        ItemManager itemManager = new ItemManager();
        QuestLog questLog = new QuestLog();
        Item item;
        MeleeEnemyKillQuest meleeQuest = new MeleeEnemyKillQuest();
        MageEnemyKillQuest mageQuest = new MageEnemyKillQuest();
        BossEnemyKillQuest bossQuest = new BossEnemyKillQuest();
        private List<Shop> _shops;
        ShopManager shopManager = new ShopManager();
        //ShopManager shopManagers = new ShopManager();

        bool gameOver = false;
        bool playerWon = false;

        #endregion

        public _GameManager(List<Shop> shop)
        {
            Init();
            this._shops = shop;
        }

        public void Gameplay()
        {
            TutorialText();
            map.Update();
            hUD.Update();
            legendColors.Update();
            Draw();

            while (!gameOver)
            {
                Update();
                Draw();
                CheckGameOver();
            }
            int topPosition = Math.Min(100, Console.BufferHeight - 1);
            Console.SetCursorPosition(20, topPosition);
            EndGame();
        }

        private void Init()
        {
            levelManager.Init(player);
            map.Init(levelManager, legendColors, player, enemyManager, itemManager, gamelog);
            gameStats.Init(levelManager, map, item);
            player.Init(map, gameStats, legendColors, enemyManager, levelManager, itemManager, _shops, gamelog);
            legendColors.Init(gameStats, map, levelManager);
            enemyManager.Init(gameStats, levelManager, legendColors, gamelog, player, map, questLog);
            itemManager.Init(gameStats, levelManager, legendColors, gamelog, player, map, enemyManager);
            gamelog.Init(player, enemyManager, itemManager, gameStats, map, questLog);
            hUD.Init(player, enemyManager, itemManager, map, legendColors, questLog, gameStats);
            shopManager.Init(gameStats, legendColors, player, map);
            questLog.AddQuest(meleeQuest); // Add the melee quest to the quest log
            questLog.AddQuest(mageQuest);
            questLog.AddQuest(bossQuest);

            //_shops = new List<Shop>();
            
        }
        
        private void Update()
        {
            player.Update();
            levelManager.Update();
            map.Update();
            legendColors.Update();
            enemyManager.Update();
            itemManager.Update();
            hUD.Update();
            gamelog.Update();
            shopManager.Update();
        }

        private void Draw()
        {
            map.Draw();
            player.Draw();
            legendColors.Draw();
            itemManager.Draw();
            enemyManager.Draw();
            hUD.Draw();
            gamelog.Draw();
        }

        void TutorialText()
        {
            Console.CursorVisible = false;

            Console.WriteLine("Text Based RPG Beta");
            Console.WriteLine();
            Console.WriteLine("Move:");
            DisplaySymbolsInColumns("Up   ", "W");
            DisplaySymbolsInColumns("Up-Left", "Q");
            Console.WriteLine();
            DisplaySymbolsInColumns("Down ", "S");
            DisplaySymbolsInColumns("Up-Right", "E");
            Console.WriteLine();
            DisplaySymbolsInColumns("Left ", "A");
            DisplaySymbolsInColumns("Down-Left", "Z");
            Console.WriteLine();
            DisplaySymbolsInColumns("Right", "D");
            DisplaySymbolsInColumns("Down-Right", "C");
            Console.WriteLine();
            Console.WriteLine("Game must be played in full screen mode for best experience.");
            Console.WriteLine();

            Console.WriteLine("Press any key to continue");
            Console.ReadKey(true);
            Console.Clear();
        }

        private void DisplaySymbolsInColumns(string direction, string description)
        {
            Console.Write($"{direction} = {description}");

            for (int i = 0; i < 5; i++)
            {
                Console.Write(" ");
            }
        }

        void CheckGameOver()
        {
            if (bossQuest.IsComplete
                )
            {
                gameOver = true;
                playerWon = true;
            }
            if (player.healthSystem.dead)
            {
                gameOver = true;
                playerWon = false;
            }
        }

        void EndGame()
        {
            if (!player.healthSystem.dead)
                Console.WriteLine(player.name + " has won! Press enter to exit");
            else
                Console.WriteLine(player.name + " has died, press enter to exit");

            ConsoleKeyInfo input = Console.ReadKey();

            while (input.Key != ConsoleKey.Enter)
            {
                Console.WriteLine("Press Enter");
                input = Console.ReadKey();
            }
            System.Environment.Exit(0);
        }
    }
}
    
