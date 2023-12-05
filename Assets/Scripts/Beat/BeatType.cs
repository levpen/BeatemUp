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
        public List<int> initialPattern;
        [SerializeField] private List<int> pattern;

        public void SetPattern(List<int> arr)
        {
            pattern = new List<int>();
            for(int i = 0; i < arr.Count; ++i)
                pattern.Add(arr[i]);
        }
        public List<int> GetPattern()
        {
            return pattern;
        }
        public void AddToPattern(int num)
        {
            if(!pattern.Any<int>(i => i == num)) {
                var proj = strategy.projectilePrefab.GetComponent<Projectile>();
                proj.SetDamage(proj.GetDamage() * Mathf.Max(1, pattern.Count)/(pattern.Count+1));
                pattern.Add(num);
            }
        }
        public void RemoveFromPattern(int num)
        {
            var proj = strategy.projectilePrefab.GetComponent<Projectile>();
            proj.SetDamage(proj.GetDamage() * pattern.Count/Mathf.Max(1, pattern.Count-1));
            Debug.Log(pattern.Count);
            pattern.Remove(num);
            Debug.Log(pattern.Count);
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