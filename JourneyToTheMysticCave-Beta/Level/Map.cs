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
        //game entities
        public LevelManager levelManager;
        public LegendColors legendColors;
        Player player;
        EnemyManager enemyManager;
        ItemManager itemManager;
        bool firstPlay = true;
        char[,] currentMap;

        public void Init(LevelManager levelManager, LegendColors legendColors, Player player, EnemyManager enemyManager, ItemManager itemManager)
        {
            this.levelManager = levelManager;
            this.legendColors = legendColors;
            this.player = player;
            this.enemyManager = enemyManager;
            this.itemManager = itemManager;
        }

        public void Update()
        {
            if (firstPlay || levelManager.levelChange) // only updates if levelchange has been triggered or first play.
            {
                currentMap = GetCurrentMapContent();
                firstPlay = false;
                levelManager.levelChange = false;
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
    }
}
