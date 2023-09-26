// using UnityEngine;
//
// namespace Beatemup {
//     public abstract class Weapon : MonoBehaviour {
//         [SerializeField] protected WeaponStrategy weaponStrategy;
//         [SerializeField] protected Transform firePoint;
//         
//         
//         void OnValidate() => layer = gameObject.layer;
//         
//         void Start() => weaponStrategy.Initialize();
//         
//         public void SetWeaponStrategy(WeaponStrategy strategy) {
//             weaponStrategy = strategy;
//             weaponStrategy.Initialize();
//         }
//     }
// }