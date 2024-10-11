using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyToTheMysticCave_Beta
{
    internal class Map
    {
        // Game entities
        public LevelManager levelManager;
        public LegendColors legendColors;
        Player player;
        EnemyManager enemyManager;
        ItemManager itemManager;
        Gamelog gamelog;
        GameStats gameStats;
        bool firstPlay = true;
        private char[,] currentMap;
        private List<Shop> shops = new List<Shop>();
        private Random random = new Random();

        public void Init(LevelManager levelManager, LegendColors legendColors, Player player, EnemyManager enemyManager, ItemManager itemManager, Gamelog gamelog)
        {
            this.levelManager = levelManager;
            this.legendColors = legendColors;
            this.player = player;
            this.enemyManager = enemyManager;
            this.itemManager = itemManager;
            this.gamelog = gamelog;

            // Initialize currentMap
            currentMap = GetCurrentMapContent();

            // Place a single shop
            PlaceSingleShop(0);
            PlaceSingleShop(1);
            PlaceSingleShop(2);
        }

        public void Update()
        {
            if (firstPlay || levelManager.levelChange) // Only updates if level change has been triggered or first play.
            {
                currentMap = GetCurrentMapContent();
                firstPlay = false;
                levelManager.levelChange = false;

                CheckPlayerShopInteraction();
            }
        }

        public void Draw()
        {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < currentMap.GetLength(0); i++)
            {
                for (int j = 0; j < currentMap.GetLength(1); j++)
                {
                    char characterToDraw = currentMap[i, j];

                    legendColors.MapColor(characterToDraw);
                    Console.Write(currentMap[i, j]);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }

        public int GetMapRowCount()
        {
            return currentMap.GetLength(0);
        }

        public int GetMapColumnCount()
        {
            return currentMap.GetLength(1);
        }

        public char[,] GetCurrentMapContent()
        {
            return levelManager.AllMapContents[levelManager.mapLevel];
        }

        public bool EmptySpace(int x, int y, int mapLevel)
        {
            if (player.pos.x == x && player.pos.y == y)
                return false;

            foreach (Enemy enemy in enemyManager.enemies)
            {
                if (enemy.GetType().Name == nameof(Ranger) && mapLevel == 0)
                {
                    if (enemy.pos.x == x && enemy.pos.y == y)
                        return false;
                }
                else if (enemy.GetType().Name == nameof(Mage) && mapLevel == 1)
                {
                    if (enemy.pos.x == x && enemy.pos.y == y)
                        return false;
                }
                else if (enemy.GetType().Name == nameof(Melee) && mapLevel == 2)
                {
                    if (enemy.pos.x == x && enemy.pos.y == y)
                        return false;
                }
            }

            foreach (Item item in itemManager.items)
            {
                switch (mapLevel)
                {
                    case 0:
                        for (int i = 0; i < itemManager.itemsLevel0; i++)
                        {
                            if (item.pos.x == x && item.pos.y == y)
                                return false;
                        }
                        break;
                    case 1:
                        for (int i = itemManager.itemsLevel0; i < itemManager.itemsLevel1; i++)
                        {
                            if (item.pos.x == x && item.pos.y == y)
                                return false;
                        }
                        break;
                    case 2:
                        for (int i = itemManager.itemsLevel1; i <= itemManager.items.Count; i++)
                        {
                            if (item.pos.x == x && item.pos.y == y)
                                return false;
                        }
                        break;
                }
            }
            return true;
        }

        public void PlaceSingleShop(int mapLevel)
        {
            int x, y;
            do
            {
                x = random.Next(GetMapColumnCount());
                y = random.Next(GetMapRowCount());
            } while (!EmptySpace(x, y, levelManager.mapLevel));
            
            Shop shop = new Shop(new Point2D { x = x, y = y });
            //shops.Add(shop);
            
            levelManager.AllMapContents[mapLevel][y, x] = 'X';
        }

        public List<Shop> GetShops()
        {
            return shops;
        }
        
        private void CheckPlayerShopInteraction()
        {
            foreach (var shop in shops)
            {
                if (player.pos.x == shop.pos.x && player.pos.y == shop.pos.y)
                {
                    shop.Interact(player, gamelog, gameStats, levelManager);
                }
            }
        }
    }
}
