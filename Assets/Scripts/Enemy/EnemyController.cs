using Unity.VisualScripting;
using UnityEditor;
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
            // Debug.Log(health);
            if (health <= 0)
            {
                Defeated();
            }
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
            if (rnd < 0.1f)
            {
                Instantiate(hpPrefab, transform.position, transform.rotation);
            }
            else
            {
                Instantiate(xpPrefab, transform.position, transform.rotation);
            }
            Destroy(this.GameObject());
        }
    }
}