using UnityEngine;

namespace Beatemup.Weapon {
    public abstract class WeaponStrategy : ScriptableObject {
        [SerializeField] protected float projectileLifetime = 4f;
        [SerializeField] protected GameObject projectilePrefab;

        public abstract void Fire(Transform firePoint, Transform crosshair);
    }
}