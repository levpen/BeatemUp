using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Beatemup.UI
{
    public class HudController : MonoBehaviour
    {
        public TextMeshProUGUI level;
        public TextMeshProUGUI hp;
        public TextMeshProUGUI money;
        
        //Progress bar controls
        public Image mask;
        private int currentLvl = 1;

        [SerializeField] int currentMoney;
        [SerializeField] float currentXp;
        [SerializeField] float maximumXp = 100;
        [SerializeField] private float levelMultiplier = 1.2f;
        [SerializeField] private UIController uiConroller;

        private void Start()
        {
            ChangeXp(currentXp);
            level.text = "Level: 1";
            hp.text = "HP: 100";
            money.text = "" + currentMoney;
        }
        public int GetMoney() {
            return currentMoney;
        }

        public void ChangeHp(float newHp)
        {
            hp.text = "HP: " + newHp;
        }

        public void AddXp(float xpToAdd)
        {
            ChangeXp(currentXp + xpToAdd);
            while (currentXp >= maximumXp)
            {
                LevelUp();
            }
        }

        public void ChangeMoney(int moneyToAdd)
        {
            currentMoney += moneyToAdd;
            money.text = "" + currentMoney;
        }

        public void ChangeXp(float newXp)
        {
            currentXp = newXp;
            mask.fillAmount = currentXp / maximumXp;
        }
        
        public void LevelUp()
        {
            uiConroller.ActivateSelection();
            
            ChangeXp(0);
            ++currentLvl;
            maximumXp *= levelMultiplier;
            
            level.text = "Level: " + currentLvl;
        }
    }
}