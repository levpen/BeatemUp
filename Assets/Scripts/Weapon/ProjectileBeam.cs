using System;
using Beatemup.Enemy;
using UnityEngine;

namespace Beatemup.Weapon
{
    public class ProjectileBeam : Projectile
    {

        new void Start()
        {
            base.Start();
            transform.Translate(GetComponent<SpriteRenderer>().bounds.size.x/2, 0, 0);
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            var target = other.gameObject.GetComponent<EnemyController>();
            if (target != null)
            {
                target.ReactToHit(damage);
            }
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            var target = other.gameObject.GetComponent<EnemyController>();
            if (target != null)
            {
                target.ReactToHit(damage);
            }
        }
    }
}