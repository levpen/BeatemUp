using System.Collections.Generic;
using System.Linq;
using Beatemup.Weapon;
using Unity.Collections;
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
        [SerializeField] private List<int> pattern;

        public void SetPattern(List<int> arr)
        {
            pattern = arr;
        }
        public List<int> GetPattern()
        {
            return pattern;
        }
        public void AddToPattern(int num)
        {
            if(!pattern.Any<int>(i => i == num)) {
                pattern.Add(num);
            }
        }
        public void RemoveFromPattern(int num)
        {
            pattern.Remove(num);
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