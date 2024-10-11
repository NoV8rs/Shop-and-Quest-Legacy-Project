using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyToTheMysticCave_Beta
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player();
            Gamelog gamelog = new Gamelog();
            List<Shop> shops = new List<Shop>();
            _GameManager gameManager = new _GameManager(shops);

            gameManager.Gameplay();
            Console.BufferHeight = 1000;
            Console.BufferWidth = 1000;
        }
    }
}
