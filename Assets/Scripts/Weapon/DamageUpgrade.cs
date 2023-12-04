using Beatemup.Beat;
using UnityEngine;

namespace Beatemup.Weapon {
    [CreateAssetMenu(fileName = "DamageUpgradae", menuName = "Beatemup/Upgrade/DamageUpgradae")]
    public class DamageUpgradae : Upgrade {
        [SerializeField] private float damageMultiplier;
        public override void UpgradeWeapon(){
            Debug.Log("upgrade");
            Debug.Log(upgradeName);
            var proj = beatType.strategy.projectilePrefab.GetComponent<Projectile>();
            beatType.strategy.projectilePrefab.GetComponent<Projectile>().SetDamage(proj.GetDamage()*damageMultiplier);
        }
    }
}