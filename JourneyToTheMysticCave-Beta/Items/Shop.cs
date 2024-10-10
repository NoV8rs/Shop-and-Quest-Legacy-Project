using System;

namespace JourneyToTheMysticCave_Beta
{
    internal class Shop : GameEntity
    {
        private ItemBrought[] items;
        private ItemBrought currentItem;
        private Random random;
        private bool itemSold;

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

        public void Interact(Player player, Gamelog log, GameStats gameStats)
        {
            if (currentItem != null && !itemSold)
            {
                log.AddMessage($"Item: {currentItem.Name}, Price: {currentItem.Price} coins. Press 'Y' to buy.");
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Y)
                {
                    currentItem.BuyItem(player, log, gameStats);
                    itemSold = true;
                }
                else
                {
                    log.AddMessage($"Did not buy {currentItem.Name}.");
                }
            }
        }

        public void Draw()
        {
            if (!itemSold)
            {
                Console.SetCursorPosition(pos.x, pos.y);
                Console.Write('S');
            }
        }
    }
}