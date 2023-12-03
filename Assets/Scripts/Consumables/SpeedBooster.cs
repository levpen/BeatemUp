

using System;
using System.Collections;
using Beatemup.Beat;
using Beatemup.Player;
using Unity.VisualScripting;
using UnityEngine;

namespace Beatemup.Consumables
{
    public class SpeedBooster : Consumable
    {
        [SerializeField] private float multiplier = 2f;
        private BeatController beatController;
        [SerializeField] private float boosterTime = 3f;
        [SerializeField] private SpriteRenderer sprite;
        public void Start() {
            beatController = GameObject.FindWithTag("Player").transform.GetChild(1).GetComponent<BeatController>();
        }
        public override void Die(PlayerController pc)
        {
            var oldBpm = beatController.GetBpm();
            StartCoroutine(StartBooster(oldBpm));
            Destroy(sprite);
        }

        private IEnumerator StartBooster(float oldBpm)
        {
            beatController.UpdateBpm(oldBpm*multiplier);
            yield return new WaitForSecondsRealtime(boosterTime);
            beatController.UpdateBpm(beatController.GetBpm()/multiplier);
            Debug.Log(oldBpm);  
            Destroy(this.GameObject());
        }
    }
}