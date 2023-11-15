using Unity.VisualScripting;
using UnityEngine;

namespace Beatemup.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        
        private static Transform player;
        public float moveSpeed;
        public float health;
        public float damage;
        
        public GameObject xpPrefab;
        public GameObject hpPrefab;
        private Animator animator;
        private SpriteRenderer spriteRenderer;
        private bool dead;
        

        private void Awake()
        {
            player = GameObject.FindWithTag("Player").transform;
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (player != null && !dead)
            {
                var curDir = (transform.position - player.position).x;
                if (curDir > 0)
                {
                    spriteRenderer.flipX = true;
                } else if (curDir < 0)
                { 
                    spriteRenderer.flipX = false;
                }
                transform.position = Vector2.MoveTowards(transform.position, player.position, Time.deltaTime * moveSpeed);                
            }
        }


        public void ReactToHit(float dmg)
        {
            // Debug.Log("Hit");
            health -= dmg;
            spriteRenderer.color = Color.blue;
            Invoke(nameof(ResetColor), 0.3f);
            // Debug.Log(health);
            if (health <= 0)
            {
                Defeated();
            }
        }
        
        void ResetColor()
        {
            spriteRenderer.color = Color.white;
        }

        void Defeated()
        {
            dead = true;
            Destroy(GetComponent<Collider2D>());
            animator.SetTrigger("Defeated");
        }
        private void Die()
        {
            var rnd = Random.Range(0f, 1f);
            var transform1 = transform;
            Instantiate(rnd < 0.1f ? hpPrefab : xpPrefab, transform1.position, transform1.rotation);
            Destroy(this.GameObject());
        }
    }
}