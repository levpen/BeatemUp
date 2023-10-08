using TMPro;
using UnityEngine;

namespace Beatemup
{
    public class HudController : MonoBehaviour
    {
        public TextMeshProUGUI level;
        public TextMeshProUGUI hp;
        public Canvas abilitySelection;
        
        private void Awake()
        {
            level.text = "Level: 1";
            hp.text = "HP: 100";
        }

        public void ChangeHp(float newHp)
        {
            hp.text = "HP: " + newHp;
        }
        public void LevelUp(int lvl)
        {
            // abilitySelection.gameObject.SetActive(true);
            //
            // Time.timeScale = 0;
            // Cursor.visible = true;
            // AudioListener.pause = true;
            
            level.text = "Level: " + lvl;
        }
    }
}