using Beatemup.Enemy;
using Beatemup.Weapon;
using UnityEngine;

namespace Beatemup
{
    public class ProjectileCircle : Projectile
    {
        private void FixedUpdate()
        {
            var curScale = transform.localScale;
            transform.localScale = new Vector3(curScale.x+Time.fixedDeltaTime, curScale.y+Time.fixedDeltaTime, 0);
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
