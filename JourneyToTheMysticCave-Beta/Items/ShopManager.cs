using System;
using System.Collections.Generic;

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
        Gamelog gamelog;

        public ShopManager()
        {
            this.gamelog = gamelog;
            shops = new List<Shop>();
        }

        public void Init(GameStats gameStats, LegendColors legendColors, Player player, Map map)
        {
            this.gameStats = gameStats;
            this.legendColors = legendColors;
            this.player = player;
            this.map = map;
            shops = new List<Shop>();

            HashSet<(int, int)> usedPositions = new HashSet<(int, int)>();

            for (int i = 0; i < gameStats.ShopCount; i++)
            {
                int x, y;
                do
                {
                    x = random.Next(map.GetMapColumnCount());
                    y = random.Next(map.GetMapRowCount());
                } while (usedPositions.Contains((x, y)) || !map.EmptySpace(x, y, gameStats.ShopCharacter));

                usedPositions.Add((x, y));
                Point2D shopPosition = new Point2D { x = x, y = y };
                Shop shop = new Shop(shopPosition);
                shops.Add(shop);
                map.GetCurrentMapContent()[shopPosition.y, shopPosition.x] = gameStats.ShopCharacter;
            }
        }

        public List<Shop> GetShops()
        {
            return shops;
        }
    }
}