using Beatemup.Player;
using Unity.VisualScripting;

namespace Beatemup.Consumables
{
    public class Experience : Consumable
    {
        public float xpPoints = 20;

        public override void Die(PlayerController pc)
        {
            pc.AddXp(xpPoints);
            Destroy(this.GameObject());
        }
    }
}