using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyToTheMysticCave_Beta
{
    internal abstract class Item : GameObject
    {
        public bool collected = false;
        public bool pickedUp = false;
        public int count;
        public Player player;
        LegendColors legendColors;

        public Item(int count, char character, string name, LegendColors legendColors, Player player)
        {
            this.count = count;
            this.character = character;
            this.name = name;
            this.legendColors = legendColors;
            this.player = player;
        }

        
        public virtual void Update() { }

        public void Draw()
        {
            if (!collected)
            {
                Console.SetCursorPosition(pos.x, pos.y);
                legendColors.MapColor(character);
                Console.Write(character.ToString());
                Console.ResetColor();
            }
            Console.CursorVisible = false;
        }


        public void TryCollect()
        {
            if (!collected)
            {
                collected = true;
                pickedUp = true;
                pos = new Point2D { x = 0, y = 0 };
            }
        }
    }
}
