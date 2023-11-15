using Beatemup.Weapon;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Beatemup.Beat
{
    [CreateAssetMenu(fileName = "BeatType", menuName = "Beatemup/BeatType", order = 1)]
    public class BeatType : ScriptableObject
    {
        public AudioClip clip;
        public WeaponStrategy strategy;
        [SerializeField] private string description;
        [SerializeField] private Batch batch;

        public string GetName()
        {
            return batch.ToString();
        }
        public int GetIndex()
        {
            return (int)batch;
        }
    }
}