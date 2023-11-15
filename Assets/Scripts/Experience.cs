﻿using Beatemup.Player;
using Unity.VisualScripting;
using UnityEngine;

namespace Beatemup
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