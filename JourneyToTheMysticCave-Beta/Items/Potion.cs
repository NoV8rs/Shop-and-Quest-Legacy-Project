using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyToTheMysticCave_Beta
{
    internal class Potion : Item
    {
        public int healAmount;

        public Potion(int count, char character, string name, int healAmount, LegendColors legendColors, Player player) : 
            base(count, character, name, legendColors, player)
        {
            this.healAmount = healAmount;
        }

        public override void Update()
        {
            if (player.pos.x == pos.x && player.pos.y == pos.y)
            {
                TryCollect();
                player.healthSystem.Heal(healAmount);
            }
        }
    }
}
