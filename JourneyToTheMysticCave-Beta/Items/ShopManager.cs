using System.Collections.Generic;
using System;

namespace JourneyToTheMysticCave_Beta
{
    internal class ShopManager
    {
        private List<Shop> shops;
        private GameStats gameStats;
        private LegendColors legendColors;
        private Player player;
        private Map map;
        private Random random = new Random();

        public ShopManager()
        {
            shops = new List<Shop>();
        }

        public void Init(GameStats gameStats, LegendColors legendColors, Player player, Map map)
        {
            this.gameStats = gameStats;
            this.legendColors = legendColors;
            this.player = player;
            this.map = map;

            for (int i = 0; i < gameStats.ShopCount; i++)
            {
                Point2D shopPosition = gameStats.PlaceCharacters(1, random);
                Shop shop = new Shop(shopPosition);
                shops.Add(shop);
            }
        }

        public void Update()
        {
            foreach (var shop in shops)
            {
                // Update shop logic if needed
            }
        }

        public void Draw()
        {
            foreach (var shop in shops)
            {
                shop.Draw();
            }
        }

        public List<Shop> GetShops()
        {
            return shops;
        }
    }
}