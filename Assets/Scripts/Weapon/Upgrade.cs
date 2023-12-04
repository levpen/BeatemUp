using Beatemup.Beat;
using UnityEngine;

namespace Beatemup.Weapon {
    public abstract class Upgrade : ScriptableObject {
        [SerializeField] protected string upgradeName;
        [SerializeField] protected BeatType beatType;
        public string GetName() => upgradeName;
        public abstract void UpgradeWeapon();
    }
}