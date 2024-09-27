using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyToTheMysticCave_Beta
{
    internal class Money : Item
    {
        public Money(int count, char character, string name, LegendColors legendColors, Player player) :
            base(count, character, name, legendColors, player)
        { }

        public override void Update()
        {
            if (player.pos.x == pos.x && player.pos.y == pos.y)
                TryCollect();
        }
    }
}
