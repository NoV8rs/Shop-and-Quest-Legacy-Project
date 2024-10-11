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
                else
                {
                    log.AddMessage($"Did not buy {currentItem.Name}.");
                }
            }
        }

        public void Draw()
        {
            if (!itemSold) // This is for the shop text
            {
                gamelog.AddMessage($"Shop available at position ({pos.x}, {pos.y}).");
                //Console.SetCursorPosition(pos.x, pos.y); // This is for the shop text
                //Console.Write("g");
            }
        }
    }
}