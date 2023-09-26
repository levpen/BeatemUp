// using UnityEngine;
//
// namespace Beatemup {
//     [CreateAssetMenu(fileName = "SingleShot", menuName = "Beatemup/WeaponStrategy/SingleShot")]
//     public class SingleShot : WeaponStrategy {
//         public override void Fire(Transform firePoint, LayerMask layer) {
//             var projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
//             projectile.transform.SetParent(firePoint);
//             projectile.layer = layer;
//             
//             var projectileComponent = projectile.GetComponent<Projectile>();
//             projectileComponent.SetSpeed(projectileSpeed);
//             
//             Destroy(projectile, projectileLifetime);
//         }
//     }
// }