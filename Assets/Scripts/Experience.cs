using System;
using Beatemup.Player;
using Unity.VisualScripting;
using UnityEngine;

namespace Beatemup
{
    public class Experience : MonoBehaviour
    {
        public float xpPoints = 20;
        public bool magnetizing = false;

        public void Die(PlayerController pc)
        {
            pc.AddXp(xpPoints);
            Destroy(this.GameObject());
        }
    }
}