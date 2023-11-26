using Beatemup.Player;
using Unity.VisualScripting;

namespace Beatemup.Consumables
{
    public class HealthPoints : Consumable
    {
        public float hpPoints = 10;

        public override void Die(PlayerController pc)
        {
            pc.AddHp(hpPoints);
            Destroy(this.GameObject());
        }
    }
}