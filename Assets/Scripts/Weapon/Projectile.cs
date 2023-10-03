﻿using Beatemup.Enemy;
using UnityEngine;

namespace Beatemup.Weapon
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float damage;
        [SerializeField] private GameObject shootPrefab;
        [SerializeField] private GameObject hitPrefab;

        private Transform parent;

        public void SetSpeed(float speed) => this.speed = speed;
        public void SetDamage(float damage) => this.damage = damage;
        public void SetParent(Transform parent) => this.parent = parent;
        
        void Start()
        {
            if (shootPrefab != null)
            {
                var shootVFX = Instantiate(shootPrefab, transform.position, Quaternion.identity);
                shootVFX.transform.forward = gameObject.transform.forward;
                shootVFX.transform.parent.SetParent(parent);

                //TODO: possible change
                DestroyParticleSystem(shootVFX);
            }
            transform.SetParent(null);
        }

        private void Update()
        {
            transform.position += transform.right * (speed * Time.deltaTime);
        }
        


        private void OnCollisionEnter2D(Collision2D other)
        {
            //Debug.Log("Collision projectile");
            if (hitPrefab != null)
            {
                ContactPoint2D contact = other.contacts[0];
                var hitVFX = Instantiate(hitPrefab, contact.point, Quaternion.identity);
            
                //TODO: possible change
                DestroyParticleSystem(hitVFX);
            }

            var target = other.gameObject.GetComponent<EnemyController>();
            if (target != null)
            {
                target.ReactToHit(damage);
            }
            Destroy(gameObject);
        }

        private void DestroyParticleSystem(GameObject vfx)
        {
            var ps = vfx.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                ps = vfx.GetComponentInChildren<ParticleSystem>();
            }

            Destroy(ps, ps.main.duration);
        }
    }
}