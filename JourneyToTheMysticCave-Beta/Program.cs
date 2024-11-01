using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

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
            
            string jsonString = File.ReadAllText("Data/DataDriven.json");
            GameStats gameStats = JsonSerializer.Deserialize<GameStats>(jsonString);
        }
    }
}
