﻿using Beatemup.Player;
using UnityEngine;

namespace Beatemup.Consumables
{
    public abstract class Consumable : MonoBehaviour
    {
        public bool magnetizing = false;
        public abstract void Die(PlayerController pc);
    }
}