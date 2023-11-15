using UnityEngine;

namespace Beatemup.Weapon {
    [CreateAssetMenu(fileName = "PassiveShot", menuName = "Beatemup/WeaponStrategy/PassiveShot")]
    public class PassiveShot : WeaponStrategy {
        public override void Fire(Transform firePoint, Transform crosshair) {
            //Debug.Log(crosshair);
            var position = firePoint.position;
            
            var projectile = Instantiate(projectilePrefab, position, Quaternion.Euler(0, 0, 0));
            projectile.transform.SetParent(firePoint);
            
            Destroy(projectile, projectileLifetime);
        }
    }
}