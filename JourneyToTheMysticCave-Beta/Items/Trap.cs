using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyToTheMysticCave_Beta
{
    internal class Trap : Item
    {
        public int trapDamage;
        EnemyManager enemyManager;
        LevelManager levelManager;

        public Trap(int count, char character, string name, int trapDamage, LegendColors legendColors,  Player player, EnemyManager enemyManager, LevelManager levelManager) : 
            base(count, character, name, legendColors, player)
        {
            this.trapDamage = trapDamage;
            this.enemyManager = enemyManager;
            this.levelManager = levelManager;
        }

        public override void Update()
        {
            GameEntity entity = GetEntityAtPosition(pos.x, pos.y);

            if (entity != null)
            {
                TryCollect();
                entity.healthSystem.TakeDamage(trapDamage, "Trap");
            }
        }

       

        private GameEntity GetEntityAtPosition(int x, int y)
        {
            foreach (Enemy enemy in enemyManager.enemies)
            {
                if (enemy is Mage && levelManager.mapLevel == 1)
                {
                    if (enemy.pos.x == x && enemy.pos.y == y)
                        return enemy;
                }
                else if (enemy is Melee && levelManager.mapLevel == 2)
                {
                    if (enemy.pos.x == x && enemy.pos.y == y)
                        return enemy;
                }
                else if (enemy is Boss && levelManager.mapLevel == 2)
                {
                    if (enemy.pos.x == x && enemy.pos.y == y)
                        return enemy;
                }
            }
            if (player.pos.x == x && player.pos.y == y)
                return player;
            else return null;
        }
    }
}