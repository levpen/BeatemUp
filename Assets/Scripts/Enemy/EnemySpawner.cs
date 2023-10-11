using System.Collections.Generic;
using UnityEngine;

namespace Beatemup.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<EnemyType> enemyTypes;
        [SerializeField] private int maxEnemies = 100;
        [SerializeField] private float spawnInterval = 0.5f;
        
        public static Collider2D spawnZone;
        

        private EnemyFactory enemyFactory;

        private float spawnTimer;
        private int enemiesSpawned;

        void Start()
        {
            enemyFactory = new EnemyFactory();
            spawnZone = GetComponent<Collider2D>();
        }


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