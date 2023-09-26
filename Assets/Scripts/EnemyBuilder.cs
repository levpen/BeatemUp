﻿using UnityEngine;

namespace Beatemup
{
    public class EnemyBuilder
    {
        private const float RadiusDelta = 1f;

        private GameObject enemyPrefab;
        private GameObject weaponPrefab;
        private float speed;
        private float distance;
        private Transform playerPosition;

        public EnemyBuilder SetBasePrefab(GameObject prefab)
        {
            enemyPrefab = prefab;
            return this;
        }

        public EnemyBuilder SetWeaponPrefab(GameObject weapon)
        {
            weaponPrefab = weapon;
            return this;
        }

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

        public EnemyBuilder SetPlayerPosition(Transform pos)
        {
            playerPosition = pos;
            return this;
        }

        public GameObject Build()
        {
            GameObject instance = Object.Instantiate(enemyPrefab);

            instance.transform.position =
                playerPosition.position +
                (Vector3)RandomPointInAnnulus(instance.transform.position, distance, distance + RadiusDelta);
            instance.GetComponent<EnemyController>().moveSpeed = speed;

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