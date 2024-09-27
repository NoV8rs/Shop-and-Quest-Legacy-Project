using JourneyToTheMysticCave_Beta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyToTheMysticCave_Beta
{
    internal class Mage : Enemy
    {
        public int randomMovementCount = 0;

        public Mage(int count, char character, string name, int damage, string enemyAttack, LegendColors legendColors, Player player, Gamelog log, Map map, EnemyManager enemyManager, GameStats stats) :
            base(count, character, name, damage, enemyAttack, player, enemyManager, map, log, stats, legendColors)
        {
        }

        public override void Update(Random random)
        {
            if (!healthSystem.mapDead)
            {
                Movement(random);
            }
            else
            {
                pos = new Point2D { x = 0, y = 0 };
                IsAlive = false;
            }
        }

       
        void Movement(Random random)
        {
            if (!healthSystem.mapDead)
            {
                if (randomMovementCount == 10)
                    pos = RandomPlacement(random);
                else
                {
                    if (pos.x == player.pos.x || pos.y == player.pos.y)
                        AttackPlayer();
                    else
                        randomMovementCount++;
                }
            }
        }

        private void AttackPlayer()
        {
            if (PlayerDistance() <= 8)
            {
                player.healthSystem.TakeDamage(damage, "Attacked");
                log.enemyAttack = enemyAttack;
            }
        }

        private Point2D RandomPlacement(Random random)
        {
            int x, y;
            x = random.Next(0, map.GetCurrentMapContent().GetLength(1));
            y = random.Next(0, map.GetCurrentMapContent().GetLength(0));

            if (CheckValidMovement(x, y))
            {
                randomMovementCount = 0;
                return new Point2D { x = x, y = y };
            }
            else
            {
                randomMovementCount = 10;
                return new Point2D { x = pos.x, y = pos.y };
            }
        }

        private bool CheckValidMovement(int x, int y)
        {
            return CheckBoundaries(x, y) && !CheckMagePos(x, y) && (player.pos.x != x && player.pos.y != y);
        }


        public bool CheckMagePos(int x, int y)
        {
            foreach (Enemy enemy in enemyManager.enemies)
            {
                if (enemy.GetType().Name == nameof(Mage))
                {
                    if (enemy.pos.x == x && enemy.pos.y == y)
                        return true;
                }
            }
            return false;
        }


    }
}