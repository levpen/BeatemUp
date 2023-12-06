using UnityEngine;

namespace Beatemup.Weapon {
    [CreateAssetMenu(fileName = "RandomAngleShot", menuName = "Beatemup/WeaponStrategy/RandomAngleShot")]
    public class RandomAngleShot : WeaponStrategy {
        public override void Fire(Transform firePoint, Transform crosshair) {
            //Debug.Log(crosshair);
            for(int i = 0; i < 3; ++i) {
                var position = firePoint.position;
                var projectile = Instantiate(projectilePrefab, position, Quaternion.Euler(0, 0, Random.Range(-180.0f, 180.0f)));
                projectile.transform.SetParent(firePoint);
                
                Destroy(projectile, projectileLifetime);
            }
        }
    }
}