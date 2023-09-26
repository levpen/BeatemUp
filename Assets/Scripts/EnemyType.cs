﻿using UnityEngine;

namespace Beatemup
{
    [CreateAssetMenu(fileName = "EnemyType", menuName = "Beatemup/EnemyType", order = 0)]
    public class EnemyType : ScriptableObject
    {
        public GameObject enemyPrefab;
        public GameObject weaponPrefab;
        public float speed;
        public float distance;
    }
}