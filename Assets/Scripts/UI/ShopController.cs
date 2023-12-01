using System;
using System.Collections;
using System.Collections.Generic;
using Beatemup.Beat;
using Beatemup.UI;
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
    
        private void Start()
        {
            for(int j = 0; j < abilities.Count; ++j)
            {
                BeatType b = abilities[j];
                Debug.Log(j);
                var obj = Instantiate(batch);
                batches.Add(obj);
                obj.transform.SetParent(this.transform, false);
                var text = obj.transform.GetChild(0).gameObject;
                var pattern = b.GetPattern();
                maxBeats.Add(pattern.Count);
                text.GetComponent<TextMeshProUGUI>().text = b.GetName()+" "+pattern.Count+"/8 Price:"+b.strategy.price;
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
                obj.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => BuyBeat(tmp, b));
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
                var pattern = b.GetPattern();
                b.AddToPattern(numi);
                var curCount = b.GetPattern().Count;
                text.GetComponent<TextMeshProUGUI>().text = b.GetName()+" "+curCount+"/8 Price:"+b.strategy.price;
                if(curCount == maxBeats[num]) {
                    LockBatch(num);
                }
                beatController.AddInstrument((int)b.batch);
            } else {
                b.RemoveFromPattern(numi);
                text.GetComponent<TextMeshProUGUI>().text = b.GetName()+" "+b.GetPattern().Count+"/8 Price:"+b.strategy.price;
                UnlockBatch(num);
            }
        }
        private void BuyBeat(int num, BeatType b)
        {
            // Debug.Log(num);
            if(hud.GetMoney() > b.strategy.price) {
                hud.ChangeMoney(-b.strategy.price);
                maxBeats[num]++;
                UnlockBatch(num);
            }
        }
    }
}
