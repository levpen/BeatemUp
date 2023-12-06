using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Beatemup.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<EnemyType> enemyTypes;
        [SerializeField] private int maxEnemies = 100000;
        public float spawnInterval = 0.5f;
        [SerializeField] private float statsUpdateInterval = 1f;
        // private float spawnUpdateTimer = 0f;
        private int maxEnemyType = 1;
        
        public static Collider2D spawnZone;
        

        private EnemyFactory enemyFactory;

        private float spawnTimer;
        private int enemiesSpawned;

        private float additionalHp = 0f;
        [SerializeField] private float additionalHpDelta = 1;

        void Start()
        {
            enemyFactory = new EnemyFactory();
            spawnZone = GetComponent<Collider2D>();
            StartEnemyCoroutines();
        }
        public void StartEnemyCoroutines() {
            StartCoroutine(SpawnEnemies());
            StartCoroutine(UpdateEnemyStats());
        }
        private IEnumerator SpawnEnemies() {
            while(enemiesSpawned < maxEnemies) {
                SpawnEnemy();
                yield return new WaitForSecondsRealtime(spawnInterval);
            }
        }

        private IEnumerator UpdateEnemyStats()
        {
            while(true) {
                spawnInterval /= 0.05f*spawnInterval+1f;
                additionalHp += additionalHpDelta;
                yield return new WaitForSecondsRealtime(statsUpdateInterval);
            }
            
        }


        // void Update()
        // {
        //     // spawnTimer += Time.deltaTime;
        //     // if (enemiesSpawned < maxEnemies && spawnTimer >= spawnInterval)
        //     // {
        //     //     SpawnEnemy();
        //     //     spawnTimer = 0f;
        //     // }
        //     // spawnUpdateTimer += Time.deltaTime;
        //     // if(spawnUpdateTimer >= spawnTimeUpdate) {
        //     //     spawnUpdateTimer = 0f;
        //     //     spawnInterval /= 0.05f*spawnInterval+1f;
        //     //     additionalHp += additionalHpDelta;
        //     // }
        // }
        public void AddEnemyType()
        {
            if(maxEnemyType < enemyTypes.Count) {
                ++maxEnemyType;
            }
        }

        private void SpawnEnemy()
        {
            EnemyType enemyType = enemyTypes[Random.Range(0, enemyTypes.Count)];

            var instance = enemyFactory.CreateEnemy(enemyType);
            instance.GetComponent<EnemyController>().health += additionalHp;
            enemiesSpawned++;
        }
    }
}