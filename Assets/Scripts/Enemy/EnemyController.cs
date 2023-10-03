using Unity.VisualScripting;
using UnityEngine;

namespace Beatemup.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        private static Transform target;
        private Rigidbody2D rb;
        public float moveSpeed;
        public float health;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            target = GameObject.FindWithTag("Player").transform;
        }

        private void Update()
        {
            var position = transform.position;
            Vector2 direction = (target.position - position).normalized;
            // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.MovePosition(position + (Vector3)direction * (Time.deltaTime * moveSpeed));
        }

        public void ReactToHit(float damage)
        {
            //Debug.Log("Hit");
            health -= damage;
            //Debug.Log(health);
            if (health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Destroy(this.GameObject());
        }
    }
}