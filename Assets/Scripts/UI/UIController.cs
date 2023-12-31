﻿using System;
using Beatemup.Beat;
using Beatemup.Enemy;
using TMPro;
using UnityEngine;

namespace Beatemup.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private BeatController beatController;
        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private Canvas abilitySelection;
        [SerializeField] private Canvas pauseMenu;
        [SerializeField] private Canvas shopMenu;
        [SerializeField] private Canvas afterDeadMenu;
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private TextMeshProUGUI bestTime;
        [SerializeField] private TextMeshProUGUI currentRunTime;
        [SerializeField] private AbilitySelector abilitySelector;
        [SerializeField] private ShopController shopController;
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
                shopController.UpdateAllText();
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
            beatController.StopAllCoroutines();
            enemySpawner.StopAllCoroutines();
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
            enemySpawner.StartEnemyCoroutines();
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
            currentRunTime.text = String.Format("{0:00}:{1:00}", (int)timer/60, (int)timer%60);
        }
    }
}