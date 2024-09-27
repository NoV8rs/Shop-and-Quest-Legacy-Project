using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyToTheMysticCave_Beta
{
    internal class Boss : Enemy
    {
        public Boss(int count, char character, string name, int damage, string enemyAttack, LegendColors legendColors, Player player, Gamelog log, EnemyManager enemyManager, Map map, GameStats stats) :
            base(count, character, name, damage, enemyAttack, player, enemyManager, map, log, stats, legendColors)
        {
        }

        public override void Update(Random random)
        {
            if (!healthSystem.mapDead)
                Movement();
            else
            {
                pos = new Point2D { x = 0, y = 0 };
                IsAlive = false;
            }
        }

        private void Movement()
        {
            dx = Math.Sign(player.pos.x - pos.x);
            dy = Math.Sign(player.pos.y - pos.y);

            newDx = pos.x + dx;
            newDy = pos.y + dy;

            if (CheckBoundaries(newDx, newDy))
            {
                if (player.pos.x == newDx && player.pos.y == newDy)
                    AttackPlayer(enemyAttack);
                else
                {
                    CheckFloor(newDx, newDy);
                    pos = new Point2D { x = newDx, y = newDy };
                }
            }
        }
    }
}
