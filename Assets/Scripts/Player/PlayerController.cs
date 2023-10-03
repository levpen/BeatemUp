using Beatemup.Enemy;
using Unity.VisualScripting;
using UnityEngine;

namespace Beatemup.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed = 50f;
        [SerializeField] private GameObject crossHair;
        [SerializeField] private float health = 100;

        [SerializeField] private new Camera camera;
        public Rigidbody2D rb;
        private Vector2 movement;

        private void Start()
        {
            Cursor.visible = false;
        }

        public void Update()
        {
            MouseMove();

            movement.x = Input.GetAxis("Horizontal");
            movement.y = Input.GetAxis("Vertical");
        }

        private void FixedUpdate()
        {
            //movement logic
            rb.MovePosition(rb.position + movement * (speed * Time.deltaTime));
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var target = other.gameObject.GetComponent<EnemyController>();
            if (target != null)
            {
                ReactToHit(target.damage);
            }
        }

        private void MouseMove()
        {
            var aim = camera.ScreenToWorldPoint(Input.mousePosition);
            aim.z = 0;
            crossHair.transform.position = aim;
        }


        private void ReactToHit(float damage)
        {
            Debug.Log("Player damaged");
            health -= damage;
            Debug.Log(health);
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