using System.Collections.Generic;
using Beatemup.Beat;
using Beatemup.UI;
using Beatemup.Weapon;
using UnityEngine;

namespace Beatemup
{
    public class WorldController : MonoBehaviour
    {
        [SerializeField] private List<BeatType> beatTypes;
        public List<List<int>> patterns = new List<List<int>>();
        [SerializeField] private BeatController beatController;
        [SerializeField] private AbilitySelector abilitySelector;
        void Start() {
            for(int i = 0; i < beatTypes.Count; ++i) {
                var b = beatTypes[i];
                // Debug.Log(b.GetName());
                // Debug.Log(b.initialPattern);
                patterns.Add(b.initialPattern);
                b.SetPattern(patterns[i]);
                if(!abilitySelector.abilities.Contains(b))
                    beatController.AddInstrument((int)b.batch);

                var proj = b.strategy.projectilePrefab.GetComponent<Projectile>();
                proj.SetDamage(proj.GetInitialDamage()/b.GetPattern().Count);
            }
        }
    }
}