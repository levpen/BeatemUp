using UnityEngine;

namespace Beatemup.Enemy
{
    public class EnemyFactory
    {
        private readonly Transform playerPos = GameObject.FindWithTag("Player").transform;
        public GameObject CreateEnemy(EnemyType enemyType)
        {
            EnemyBuilder builder = new EnemyBuilder()
                .SetBasePrefab(enemyType.enemyPrefab)
                //.SetWeaponPrefab(enemyType.weaponPrefab)
                .SetSpeed(enemyType.speed)
                .SetDistance(enemyType.distance)
                .SetHealth(enemyType.health)
                .SetDamage(enemyType.damage)
                .SetMoneyToAdd(enemyType.moneyToAdd)
                .SetXpToAdd(enemyType.xpToAdd)
                .SetPlayerPosition(playerPos);
            return builder.Build();
        }
    }
}