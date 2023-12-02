using System;
using Beatemup.Beat;
using TMPro;
using UnityEngine;

namespace Beatemup.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private BeatController beatController;
        [SerializeField] private Canvas abilitySelection;
        [SerializeField] private Canvas pauseMenu;
        [SerializeField] private Canvas shopMenu;
        [SerializeField] private Canvas afterDeadMenu;
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private TextMeshProUGUI bestTime;
        [SerializeField] private AbilitySelector abilitySelector;
        bool active;
        private float timer;
        public bool timeStopped;
        
        private void Start()
        {
            ResumeTime();
        }

        public void Update()
        {
            if (!abilitySelection.gameObject.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape))
            {
                ToggleScreen(pauseMenu);
            }
            if (!abilitySelection.gameObject.activeInHierarchy && Input.GetKeyDown(KeyCode.I))
            {
                ToggleScreen(shopMenu);
            }

            if (!timeStopped)
            {
                timer += Time.deltaTime;
                timerText.text = String.Format("{0:00}:{1:00}", (int)timer/60, (int)timer%60);
            }
        }

        private void StopTime()
        {
            timeStopped = true;
            StopCoroutine(BeatController.mainCoroutine);
            Time.timeScale = 0;
            Cursor.visible = true;
            // AudioListener.pause = true;
        }
        
        private void ResumeTime()
        {
            timeStopped = false;
            Time.timeScale = 1;
            Cursor.visible = false;
            AudioListener.pause = false;
            beatController.StartBeatLoop();
        }
        public void ToggleScreen(Canvas screen)
        {
            if (!timeStopped && !screen.gameObject.activeSelf)
            {
                screen.gameObject.SetActive(true);
                StopTime();
            }
            else if(timeStopped && screen.gameObject.activeSelf)
            {
                screen.gameObject.SetActive(false);
                ResumeTime();
            }
        }

        public void ActivateSelection()
        {
            abilitySelection.gameObject.SetActive(true);
            abilitySelector.UpdateAbilities();
            StopTime();
        }
        public void DeactivateSelection()
        {
            abilitySelection.gameObject.SetActive(false);
            ResumeTime();
        }
        public void UpdateBestTime()
        {
            ToggleScreen(afterDeadMenu);
            if (!PlayerPrefs.HasKey("bestTime"))
            {
                PlayerPrefs.SetFloat("bestTime", timer);
            } else if(PlayerPrefs.GetFloat("bestTime") < timer) {
                PlayerPrefs.SetFloat("bestTime", timer);
            }
            var best = PlayerPrefs.GetFloat("bestTime");
            bestTime.text = String.Format("{0:00}:{1:00}", (int)best/60, (int)best%60);
        }
    }
}