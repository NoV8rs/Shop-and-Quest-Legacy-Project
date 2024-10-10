using System;

namespace JourneyToTheMysticCave_Beta
{
    internal class ItemBrought
    {
        public string Name { get; }
        public int Price { get; }
        private Action<Player> effect;

        public ItemBrought(string name, int price, Action<Player> effect)
        {
            Name = name;
            Price = price;
            this.effect = effect;
        }

        public void BuyItem(Player player, Gamelog log, GameStats stats)
        {
            if (stats.MoneyCount >= Price)
            {
                stats.MoneyCount -= Price;
                effect(player);
                log.AddMessage($"Bought {Name} for {Price} coins.");
            }
            else
            {
                log.AddMessage($"Not enough money to buy {Name}. It costs {Price} coins.");
            }
        }
    }
}