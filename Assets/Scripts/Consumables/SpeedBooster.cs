

using Beatemup.Player;
using Unity.VisualScripting;
using UnityEngine;

namespace Beatemup.Consumables
{
    public class SpeedBooster : Consumable
    {
        [SerializeField] private double multiplier = 1.2;
        public override void Die(PlayerController pc)
        {
            Destroy(this.GameObject());
        }
    }
}