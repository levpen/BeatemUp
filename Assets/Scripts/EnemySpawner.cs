using System.Collections.Generic;
using UnityEngine;

namespace Beatemup
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<EnemyType> enemyTypes;
        [SerializeField] private int maxEnemies = 100;
        [SerializeField] private float spawnInterval = 0.5f;
        

        private EnemyFactory enemyFactory;

        private float spawnTimer;
        private int enemiesSpawned;

        void Start() => enemyFactory = new EnemyFactory();

        void Update()
        {
            spawnTimer += Time.deltaTime;
            if (enemiesSpawned < maxEnemies && spawnTimer >= spawnInterval)
            {
                SpawnEnemy();
                spawnTimer = 0f;
            }
        }

        private void SpawnEnemy()
        {
            EnemyType enemyType = enemyTypes[Random.Range(0, enemyTypes.Count)];

            var instance = enemyFactory.CreateEnemy(enemyType);
            
            enemiesSpawned++;
        }
    }
}