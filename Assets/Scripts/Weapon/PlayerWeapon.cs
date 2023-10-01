using UnityEngine;

namespace Beatemup.Weapon {
    public class PlayerWeapon : Weapon {
        float fireTimer;

        void Update() {
            fireTimer += Time.deltaTime;
            
            if (fireTimer >= weaponStrategy.FireRate) {
                weaponStrategy.Fire(firePoint);
                fireTimer = 0f;
            }
        }
    }
}