using System;

namespace JourneyToTheMysticCave_Beta
{
    internal class Shop : GameEntity
    {
        private ItemBrought[] items;
        private ItemBrought currentItem;
        GameStats gameStats = new GameStats();
        private Random random;
        private bool itemSold;
        
        private Gamelog gamelog = new Gamelog();
        
        //wpublic Point2D pos { get; set; }

        public Shop(Point2D position)
        {
            this.pos = position;
            items = new ItemBrought[]
            {
                new ItemBrought("Sword", 2, (p) => p.damage += 10),
                new ItemBrought("Potion", 1, (p) => p.healthSystem.Heal(20)),
                new ItemBrought("MaxHealth", 3, (p) => p.healthSystem.health = 100)
            };
            random = new Random();
            SelectRandomItem();
        }
        
        private Point2D AdjustPositionIfOnBorder(Point2D position, int mapWidth, int mapHeight)
        {
            int x = position.x;
            int y = position.y;

            if (x == 0) x = 1;
            if (y == 0) y = 1;
            if (x == mapWidth - 1) x = mapWidth - 2;
            if (y == mapHeight - 1) y = mapHeight - 2;

            return new Point2D { x = x, y = y };
        }

        private void SelectRandomItem()
        {
            if (items.Length > 0)
            {
                currentItem = items[random.Next(items.Length)];
                itemSold = false;
            }
        }

        public void Interact(Player player, Gamelog log, GameStats gameStats, LevelManager levelManager)
        {
            if (currentItem != null && !itemSold)
            {
                log.AddMessage($"Item: {currentItem.Name}, Price: {currentItem.Price} coins. Press 'Y' to buy.");
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Y)
                {
                    if (gameStats.MoneyCount >= currentItem.Price)
                    {
                        currentItem.BuyItem(player, log, gameStats);
                        itemSold = true;
                    }
                    else
                    {
                        log.AddMessage($"Not enough money to buy {currentItem.Name}. It costs {currentItem.Price} coins.");
                    }
                }
            }
        }
    }
}