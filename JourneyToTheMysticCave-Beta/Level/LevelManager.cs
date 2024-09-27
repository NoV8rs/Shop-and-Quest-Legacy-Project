using System;
using System.IO;

namespace JourneyToTheMysticCave_Beta
{
    internal class LevelManager
    {
        public int mapLevel = 0;
        public int previousLevel = 0;
        private string[] mapTextFiles = new string[] { "Level\\Map0.txt", "Level\\Map1.txt", "Level\\Map2.txt" };
        private char[][,] mapContents = new char[3][,];
        public bool levelChange;

        public Player player;

        public char[][,] AllMapContents
        {
            get { return mapContents; }
        }

        public void Init(Player player) // Initialize level maps.
        {
            this.player = player;

            for (int i = 0; i < mapTextFiles.Length; i++)
            {
                string[] lines = File.ReadAllLines(mapTextFiles[i]);
                mapContents[i] = new char[lines.Length, lines[0].Length];

                for (int j = 0; j < lines.Length; j++)
                {
                    for (int k = 0; k < lines[j].Length; k++)
                    {
                        mapContents[i][j, k] = lines[j][k];
                    }
                }
            }
        }  

        public void Update() // are we changing the level or not
        {
            CheckMapChange();
        }

        private void CheckMapChange()
        {
            switch (mapLevel)
            {
                case 0:
                    if (player.pos.x == 40 && player.pos.y == 7)
                    {
                        mapLevel = 1;
                        levelChange = true;
                        player.pos = new Point2D { x = 1, y = 7 };
                    }
                    break;
                case 1:
                    if (player.pos.x == 0 && player.pos.y == 7)
                    {
                        mapLevel = 0;
                        levelChange = true;
                        player.pos = new Point2D { x = 39, y = 7 };
                    }
                    if (player.pos.x == 37 && player.pos.y == 3)
                    {
                        mapLevel = 2;
                        levelChange = true;
                        player.pos = new Point2D {x = 37, y = 21 };
                    }
                    break;
                case 2:
                    if (player.pos.x == 37 && player.pos.y == 22)
                    {
                        mapLevel = 1;
                        levelChange = true;
                        player.pos = new Point2D { x = 37, y = 4 };
                    }
                    break;
            }
            if(mapLevel != previousLevel)
            {
                Console.Clear();
                previousLevel = mapLevel;
            }
        }

        private char[,] GetMapContent(int mapLevel)
        {
            if (mapLevel >= 0 && mapLevel < AllMapContents.Length)
                return AllMapContents[mapLevel];
            else
                throw new IndexOutOfRangeException("Index is out of range.");
        }

        public bool InitialBoundaries(int x, int y, int levelNumber)
        {
            return x >= 0 && x < GetMapContent(levelNumber).GetLength(1) && y >= 0 && y < GetMapContent(levelNumber).GetLength(0) &&
                GetMapContent(levelNumber)[y, x] != '#' && GetMapContent(levelNumber)[y, x] != '~' && 
                GetMapContent(levelNumber)[y, x] != '^' && GetMapContent(levelNumber)[y,x] != '*' &&
                GetMapContent(levelNumber)[y, x] != 'P';
        }
    }
}
