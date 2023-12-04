﻿using UnityEngine;

namespace Beatemup.Weapon {
    public abstract class WeaponStrategy : ScriptableObject {
        [SerializeField] protected float projectileLifetime = 4f;
        public GameObject projectilePrefab;
        [SerializeField] public int price;

        public abstract void Fire(Transform firePoint, Transform crosshair);
    }
}