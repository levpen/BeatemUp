using System;
using System.Collections.Generic;
using Beatemup.Beat;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Beatemup.UI
{
    public class AbilitySelector : MonoBehaviour
    {
        private const int btnsNum = 3;
        public List<BeatType> abilities;
        [SerializeField] private BeatController beatController;
        [SerializeField] private Button[] btns = new Button[btnsNum];
        private int[] btnsIndxes = new int[btnsNum];
        [SerializeField] private UIController uiController;


        void ActivateButton(int btnIndex, int abilityIndex)
        {
            btnsIndxes[btnIndex] = abilityIndex;
            btns[btnIndex].GetComponentInChildren<TextMeshProUGUI>().text = abilities[abilityIndex].GetName();
        }

        public void UpdateAbilities()
        {
            int n = Math.Min(btnsNum, abilities.Count);

            HashSet<int> indices = new HashSet<int>();
            for (int i = 0; i < n; ++i)
            {
                int abilInd = 0;
                do
                {
                    abilInd = Random.Range(0, abilities.Count);
                } while (indices.Contains(abilInd));

                indices.Add(abilInd);
                ActivateButton(i, abilInd);
            }
            for (int i = n; i < btnsNum; i++)
            {
                btns[i].GetComponentInChildren<TextMeshProUGUI>().text = "NONE";
            }
        }

        void RemoveAbilityFromList(int index)
        {
            abilities.RemoveAt(index);
        }

        public void AddAbility(int btnIndex)
        {
            if (abilities.Count == 0)
            {
                uiController.DeactivateSelection();
            }
            else if (btnIndex >= abilities.Count)
            {
                Debug.Log("no such ability");
            }
            else
            {
                beatController.AddInstrument(abilities[btnsIndxes[btnIndex]].GetIndex());
                RemoveAbilityFromList(btnsIndxes[btnIndex]);
                uiController.DeactivateSelection();
            }
        }
    }
}