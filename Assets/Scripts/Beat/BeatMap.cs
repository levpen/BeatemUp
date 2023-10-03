using Beatemup.Weapon;
using UnityEngine;

namespace Beatemup.Beat
{
    [CreateAssetMenu(fileName = "BeatMap", menuName = "Beatemup/BeatMap", order = 1)]
    public class BeatMap : ScriptableObject
    {
        public BeatType beat;
        public WeaponStrategy strategy;
    }
}