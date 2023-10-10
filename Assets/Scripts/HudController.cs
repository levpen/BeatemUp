using Beatemup.Beat;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Beatemup
{
    public class HudController : MonoBehaviour
    {
        public TextMeshProUGUI level;
        public TextMeshProUGUI hp;
        public Canvas abilitySelection;
        
        //Progress bar controls
        public Image mask;
        private int currentLvl = 1;
        [SerializeField] float currentXp;
        [SerializeField] float maximumXp = 100;
        [SerializeField] private float levelMultiplier = 1.2f;
        
        private void Start()
        {
            ChangeXp(currentXp);
            level.text = "Level: 1";
            hp.text = "HP: 100";
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

        public void ChangeXp(float newXp)
        {
            currentXp = newXp;
            mask.fillAmount = currentXp / maximumXp;
        }

        public void ActivateSelection()
        {
            StopCoroutine(BeatController.mainCoroutine);
            abilitySelection.gameObject.SetActive(true);
            //time stop
            Time.timeScale = 0;
            Cursor.visible = true;
            AudioListener.pause = true;
        }
        public void DeactivateSelection()
        {
            abilitySelection.gameObject.SetActive(false);
            //time stop
            Time.timeScale = 1;
            Cursor.visible = false;
            AudioListener.pause = false;
        }
        public void LevelUp()
        {
            ActivateSelection();
            
            ChangeXp(0);
            ++currentLvl;
            maximumXp *= levelMultiplier;
            
            level.text = "Level: " + currentLvl;
        }
    }
}