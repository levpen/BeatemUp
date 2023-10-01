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
                .SetWeaponPrefab(enemyType.weaponPrefab)
                .SetSpeed(enemyType.speed)
                .SetDistance(enemyType.distance)
                .SetHealth(enemyType.health)
                .SetPlayerPosition(playerPos);
            return builder.Build();
        }
    }
}