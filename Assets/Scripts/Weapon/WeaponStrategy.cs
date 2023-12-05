using UnityEngine;

namespace Beatemup.Weapon {
    public abstract class WeaponStrategy : ScriptableObject {
        [SerializeField] protected float projectileLifetime = 4f;
        public GameObject projectilePrefab;
        [SerializeField] protected int price;
        public int currentPrice;
        public void SetInitialPrice() => currentPrice = price;
        public abstract void Fire(Transform firePoint, Transform crosshair);
    }
}