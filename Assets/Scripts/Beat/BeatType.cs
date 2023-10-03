using Beatemup.Weapon;
using UnityEngine;

namespace Beatemup.Beat
{
    [CreateAssetMenu(fileName = "BeatType", menuName = "Beatemup/BeatType", order = 1)]
    public class BeatType : ScriptableObject
    {
        public AudioClip clip;
        public float timeInterval;
    }
}