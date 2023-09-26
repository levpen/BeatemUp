using System;
using UnityEngine;

namespace Beatemup
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private GameObject shootPrefab;
        [SerializeField] private GameObject hitPrefab;

        private Transform parent;

        public void SetSpeed(float speed) => this.speed = speed;
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
            transform.position += transform.forward * (speed * Time.deltaTime);
        }


        private void OnCollisionEnter(Collision other)
        {
            if (hitPrefab != null)
            {
                ContactPoint contact = other.contacts[0];
                var hitVFX = Instantiate(hitPrefab, contact.point, Quaternion.identity);

                //TODO: possible change
                DestroyParticleSystem(hitVFX);
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