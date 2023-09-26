using UnityEngine;

namespace Beatemup
{
    public class EnemyController : MonoBehaviour
    {
        static Transform target;
        private Rigidbody2D rb;
        public float moveSpeed = 20;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            target = GameObject.FindWithTag("Player").transform;
        }

        private void Update()
        {
            var position = transform.position;
            Vector2 direction = (target.position - position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
            rb.MovePosition(position + (Vector3)direction * (Time.deltaTime * moveSpeed));
        }
    }
}