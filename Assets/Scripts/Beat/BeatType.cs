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
        public Batch batch;
        [SerializeField] private int[] pattern;

        public void SetPattern(int[] arr)
        {
            int n = arr.Length;
            pattern = new int [n];
            for(int i = 0; i < n; ++i)
            {
                pattern[i] = arr[i];
            }
        }
        public int[] GetPattern()
        {
            return pattern;
        }
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