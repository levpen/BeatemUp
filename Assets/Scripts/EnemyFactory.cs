using UnityEngine;

namespace Beatemup
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
                .SetPlayerPosition(playerPos);
            return builder.Build();
        }
    }
}