using UnityEngine;

namespace Beatemup.Enemy
{
    public class EnemyBuilder
    {
        private const float RadiusDelta = 10f;

        private GameObject enemyPrefab;
        //private GameObject weaponPrefab;
        private float speed;
        private float distance;
        private float health;
        private float damage;
        private int moneyToAdd;
        private Transform playerPosition;

        public EnemyBuilder SetBasePrefab(GameObject prefab)
        {
            enemyPrefab = prefab;
            return this;
        }

        // public EnemyBuilder SetWeaponPrefab(GameObject weapon)
        // {
        //     weaponPrefab = weapon;
        //     return this;
        // }

        public EnemyBuilder SetSpeed(float speed)
        {
            this.speed = speed;
            return this;
        }

        public EnemyBuilder SetDistance(float dist)
        {
            distance = dist;
            return this;
        }
        public EnemyBuilder SetHealth(float hp)
        {
            health = hp;
            return this;
        }
        
        public EnemyBuilder SetDamage(float dmg)
        {
            damage = dmg;
            return this;
        }
        public EnemyBuilder SetMoneyToAdd(int money)
        {
            moneyToAdd=money;
            return this;
        }

        public EnemyBuilder SetPlayerPosition(Transform pos)
        {
            playerPosition = pos;
            return this;
        }

        public GameObject Build()
        {
            GameObject instance = Object.Instantiate(enemyPrefab);

            Vector3 curRndPoint;
            do
            {
                curRndPoint = RandomPointInAnnulus(instance.transform.position, distance, distance + RadiusDelta);
            } while (!EnemySpawner.spawnZone.bounds.Contains(curRndPoint + playerPosition.position));

            instance.transform.position =
                playerPosition.position +
                curRndPoint;
            
            var controller = instance.GetComponent<EnemyController>();
            controller.moveSpeed = speed;
            controller.health = health;
            controller.damage = damage;
            controller.moneyToAdd = moneyToAdd;

            return instance;
        }


        private Vector2 RandomPointInAnnulus(Vector2 origin, float minRadius, float maxRadius)
        {
            var randomDirection = (Random.insideUnitCircle * origin).normalized;
            var randomDistance = Random.Range(minRadius, maxRadius);
            var point = origin + randomDirection * randomDistance;

            return point;
        }
    }
}