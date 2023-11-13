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
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private AbilitySelector abilitySelector;
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
                TogglePause();
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
            AudioListener.pause = true;
        }
        
        private void ResumeTime()
        {
            timeStopped = false;
            Time.timeScale = 1;
            Cursor.visible = false;
            AudioListener.pause = false;
            beatController.StartBeatLoop();
        }
        public void TogglePause()
        {
            if (!pauseMenu.gameObject.activeSelf)
            {
                pauseMenu.gameObject.SetActive(true);
                StopTime();
            }
            else
            {
                pauseMenu.gameObject.SetActive(false);
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
    }
}