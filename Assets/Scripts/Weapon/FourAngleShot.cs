using UnityEngine;

namespace Beatemup.Weapon {
    [CreateAssetMenu(fileName = "FourAngleShot", menuName = "Beatemup/WeaponStrategy/FourAngleShot")]
    public class FourAngleShot : WeaponStrategy {
        public override void Fire(Transform firePoint, Transform crosshair) {
            //Debug.Log(crosshair);
            var position = firePoint.position;
            Vector3 rot = crosshair.position - position;
            // var rotZ = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;

            var deltaRot = -135;
            for(int i = 0; i < 4; ++i) {

                var projectile = Instantiate(projectilePrefab, position, Quaternion.Euler(0, 0, deltaRot));
                projectile.transform.SetParent(firePoint);
                
                Destroy(projectile, projectileLifetime);
                deltaRot += 90;
            }
            
        }
    }
}