using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyToTheMysticCave_Beta
{
    internal class Program
    {
        static _GameManager gameManager = new _GameManager();

        static void Main(string[] args)
        {
            gameManager.Gameplay();
        }
    }
}
