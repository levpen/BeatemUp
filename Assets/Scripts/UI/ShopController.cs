using System;
using System.Collections;
using System.Collections.Generic;
using Beatemup.Beat;
using Beatemup.UI;
using Beatemup.Weapon;
using TMPro;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Beatemup
{
    public class ShopController : MonoBehaviour
    {
        [SerializeField] private List<BeatType> abilities;
        [SerializeField] GameObject batch;
        private List<GameObject> batches = new List<GameObject>();
        private List<int> maxBeats = new List<int>();
        [SerializeField] HudController hud;
        [SerializeField] BeatController beatController;
        private AudioSource coinSound = null;
        [SerializeField] private AbilitySelector abilitySelector;
    
        private void Start()
        {
            coinSound = GetComponent<AudioSource>();
            for(int j = 0; j < abilities.Count; ++j)
            {
                BeatType b = abilities[j];
                //Debug.Log(j);
                var obj = Instantiate(batch);
                batches.Add(obj);
                obj.transform.SetParent(this.transform, false);
                var text = obj.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
                var pattern = b.GetPattern();
                maxBeats.Add(pattern.Count);
                UpdateBatchText(text, b);
                foreach(int i in pattern) {
                    var toggle = obj.transform.GetChild(1).GetChild(i).gameObject.GetComponent<Toggle>();
                    toggle.isOn = true;
                }
                int tmp = j;
                for(int i = 0; i < 8; ++i) {
                    var toggle = obj.transform.GetChild(1).GetChild(i).gameObject.GetComponent<Toggle>();
                    int tmpi = i;
                    toggle.onValueChanged.AddListener((x) => ChangePatern(tmp, tmpi, b));
                }
                obj.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => BuyBeat(tmp, b, text));
                LockBatch(j);
            }
        }
        private void UnlockBatch(int num) {
            for(int i = 0; i < 8; ++i) {
                var toggle = batches[num].transform.GetChild(1).GetChild(i).gameObject.GetComponent<Toggle>();
                toggle.interactable = true;
            }
        }
        private void LockBatch(int num) {
            for(int i = 0; i < 8; ++i) {
                var toggle = batches[num].transform.GetChild(1).GetChild(i).gameObject.GetComponent<Toggle>();
                if(!toggle.isOn)
                    toggle.interactable = false;
            }
        }
        private void ChangePatern(int num, int numi, BeatType b)
        {
            var cur = batches[num];
            var text = cur.transform.GetChild(0).gameObject;
            if(cur.transform.GetChild(1).GetChild(numi).gameObject.GetComponent<Toggle>().isOn) {
                b.AddToPattern(numi);
                UpdateBatchText(text.GetComponent<TextMeshProUGUI>(), b);
                if(b.GetPattern().Count == maxBeats[num]) {
                    LockBatch(num);
                }
                if(!abilitySelector.abilities.Contains(b))
                    beatController.AddInstrument((int)b.batch);
            } else {
                b.RemoveFromPattern(numi);
                UpdateBatchText(text.GetComponent<TextMeshProUGUI>(), b);
                UnlockBatch(num);
                if(!abilitySelector.abilities.Contains(b))
                    beatController.AddInstrument((int)b.batch);
            }
        }

        void UpdateBatchText(TextMeshProUGUI text, BeatType b) {
            text.text = b.GetName()+" "+b.GetPattern().Count+"/8 Price:"+b.strategy.price+" Damage:"+b.strategy.projectilePrefab.GetComponent<Projectile>().GetDamage();
        }
        public void UpdateAllText() {
            for(int j = 0; j < batches.Count; ++j) {
                var text = batches[j].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
                UpdateBatchText(text, abilities[j]);
            }
        }
        private void BuyBeat(int num, BeatType b, TextMeshProUGUI text)
        {
            Debug.Log(num);
            if(!abilitySelector.abilities.Contains(b) && hud.GetMoney() >= b.strategy.price) {
                hud.ChangeMoney(-b.strategy.price);
                b.strategy.price *= 2;
                UpdateBatchText(text, b);
                maxBeats[num]++;
                UnlockBatch(num);
                coinSound.Play();
            }
        }
    }
}
