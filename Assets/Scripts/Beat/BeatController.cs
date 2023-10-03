using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Beatemup.Beat
{
    public class BeatController : MonoBehaviour
    {
        [SerializeField] private BeatMap[] beatBatch;
        private AudioSource[] sources;
        private const int BEAT_COUNT = 8;   //playing with 1/8 notes
        
        [SerializeField] private int[,] batchArray;     //TODO: possible change of structure
        
        [SerializeField] private float bpm = 120f;
        private float bpmInSeconds;
        public Transform firePoint;
        public Transform crossHair;


        void Awake()
        {
            bpmInSeconds = 60f / bpm / (BEAT_COUNT / 4);
            foreach (var beatMap in beatBatch)
            {
                var comp = this.AddComponent<AudioSource>();
                comp.playOnAwake = false;
                comp.clip = beatMap.beat.clip;
            }
            
            //Hat, kick, snare
            batchArray = new[,]
            {
                { 1, 1, 0 }, { 1, 0, 0 }, { 1, 0, 1 }, { 1, 0, 0 }, { 1, 1, 0 }, { 1, 0, 0 }, { 1, 0, 1 }, { 1, 1, 0 }
            };
            sources = GetComponents<AudioSource>();
        }

        private void Start()
        {
            StartCoroutine(PlayBatch());
        }
        
        private IEnumerator PlayBatch()
        {
            int curBeat = 0;
            //Main music cycle
            while (true)
            {
                for (int i = 0; i < sources.Length; ++i)
                {
                    if (batchArray[curBeat, i] == 1)
                    {
                        sources[i].Play();
                        beatBatch[i].strategy.Fire(firePoint, crossHair);
                    }
                }
                yield return new WaitForSecondsRealtime(bpmInSeconds);
                curBeat = (curBeat + 1) % BEAT_COUNT;
            }
        }
    }
}