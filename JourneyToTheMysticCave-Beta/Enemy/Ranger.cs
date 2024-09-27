using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyToTheMysticCave_Beta
{
    internal class Ranger : Enemy
    {
        Random random = new Random();

        public Ranger(int count, char character, string name, int damage, string enemyAttack, LegendColors legendColors, Player player, Gamelog log, EnemyManager enemyManager, Map map, GameStats stats) :
            base(count, character, name, damage, enemyAttack, player, enemyManager, map, log, stats, legendColors)
        {
        }

        public override void Update(Random random)
        {
            if (!healthSystem.mapDead)
            {
                Movement();
            }
            else
            {
                pos = new Point2D { x = 0, y = 0 };
                IsAlive = false;
            }
        }
       
        private void Movement()
        {
            int maxIterations = 100;
            int iterations = 0;

            if (PlayerDistance() < 12 && PlayerDistance() >= 4)
            {
                while (iterations < maxIterations)
                {
                    dx = Math.Sign(player.pos.x - pos.x);
                    dy = Math.Sign(player.pos.y - pos.y);

                    newDx = pos.x + dx;
                    newDy = pos.y + dy;

                    if (TryMove(newDx, newDy))
                        break;

                    iterations++;
                }
            }
            if (PlayerDistance() <= 3)
                AttackPlayer();
        }


        private bool TryMove(int x, int y)
        {
            if (CheckBoundaries(x, y) && !CheckRangerPos(x, y))
            {
                CheckFloor(x, y);

                if (inDeep)
                    pos = new Point2D { x = pos.x, y = pos.y };
                else
                    pos = new Point2D { x = x, y = y };
                
                return true;
            }
            return false;
        }

        private void AttackPlayer()
        {
            player.healthSystem.TakeDamage(damage, "Attacked");
            log.enemyAttack = enemyAttack;
        }

        public bool CheckRangerPos(int x, int y)
        {
            foreach (Enemy enemy in enemyManager.enemies)
            {
                if (enemy.GetType().Name == nameof(Ranger))
                {
                    if (enemy.pos.x == x && enemy.pos.y == y)
                        return true;
                }
            }
            return false;
        }
    }
}
