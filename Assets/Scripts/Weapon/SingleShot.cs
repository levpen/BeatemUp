using UnityEngine;

namespace Beatemup.Weapon {
    [CreateAssetMenu(fileName = "SingleShot", menuName = "Beatemup/WeaponStrategy/SingleShot")]
    public class SingleShot : WeaponStrategy {
        public override void Fire(Transform firePoint, Transform crosshair) {
            //Debug.Log(crosshair);
            var position = firePoint.position;
            Vector3 rot = crosshair.position - position;
            var rotZ = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;
            
            var projectile = Instantiate(projectilePrefab, position, Quaternion.Euler(0, 0, rotZ));
            projectile.transform.SetParent(firePoint);
            
            Destroy(projectile, projectileLifetime);
        }
    }
}