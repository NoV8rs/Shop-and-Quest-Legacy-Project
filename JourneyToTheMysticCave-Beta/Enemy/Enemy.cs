using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyToTheMysticCave_Beta
{
    internal abstract class Enemy : GameEntity
    {
        protected int count;
        
        public EnemyManager enemyManager;
        public Player player;
        public Map map;
        public Gamelog log;
        GameStats stats;
        LegendColors legendColors;
        
        public bool processed;
        public bool inDeep;
        public int waterMove;
        public string enemyAttack;

        public bool IsAlive { get; set; } = true;

        // Constructor
        public Enemy(int count, char character, string name, int damage, string enemyAttack, Player player, EnemyManager enemyManager, Map map, Gamelog log, GameStats stats, LegendColors legendColors)
        {
            this.count = count;
            this.character = character;
            this.name = name;
            this.damage = damage;
            this.player = player;
            this.enemyManager = enemyManager;
            this.enemyAttack = enemyAttack;
            this.map = map;
            this.log = log;
            this.stats = stats;
            this.legendColors = legendColors;
        }

        // Movement
        public int dx;
        public int dy;
        public int newDx;
        public int newDy;


        public abstract void Update(Random random);

        public void Draw()
        {
            if (!healthSystem.mapDead)
            {
                Console.SetCursorPosition(pos.x, pos.y);

                legendColors.MapColor(character);
                if (map.GetCurrentMapContent()[pos.y, pos.x] == 'P')
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                else if (map.GetCurrentMapContent()[pos.y, pos.x] == '~')
                    Console.BackgroundColor = ConsoleColor.Blue;

                Console.Write(character.ToString());
                Console.ResetColor();
            }
            Console.CursorVisible = false;
        }


        public int PlayerDistance() //calculates distance to player
        {
            return Math.Abs(pos.x - player.pos.x) + Math.Abs(pos.y - player.pos.y);
        }

        public void AttackPlayer(string attack)
        {
            player.healthSystem.TakeDamage(damage, "Attacked");
            log.enemyAttack = attack;
        }

        public bool CheckBoundaries(int x, int y)
        {
            return x > 0 && x < map.GetMapColumnCount() && y > 0 && y < map.GetMapRowCount() &&
                map.GetCurrentMapContent()[y, x] != '#' && map.GetCurrentMapContent()[y, x] != '^' && map.GetCurrentMapContent()[y, x] != '*';
        }

        public void CheckFloor(int x, int y)
        {
            if (map.GetCurrentMapContent()[y, x] == 'P')
            {
                healthSystem.TakeDamage(stats.PoisonDamage, "Floor");
            }
            else if (map.GetCurrentMapContent()[y, x] == '~' && !inDeep)
            {
                inDeep = true;
                waterMove = 0;
            }
            else if (inDeep)
            {
                if(waterMove == 1)
                {
                    inDeep = false;
                    waterMove = 0;
                }
                else
                    waterMove++;
            }
        }
    }
}
