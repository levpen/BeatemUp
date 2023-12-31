using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Beatemup.Beat
{
    public class BeatController : MonoBehaviour
    {
        [SerializeField] private BeatType[] beatBatch;
        private AudioSource[] sources;
        private const int BEAT_COUNT = 8; //playing with 1/8 notes

        [SerializeField] private List<List<int>> batchArray;

        [SerializeField] private float bpm = 120f;
        private float bpmInSeconds;
        public Transform firePoint;
        public Transform crossHair;

        [SerializeField] private Animator playerAnimator;
        public static Coroutine mainCoroutine;

        private int instrumentsNumber;
        [SerializeField] private TextMeshProUGUI textBpm;

        // private bool initialInstrument = true;

        public void StartBeatLoop()
        {
            mainCoroutine = StartCoroutine(PlayBatch());
        }

        private float UpdateBpmInSeconds()
        {
            bpmInSeconds = 60f / bpm / (BEAT_COUNT / 4f);
            return bpmInSeconds;
        }
        
        void Awake()
        {
            UpdateBpmInSeconds();
            textBpm.text = "BPM: "+bpm;
            foreach (var beatType in beatBatch)
            {
                var comp = this.AddComponent<AudioSource>();
                comp.playOnAwake = false;
                comp.clip = beatType.clip;
            }

            instrumentsNumber = beatBatch.Length;
            //Hat, kick, snare, clap
            batchArray = new List<List<int>>();
            for (int i = 0; i < BEAT_COUNT; ++i)
            { 
                batchArray.Add(new List<int>(instrumentsNumber));
                for (int j = 0; j < instrumentsNumber; ++j)
                {
                    batchArray[i].Add(0);
                }
                Debug.Log(batchArray[i].Count);
            }

            sources = GetComponents<AudioSource>();
            AddInstrument(1);
            // AddInstrument(3);
        }

        public float GetBpm() {
            return bpm;
        }

        public void UpdateBpm(float newBpm)
        {
            bpm = newBpm;
            textBpm.text = "BPM: "+bpm;
            UpdateBpmInSeconds();
        }

        public void AddInstrument(int instrument)
        {
            Batch num = (Batch)instrument;
            var b = Array.Find(beatBatch, x => x.batch == num);
            for(int i = 0; i < BEAT_COUNT; ++i) {
                batchArray[i][(int)num] = 0;
            }
            foreach(var i in b.GetPattern())
            {
                batchArray[i][(int)num] = 1;
            }
        }

        private IEnumerator PlayBatch()
        {
            int curBeat = 0;
            //Main music cycle
            while (true)
            {
                bool fired = false;
                for (int i = 0; i < sources.Length; ++i)
                {
                    if (batchArray[curBeat][i] == 1)
                    {
                        if (!fired)
                        {
                            fired = true;
                            // Debug.Log("triggered");
                            playerAnimator.SetTrigger("shootAttack");
                        }

                        sources[i].Play();
                        beatBatch[i].strategy.Fire(firePoint, crossHair);
                    }
                }

                yield return new WaitForSecondsRealtime(bpmInSeconds);
                if (fired)
                {
                    playerAnimator.SetTrigger("shootAttack");
                }

                curBeat = (curBeat + 1) % BEAT_COUNT;
            }
        }
    }
}