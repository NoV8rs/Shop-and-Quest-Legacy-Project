using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyToTheMysticCave_Beta
{
    internal class Melee : Enemy
    {

        public Melee(int count, char character, string name, int damage, string enemyAttack, LegendColors legendColors, Player player, Gamelog log, EnemyManager enemyManager, Map map, GameStats stats) :
            base(count, character, name, damage, enemyAttack, player, enemyManager, map, log, stats, legendColors)
        { }

        public override void Update(Random random)
        {
            if (!healthSystem.mapDead)
                Movement(random);
            else
            {
                pos = new Point2D { x = 0, y = 0 };
                IsAlive = false;
            }
        }

        private void Movement(Random random)
        {
            int maxIterations = 100;
            int iterations = 0;

            if (PlayerDistance() < 3)
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
            else
            {
                while (iterations < maxIterations)
                {
                    int direction = random.Next(0, 4);
                    dx = (direction == 2) ? 1 : (direction == 3) ? -1 : 0;
                    dy = (direction == 0) ? 1 : (direction == 1) ? -1 : 0;

                    newDx = pos.x + dx;
                    newDy = pos.y + dy;

                    if (TryMove(newDx, newDy))
                        break;

                    iterations++;
                }
            }
        }

        private bool TryMove(int x, int y)
        {
            if(CheckBoundaries(x,y) && !CheckMeleePos(x,y))
            {
                if (player.pos.x == x && player.pos.y == y)
                    AttackPlayer(enemyAttack);
                else
                {
                    CheckFloor(x, y);

                    if (inDeep)
                        pos = new Point2D { x = pos.x , y = pos.y };
                    else
                        pos = new Point2D { x = x , y = y };
                }
                return true;
            }
            return false;
        }

        public bool CheckMeleePos(int x, int y)
        {
            foreach (Enemy enemy in enemyManager.enemies)
            {
                if (enemy.GetType().Name == nameof(Melee))
                {
                    if (enemy.pos.x == x && enemy.pos.y == y)
                        return true;
                }
            }
            return false;
        }
    }
}
