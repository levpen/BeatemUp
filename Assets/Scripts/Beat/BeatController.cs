using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Beatemup.Beat
{
    public class BeatController : MonoBehaviour
    {
        [SerializeField] private BeatMap[] beatBatch;
        private AudioSource[] sources;
        private const int BEAT_COUNT = 8; //playing with 1/8 notes

        [SerializeField] private int[,] batchArray; //TODO: possible change of structure

        [SerializeField] private float bpm = 120f;
        private float bpmInSeconds;
        public Transform firePoint;
        public Transform crossHair;

        [SerializeField] private Animator playerAnimator;
        public static Coroutine mainCoroutine;


        void Awake()
        {
            bpmInSeconds = 60f / bpm / (BEAT_COUNT / 4f);
            foreach (var beatMap in beatBatch)
            {
                var comp = this.AddComponent<AudioSource>();
                comp.playOnAwake = false;
                comp.clip = beatMap.beat.clip;
            }

            //Hat, kick, snare
            batchArray = new[,]
            {
                { 0, 1, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 1, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }
            };
            sources = GetComponents<AudioSource>();
        }

        public void AddInstrument(int instrument)
        {
            Batch num = (Batch)instrument;
            switch (num)
            {
                case (Batch.hat):
                    for (int i = 0; i < BEAT_COUNT; ++i)
                    {
                        batchArray[i, (int)num] = 1;
                    }

                    break;
                case (Batch.snare):
                    for (int i = 0; i < BEAT_COUNT; ++i)
                    {
                        if (i == 2 || i == 6)
                            batchArray[i, (int)num] = 1;
                    }

                    break;
            }
            mainCoroutine = StartCoroutine(PlayBatch());
        }

        private void Start()
        {
            mainCoroutine = StartCoroutine(PlayBatch());
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
                    if (batchArray[curBeat, i] == 1)
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