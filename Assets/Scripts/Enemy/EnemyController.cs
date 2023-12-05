using Unity.VisualScripting;
using UnityEngine;
using Beatemup.UI;
using Beatemup.Consumables;

namespace Beatemup.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        private HudController hudController;
        private static Transform player;
        public float moveSpeed;
        public float health;
        public float damage;
        public int moneyToAdd;
        public float xpToAdd;
        // [SerializeField] private float hpUpdateInterval = 1f;
        // private float timer = 0;
        
        public GameObject xpPrefab;
        public GameObject hpPrefab;
        public GameObject speedBoosterPrefab;
        private Animator animator;
        private SpriteRenderer spriteRenderer;
        private bool dead;
        private bool rotated = false;
        

        private void Awake()
        {
            player = GameObject.FindWithTag("Player").transform;
            hudController = GameObject.FindWithTag("HUD").GetComponent<HudController>();
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            // timer += Time.deltaTime;
            if (player != null && !dead)
            {
                var curDir = (transform.position - player.position).x;
                if (!rotated && curDir > 0)
                {
                    transform.Rotate(0,180,0);
                    rotated = true;
                } else if (rotated && curDir < 0)
                { 
                    transform.Rotate(0,-180,0);
                    rotated = false;
                }
                transform.position = Vector2.MoveTowards(transform.position, player.position, Time.deltaTime * moveSpeed);
            }
            // if(timer >= hpUpdateInterval) {
            //     timer = 0;
            //     health += 1/health;
            // }
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
            animator.SetTrigger("Defeated");
            Destroy(GetComponent<Collider2D>());
        }
        private void Die()
        {
            var rnd = Random.Range(0f, 1f);
            var transform1 = transform;
            hudController.ChangeMoney(moneyToAdd);
            var curPrefab = xpPrefab;
            if(rnd < 0.01f) {
                curPrefab = speedBoosterPrefab;
            } else if(rnd < 0.2f) {
                curPrefab = hpPrefab;
            }
            var xp = Instantiate(curPrefab, transform1.position, transform1.rotation);
            if(xp.GetComponent<Experience>()) {
                xp.GetComponent<Experience>().xpPoints = xpToAdd;
                if(xpToAdd >= 50)
                    xp.GetComponent<SpriteRenderer>().color = Color.green;
                if(xpToAdd >= 80)
                    xp.GetComponent<SpriteRenderer>().color = Color.magenta;
            }
            Destroy(this.GameObject());
        }
    }
}