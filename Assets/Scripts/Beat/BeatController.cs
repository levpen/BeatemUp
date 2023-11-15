using System;
using System.Collections;
using System.Collections.Generic;
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

        // private bool initialInstrument = true;

        public void StartBeatLoop()
        {
            mainCoroutine = StartCoroutine(PlayBatch());
        }

        void Awake()
        {
            bpmInSeconds = 60f / bpm / (BEAT_COUNT / 4f);
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
            // initialInstrument = false;
        }

        public void AddInstrument(int instrument)
        {
            Batch num = (Batch)instrument;
            var b = Array.Find(beatBatch, x => x.batch == num);
            foreach(var i in b.GetPattern())
            {
                batchArray[i][(int)num] = 1;
            }
            // if(!initialInstrument)
            //     mainCoroutine = StartCoroutine(PlayBatch());
        }

        private void Start()
        {
            // StartBeatLoop();
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